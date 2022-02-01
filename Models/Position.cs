using System.ComponentModel.DataAnnotations;

namespace Furlough.Models
{
    public partial class Position
    {
        [Display(Name = "id", ResourceType = typeof(Resources.Models.Position.Display))]
        public int Id { get; set; }
        [Display(Name = "title", ResourceType = typeof(Resources.Models.Position.Display))]
        public string Title { get; set; } = null!;
    }
}
