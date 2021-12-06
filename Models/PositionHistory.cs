namespace Furlough.Models
{
    public class PositionHistory
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int PositionId { get; set; }
        public int SetByUserId { get; set; }
        public DateTime DateSet { get; set; }
    }
}
