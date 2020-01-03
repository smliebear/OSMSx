namespace OilManage.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StaffRole")]
    public partial class StaffRole
    {
        [Key]
        [Column(Order = 0)]
        public Guid StaffId { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid RoleId { get; set; }
    }
}
