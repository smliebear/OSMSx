namespace OilManage.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProcessStepRecord")]
    public partial class ProcessStepRecord
    {
        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string Type { get; set; }

        [StringLength(500)]
        public string RecordRemarks { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StepOrder { get; set; }

        [Key]
        [Column(Order = 3)]
        public Guid WaitForExecutionStaffId { get; set; }

        [Key]
        [Column(Order = 4)]
        public DateTime CreateTime { get; set; }

        [Key]
        [Column(Order = 5)]
        public DateTime UpdateTime { get; set; }

        [Key]
        [Column(Order = 6)]
        public bool WhetherToExecute { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(50)]
        public string No { get; set; }

        [Key]
        [Column(Order = 8)]
        public Guid RefOrderId { get; set; }

        [Key]
        [Column(Order = 9)]
        public bool Result { get; set; }

        [StringLength(4000)]
        public string ExecuteMethod { get; set; }

        [StringLength(400)]
        public string Discrible { get; set; }
    }
}
