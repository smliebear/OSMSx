namespace OilManage.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OilMaterialOrder")]
    public partial class OilMaterialOrder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OilMaterialOrder()
        {
            OilMaterialOrderDetail = new HashSet<OilMaterialOrderDetail>();
            OilMaterialOrderDetail1 = new HashSet<OilMaterialOrderDetail>();
            OilMaterialOrderDetail2 = new HashSet<OilMaterialOrderDetail>();
        }

        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string No { get; set; }

        public Guid ApplyPersonId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ApplyDate { get; set; }

        [StringLength(500)]
        public string Remark { get; set; }

        public bool IsDel { get; set; }

        public DateTime? CreateTime { get; set; }

        public DateTime? UpdateTime { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OilMaterialOrderDetail> OilMaterialOrderDetail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OilMaterialOrderDetail> OilMaterialOrderDetail1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OilMaterialOrderDetail> OilMaterialOrderDetail2 { get; set; }
    }
}
