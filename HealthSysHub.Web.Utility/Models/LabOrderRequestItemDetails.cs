﻿namespace HealthSysHub.Web.Utility.Models
{
    public class LabOrderRequestItemDetails
    {
        public Guid? LabOrderRequestItemId { get; set; }
        public Guid? TestId { get; set; }
        public string? TestName { get; set; }
        public decimal? ItemQty { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
