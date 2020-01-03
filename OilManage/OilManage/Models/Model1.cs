namespace OilManage.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Approver> Approver { get; set; }
        public virtual DbSet<Entry> Entry { get; set; }
        public virtual DbSet<Job> Job { get; set; }
        public virtual DbSet<LeaveOffice> LeaveOffice { get; set; }
        public virtual DbSet<OilMaterialOrder> OilMaterialOrder { get; set; }
        public virtual DbSet<OilMaterialOrderDetail> OilMaterialOrderDetail { get; set; }
        public virtual DbSet<OrganizationStructure> OrganizationStructure { get; set; }
        public virtual DbSet<ProcessItem> ProcessItem { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Staff> Staff { get; set; }
        public virtual DbSet<SystemResourceModule> SystemResourceModule { get; set; }
        public virtual DbSet<ProcessStepRecord> ProcessStepRecord { get; set; }
        public virtual DbSet<RoleResourceModule> RoleResourceModule { get; set; }
        public virtual DbSet<StaffRole> StaffRole { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Approver>()
                .Property(e => e.JobCode)
                .IsUnicode(false);

            modelBuilder.Entity<Entry>()
                .Property(e => e.Height)
                .HasPrecision(5, 2);

            modelBuilder.Entity<Entry>()
                .Property(e => e.IDNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Entry>()
                .Property(e => e.Tel)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Entry>()
                .Property(e => e.FamilyStatus1Tel)
                .IsUnicode(false);

            modelBuilder.Entity<Entry>()
                .Property(e => e.FamilyStatus2Tel)
                .IsUnicode(false);

            modelBuilder.Entity<Entry>()
                .Property(e => e.EmergencyContactTel)
                .IsUnicode(false);

            modelBuilder.Entity<Entry>()
                .Property(e => e.No)
                .IsUnicode(false);

            modelBuilder.Entity<LeaveOffice>()
                .Property(e => e.LeaveType)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<OilMaterialOrder>()
                .HasMany(e => e.OilMaterialOrderDetail)
                .WithRequired(e => e.OilMaterialOrder)
                .HasForeignKey(e => e.OrderId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OilMaterialOrder>()
                .HasMany(e => e.OilMaterialOrderDetail1)
                .WithRequired(e => e.OilMaterialOrder1)
                .HasForeignKey(e => e.OrderId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OilMaterialOrder>()
                .HasMany(e => e.OilMaterialOrderDetail2)
                .WithRequired(e => e.OilMaterialOrder2)
                .HasForeignKey(e => e.OrderId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProcessItem>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<ProcessItem>()
                .HasMany(e => e.Approver)
                .WithOptional(e => e.ProcessItem)
                .HasForeignKey(e => e.ProcessItemId);

            modelBuilder.Entity<ProcessItem>()
                .HasMany(e => e.Approver1)
                .WithOptional(e => e.ProcessItem1)
                .HasForeignKey(e => e.ProcessItemId);

            modelBuilder.Entity<ProcessItem>()
                .HasMany(e => e.Approver2)
                .WithOptional(e => e.ProcessItem2)
                .HasForeignKey(e => e.ProcessItemId);

            modelBuilder.Entity<Staff>()
                .Property(e => e.Status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ProcessStepRecord>()
                .Property(e => e.Type)
                .IsUnicode(false);
        }
    }
}
