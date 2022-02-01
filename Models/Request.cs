using System.ComponentModel.DataAnnotations;

namespace Furlough.Models
{
    public partial class Request
    {
        [Display(Name = "id", ResourceType = typeof(Resources.Models.Request.Display))]
        public int Id { get; set; }
        [Display(Name = "dates", ResourceType = typeof(Resources.Models.Request.Display))]
        public string Dates { get; set; }
        [Display(Name = "requestedByUserId", ResourceType = typeof(Resources.Models.Request.Display))]
        public int RequestedByUserId { get; set; }
        [Display(Name = "requestedOn", ResourceType = typeof(Resources.Models.Request.Display))]
        public DateTime RequestedOn { get; set; }
        [Display(Name = "requestStatusId", ResourceType = typeof(Resources.Models.Request.Display))]
        public byte RequestStatusId { get; set; }
        [Display(Name = "daysAmount", ResourceType = typeof(Resources.Models.Request.Display))]
        public int DaysAmount { get; set; }
        [Display(Name = "requestTypeId", ResourceType = typeof(Resources.Models.Request.Display))]
        public int RequestTypeId { get; set; }
        [Display(Name = "reason", ResourceType = typeof(Resources.Models.Request.Display))]
        public string? Reason { get; set; }
        [Display(Name = "lud", ResourceType = typeof(Resources.Models.Request.Display))]
        public DateTime LUD { get; set; }
        [Display(Name = "lun", ResourceType = typeof(Resources.Models.Request.Display))]
        public int LUN { get; set; }
        [Display(Name = "lubUserId", ResourceType = typeof(Resources.Models.Request.Display))]
        public int LUBUserId { get; set; }
    }
}
