using Microsoft.EntityFrameworkCore;
using POS_API.Models;

namespace POS_API.Model
{
    public class POSEntities: DbContext
    {
        public POSEntities(DbContextOptions<POSEntities> options): base(options)
        {
            
        }
        public virtual DbSet<tblCompany> tblCompanies { get; set; }
        public virtual DbSet<tblComponent> tblComponents { get; set; }
        public virtual DbSet<tblField> tblFields { get; set; }
        public virtual DbSet<tblItem> tblItems { get; set; }
        public virtual DbSet<tblLevel1> tblLevel1 { get; set; }
        public virtual DbSet<tblLevel2> tblLevel2 { get; set; }
        public virtual DbSet<tblLevel3> tblLevel3 { get; set; }
        public virtual DbSet<tblModule> tblModules { get; set; }
        public virtual DbSet<tblParty> tblParties { get; set; }
        public virtual DbSet<tblPermission> tblPermissions { get; set; }
        public virtual DbSet<tblPledger> tblPledgers { get; set; }
        public virtual DbSet<tblRole> tblRoles { get; set; }
        public virtual DbSet<tblStockDetail> tblStockDetails { get; set; }
        public virtual DbSet<tblStockMain> tblStockMains { get; set; }
        public virtual DbSet<tblTransporter> tblTransporters { get; set; }
        public virtual DbSet<tblUser> tblUsers { get; set; }
        public virtual DbSet<tblWarehouse> tblWarehouses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
