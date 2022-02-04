namespace Furlough.DAL.Models
{
    public class DepartmentPositions
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public int PositionId { get; set; }
        public string? Title { get; set; }
    }
}
