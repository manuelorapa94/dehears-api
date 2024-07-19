using dehearsWebApi.Constant;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace dehearsWebApi.Entity
{
    public class UserEntity : IdentityUser
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public required string UserId { get; set; }
        [Required]
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
        public required string UserName { get; set; }
        [Required]
        public required string Password { get; set; }
        [Required]
        public UserRole UserRole { get; set; }
        public string? Email { get; set; }
        public required string ContactNo { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
