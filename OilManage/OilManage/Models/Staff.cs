namespace OilManage.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Staff")]
    public partial class Staff
    {
        public Guid Id { get; set; }

        [StringLength(500)]
        public string No { get; set; }

        [StringLength(500)]
        public string Name { get; set; }

        public bool? Sex { get; set; }

        [Column(TypeName = "date")]
        public DateTime? BirthDay { get; set; }

        [StringLength(500)]
        public string NativePlace { get; set; }

        [StringLength(500)]
        public string Address { get; set; }

        [StringLength(1000)]
        public string Password { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(20)]
        public string Tel { get; set; }

        [StringLength(1)]
        public string Status { get; set; }

        public DateTime? CreateTime { get; set; }

        public DateTime? UpdateTime { get; set; }

        public Guid? JobId { get; set; }

        public Guid? OrgID { get; set; }

        public bool? IsHSEGroup { get; set; }
    }
}
