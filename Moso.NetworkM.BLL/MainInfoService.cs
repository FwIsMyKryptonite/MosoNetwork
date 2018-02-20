using Moso.NetworkM.IBLL;
using Moso.NetworkM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moso.NetworkM.Common;

namespace Moso.NetworkM.BLL
{
    public partial class MainInfoService : BaseService<MainInfo>, IMainInfoService
    {

        //public override void SetCurrentDal()
        //{
        //    currentDal = this.CurrentDBSession.MainInfoDal;
        //}

        #region 权限变更
        public bool SetMainPermissionInfo(int mainId, List<int> pList)
        {
            MainInfo mainInfo = this.CurrentDBSession.MainInfoDal.LoadEntities(m => m.Id == mainId).FirstOrDefault();
            if (mainInfo != null)
            {
                mainInfo.PermissionInfo.Clear();
                foreach (int pId in pList)
                {
                    var permissionInfo = this.CurrentDBSession.PermissionInfoDal.LoadEntities(p => p.PId == pId).FirstOrDefault();
                    mainInfo.PermissionInfo.Add(permissionInfo);
                }
                return this.CurrentDBSession.SaveChanges();
            }
            return false;
        }
        #endregion

        #region 搜索功能
        public IQueryable<MainInfo> LoadSearchEntities(Model.Search.MainInfoSearch mainInfoSearch, short delFlag)
        {
            var temp = this.CurrentDBSession.MainInfoDal.LoadEntities(m => m.DelFlag == delFlag);
            if (!string.IsNullOrEmpty(mainInfoSearch.QueryText))
            {
                switch (mainInfoSearch.QueryOption)
                {
                    case "StaffName":
                        temp = temp.Where<MainInfo>(m => m.SName.Contains(mainInfoSearch.QueryText));
                        break;
                    case "EmpId": temp = temp.Where<MainInfo>(m => m.EmpId.Contains(mainInfoSearch.QueryText)); break;
                    case "AdAcount": temp = temp.Where<MainInfo>(m => m.AdAcount.Contains(mainInfoSearch.QueryText)); break;
                    case "Ip": temp = temp.Where<MainInfo>(m => m.Ip.Contains(mainInfoSearch.QueryText)); break;
                    case "MacAddress": temp = temp.Where<MainInfo>(m => m.MacAddress.Contains(mainInfoSearch.QueryText)); break;
                    case "Asset": temp = temp.Where<MainInfo>(m => m.PCAsset.Contains(mainInfoSearch.QueryText)); break;
                }
            }
            mainInfoSearch.TotalCount = temp.Count();
            return temp.OrderBy<MainInfo, int>(m => m.Id).Skip<MainInfo>((mainInfoSearch.PageIndex - 1) * mainInfoSearch.PageSize).Take<MainInfo>(mainInfoSearch.PageSize);
        }
        #endregion

        #region 批量加入姓名首字母
        public bool MainInfoModelUpdate(List<int> idList)
        {
            foreach (int id in idList)
            {
                var mainInfoModel = this.CurrentDBSession.MainInfoDal.LoadEntities(m => m.Id == id).FirstOrDefault();
                mainInfoModel.SName = Moso.NetworkM.Common.Chs2PinYinHelper.GetFirst(mainInfoModel.StaffName);
                this.CurrentDBSession.MainInfoDal.EditEntity(mainInfoModel);
            }
            return this.CurrentDBSession.SaveChanges();
        }
        #endregion

        #region 添加用户
        public bool AddModels(Dictionary<string, object> dic, MainInfo mainInfo, out string msg)
        {
            bool b = true;
            foreach (KeyValuePair<string, object> k in dic)
            {
                switch (k.Key)
                {
                    case "staffInfo":
                        StaffInfo s = k.Value as StaffInfo;
                        string sString = SerializeHelper.SerializeToString(s);
                        StaffInfo temp1 = this.CurrentDBSession.StaffInfoDal.LoadEntities(t => t.EmpId == s.EmpId).FirstOrDefault();
                        string temp1String = SerializeHelper.SerializeToString(temp1);
                        if (temp1 == null)
                        {
                            this.CurrentDBSession.StaffInfoDal.AddEntity(s);
                            b = false;
                        }
                        else if (sString != temp1String)
                        {
                            if (!string.IsNullOrEmpty(s.Company))
                            {
                                temp1.Company = s.Company;
                            }
                            if (!string.IsNullOrEmpty(s.Depart))
                            {
                                temp1.Depart = s.Depart;
                            }
                            this.CurrentDBSession.StaffInfoDal.EditEntity(temp1);
                            b = false;
                        }
                        break;
                    case "adInfo":
                        ADInfo a = k.Value as ADInfo;
                        string aString = SerializeHelper.SerializeToString(k.Value);
                        var temp2 = this.CurrentDBSession.ADInfoDal.LoadEntities(t => t.AdAcount == a.AdAcount).FirstOrDefault();
                        string temp2String = SerializeHelper.SerializeToString(temp2);
                        if (temp2 == null)
                        {
                            this.CurrentDBSession.ADInfoDal.AddEntity(a);
                            b = false;
                        }
                        else if (aString != temp2String)
                        {
                            this.CurrentDBSession.ADInfoDal.EditEntity(a);
                            b = false;
                        }
                        break;
                    case "emailInfo":
                        EmailInfo e = k.Value as EmailInfo;
                        string eString = SerializeHelper.SerializeToString(k.Value);
                        var temp3 = this.CurrentDBSession.EmailInfoDal.LoadEntities(t => t.EAddress == e.EAddress).FirstOrDefault();
                        string temp3String = SerializeHelper.SerializeToString(temp3);
                        if (temp3 == null)
                        {
                            this.CurrentDBSession.EmailInfoDal.AddEntity(e);
                            b = false;
                        }
                        else if (eString != temp3String)
                        {
                            this.CurrentDBSession.EmailInfoDal.EditEntity(e);
                            b = false;
                        }
                        break;
                }

            }
            if (b == false && this.CurrentDBSession.SaveChanges())
            {
                this.CurrentDBSession.MainInfoDal.AddEntity(mainInfo);
                if (this.CurrentDBSession.SaveChanges())
                {
                    msg = "success";
                    return true;
                }
                else
                {
                    msg = "no";
                    return false;
                }
            }
            else
            {
                msg = "noChange";
                return false;
            }
        }
        #endregion

        #region 邮箱编辑
        public bool UpdateMainEmailInfos(EmailInfo emailinfo, int mainId)
        {
            string selectedMainInfoEaddressOld = string.Empty;
            var selectedMainInfo = this.CurrentDBSession.MainInfoDal.LoadEntities(m => m.Id == mainId).FirstOrDefault();
            if (selectedMainInfo != null)
            {
                selectedMainInfoEaddressOld = selectedMainInfo.EAddress;
                //return false;
            }
            if (selectedMainInfoEaddressOld == emailinfo.EAddress)
            {
                var emainInfo66 = selectedMainInfo.EmailInfo;
                emainInfo66.EAcount = emailinfo.EAcount;
                emainInfo66.EDisplay = emailinfo.EDisplay;
                emainInfo66.EOut = emailinfo.EOut;
                emainInfo66.EPwd = emailinfo.EPwd;
                this.CurrentDBSession.EmailInfoDal.EditEntity(emainInfo66);
                return this.CurrentDBSession.SaveChanges();
            }
            else
            {
                var mainInfos = this.CurrentDBSession.MainInfoDal.LoadEntities(m => m.EAddress == selectedMainInfoEaddressOld).ToList();
                var mainInfosIdList = (from m in mainInfos
                                       select m.Id).ToList();
                foreach (MainInfo mainInfo in mainInfos)
                {
                    mainInfo.EAddress = null;
                    this.CurrentDBSession.MainInfoDal.EditEntity(mainInfo);
                }
                this.CurrentDBSession.SaveChanges();
                var emailInfo = this.CurrentDBSession.EmailInfoDal.LoadEntities(e => e.EAddress == selectedMainInfoEaddressOld).FirstOrDefault();
                this.CurrentDBSession.EmailInfoDal.DeleteEntity(emailInfo);
                this.CurrentDBSession.SaveChanges();

                var ee = this.CurrentDBSession.EmailInfoDal.LoadEntities(e => e.EAddress == emailinfo.EAddress).FirstOrDefault();
                if (ee == null)
                {
                    this.CurrentDBSession.EmailInfoDal.AddEntity(emailinfo);
                }
                else
                {
                    this.CurrentDBSession.EmailInfoDal.DeleteEntity(ee);
                    this.CurrentDBSession.SaveChanges();
                    this.CurrentDBSession.EmailInfoDal.AddEntity(emailinfo);
                    //if (ee.DelFlag == 1)
                    //{
                    //    ee.DelFlag = 0;
                    //    this.CurrentDBSession.EmailInfoDal.EditEntity(ee);
                    //}
                }
                if (this.CurrentDBSession.SaveChanges())
                {
                    foreach (int id in mainInfosIdList)
                    {
                        MainInfo mm = this.CurrentDBSession.MainInfoDal.LoadEntities(m => m.Id == id).FirstOrDefault();
                        mm.EAddress = emailinfo.EAddress;
                        this.CurrentDBSession.MainInfoDal.EditEntity(mm);
                    }
                    return this.CurrentDBSession.SaveChanges();
                }
                return false;
            }
        }
        #endregion



    }
}
