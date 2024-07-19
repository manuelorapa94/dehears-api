using dehearsWebApi.Model.Auth;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using System.Text;
using dehearsWebApi.Entity;
using Microsoft.AspNetCore.Identity;
using dehearsWebApi.Helpers;

namespace dehearsWebApi.Services.Auth
{
    public class UserServices(DataContext dataContext)
    {
        private readonly DataContext _dataContext = dataContext;

        internal async Task<object> CreateUserAsync(CreateUserModel model)
        {
            if (model == null)
                return new { Message = "Invalid create", IsInValid = true };

            if (await CheckUsernameExistAsync(model.UserName))
                return new { Message = "Username already exists", IsExist = true };

            if (await CheckEmailExistAsync(model.Email))
                return new { Message = "Email already exists", IsExist = true };

            var pass = CheckPasswordStrength(model.Password);
            if (!string.IsNullOrEmpty(pass))
                return new { Message = pass.ToString() };

            // Generate GUID for UserId
            var userId = GenerateCustomGuid();

            var userintitydata = new UserEntity
            {
                UserId = userId,
                Firstname = model.Firstname,
                Lastname = model.Lastname,
                Middlename = model.Middlename,
                FacilityCode = model.FacilityCode,
                Address = model.Address,
                Barangay = model.Barangay,
                Municipality = model.Municipality,
                Province = model.Province,
                UserName = model.UserName,
                Password = PasswordHasher.HashPassword(model.Password),
                UserRole = model.UserRole,
                Email = model.Email,
                ContactNo = model.ContactNo,
                CreatedAt = DateTime.Now,
            };

            _dataContext.Users.Add(userintitydata);
            await _dataContext.SaveChangesAsync();

            return new { Message = "User created successfully", data = userintitydata };
        }

        private string GenerateCustomGuid()
        {
            // Generate a standard GUID
            var guid = Guid.NewGuid();

            // Convert to string and remove hyphens
            var guidString = guid.ToString("N");

            // Generate random values for custom parts
            var randomPart1 = RandomString(5);
            var randomPart2 = RandomString(3);
            var randomPart3 = RandomString(2);

            var formattedGuid = guidString.Insert(3, randomPart1).Insert(9, randomPart2).Insert(14, randomPart3);

            return formattedGuid;
        }

        private string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        internal async Task<object> UpdateUserAsync(UpdateUserModel model)
        {
            var result = await _dataContext.Users.FindAsync(model.UserId);

            if (result == null)
                return new { Message = "Invalid update", IsInValid = true };

            if (await CheckUsernameExistAsync(model.UserName))
                return new { Message = "Username already exists", IsExist = true };

            if (await CheckEmailExistAsync(model.Email))
                return new { Message = "Email already exists", IsExist = true };

            result.Firstname = model.Firstname;
            result.Lastname = model.Lastname;
            result.Middlename = model.Middlename;
            result.Address = model.Address;
            result.Barangay = model.Barangay;
            result.Municipality = model.Municipality;
            result.Province = model.Province;
            result.UserName = model.UserName;
            result.Email = model.Email;
            result.ContactNo = model.ContactNo;
            result.UpdatedAt = DateTime.Now;

            await _dataContext.SaveChangesAsync();

            return new { Message = "User update successfully", data = result };
        }

        internal async Task<object> DeleteUserAsync(string UserId)
        {
            var result = await _dataContext.Users.FindAsync(UserId);
            if (result != null)
            {
                _dataContext.Remove(result);
                return new { Message = "User remove successfully", data = result };
            }

            return new { Message = "Invalid delte", IsInValid = true };
        }

        private Task<bool> CheckUsernameExistAsync(string UserName)
            => _dataContext.Users.AnyAsync(u => u.UserName == UserName);

        private Task<bool> CheckEmailExistAsync(string Email)
            => _dataContext.Users.AnyAsync(u => u.Email == Email);

        private static string CheckPasswordStrength(string password)
        {
            StringBuilder sb = new StringBuilder();
            if (password.Length < 8)
                sb.Append("Minimum password length should be 8 " + Environment.NewLine);

            if (!(Regex.IsMatch(password, "[a-z]") && Regex.IsMatch(password, "[A-Z]") && Regex.IsMatch(password, "\\d")))
                sb.Append("Password should be Alphanumeric " + Environment.NewLine);

            if (!Regex.IsMatch(password, @"[<>,@!#$%^&*()_+\[\]{}?:;|'\\.,/~`=]"))
                sb.Append("Password should contain special character " + Environment.NewLine);

            return sb.ToString();
        }
    }
}
