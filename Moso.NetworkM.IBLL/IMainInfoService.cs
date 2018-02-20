using Moso.NetworkM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moso.NetworkM.IBLL
{
    public partial interface IMainInfoService : IBaseService<MainInfo>
    {
        bool SetMainPermissionInfo(int mainId, List<int> pList);
        IQueryable<MainInfo> LoadSearchEntities(Model.Search.MainInfoSearch mainInfoSearch, short delFlag);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        bool MainInfoModelUpdate(List<int> idList);
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="mainInfo"></param>
        /// <returns></returns>
        bool AddModels(Dictionary<string, object> dic, MainInfo mainInfo, out string msg);

        bool UpdateMainEmailInfos(EmailInfo emailinfo, int mainId);
    }
}
