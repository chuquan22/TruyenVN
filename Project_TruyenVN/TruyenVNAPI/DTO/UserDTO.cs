using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TruyenVNAPI.DTO
{
    public class UserDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public bool isActive { get; set; }
        public String DateOfBirth { get; set; }
        public string Gender { get; set; }
        public DateTime CreateAt { get; set; }
        public int Role { get; set; }

    }
}
