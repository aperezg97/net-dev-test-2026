using System.ComponentModel;

namespace TaskApp.Entities.Dtos
{
    public class LoginDto
    {
        [DefaultValue("admin")]
        public required string User { get; set; }

        [DefaultValue("Admin!123")]
        public required string Pass { get; set; }
    }
}
