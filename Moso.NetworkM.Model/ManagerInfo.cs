//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Moso.NetworkM.Model
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    
    public partial class ManagerInfo
    {
        public ManagerInfo()
        {
            this.RoleInfo = new HashSet<RoleInfo>();
            this.O_ManagerInfo_ActionInfo = new HashSet<O_ManagerInfo_ActionInfo>();
        }
    
        public int Id { get; set; }
        public string MName { get; set; }
        public string MPwd { get; set; }
        public Nullable<System.DateTime> SubTime { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public string Remark { get; set; }
        public string Sort { get; set; }
        public Nullable<short> DelFlag { get; set; }
    
    [JsonIgnore]
        public virtual ICollection<RoleInfo> RoleInfo { get; set; }
    [JsonIgnore]
        public virtual ICollection<O_ManagerInfo_ActionInfo> O_ManagerInfo_ActionInfo { get; set; }
    }
}
