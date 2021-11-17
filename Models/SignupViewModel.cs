namespace Furlough.Models
{
    public class SignupViewModel
    {
        public int RoleId { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int? UpdateBy { get; set; }
        public DateTime JoinDate { get; set; }
        public int PositionId { get; set; }
        public int DepartmentId { get; set; }
        public string Email { get; set; } = null!;
        public string Name { get; set; } = null!;
    }
}
