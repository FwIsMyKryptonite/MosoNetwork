﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Moso.NetworkM.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MosoNetEntities : DbContext
    {
        public MosoNetEntities()
            : base("name=MosoNetEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<ADInfo> ADInfo { get; set; }
        public DbSet<PermissionInfo> PermissionInfo { get; set; }
        public DbSet<StaffInfo> StaffInfo { get; set; }
        public DbSet<MainInfo> MainInfo { get; set; }
        public DbSet<EmailInfo> EmailInfo { get; set; }
        public DbSet<ManagerInfo> ManagerInfo { get; set; }
        public DbSet<RoleInfo> RoleInfo { get; set; }
        public DbSet<ActionInfo> ActionInfo { get; set; }
        public DbSet<O_ManagerInfo_ActionInfo> O_ManagerInfo_ActionInfo { get; set; }
    }
}
