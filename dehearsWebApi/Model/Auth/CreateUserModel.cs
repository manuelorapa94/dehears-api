using dehearsWebApi.Constant;
using System.ComponentModel.DataAnnotations;

namespace dehearsWebApi.Model.Auth
{
    public class CreateUserModel
    {
        public required string Firstname { get; set; }
        [Required]
        public required string Lastname { get; set; }
        [Required]
        public string? Middlename { get; set; }
        public required string FacilityCode { get; set; }
        [Required]
        public string? Address { get; set; }
        [Required]
        public int Barangay { get; set; }
        [Required]
        public int Municipality { get; set; }
        [Required]
        public int Province { get; set; }
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public required string Password { get; set; }
        [Required]
        public UserRole UserRole { get; set; }
        public string Email { get; set; }
        public required string ContactNo { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
