using KlickitTask.Data;
using KlickitTask.DTO;
using KlickitTask.Healpers;
using KlickitTask.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KlickitTask.Services
{
    public class AuthService : IAuthService
    {
        public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
            IOptions<JWT> jwt, KlickitTaskEnteties klickitTaskEnteties)
        {
            _userManager = userManager;
            _jwt = jwt.Value;
            _klickitTaskEnteties = klickitTaskEnteties;
        }
        KlickitTaskEnteties _klickitTaskEnteties; //=new CoursatOnlineDbContext();
        private readonly UserManager<ApplicationUser> _userManager;//
        private readonly JWT _jwt;

        //register student
        public async Task<AuthModel> RegisterAsync(RegisterModel model, UserType role)
        {
            if (await _userManager.FindByEmailAsync(model.Email) is not null)
                return new AuthModel { Message = "Email is already registered!" };

            if (await _userManager.FindByNameAsync(model.UserName) is not null)
                return new AuthModel { Message = "Username is already registered!" };

            var user = new ApplicationUser
            {

                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Country = model.Country,
                Governate = model.Governate,
                City = model.City,
                UserName = model.UserName,

            };
          
        var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var errors = string.Empty;

                foreach (var error in result.Errors)
                    errors += $"{error.Description},";

                return new AuthModel { Message = errors };
            }
            await _userManager.AddToRoleAsync(user, role.ToString());

            var jwtSecurityToken = await CreateJwtToken(user);


            await _userManager.UpdateAsync(user);


            //admin 0
            if (role == UserType.Admin)
            {
                _klickitTaskEnteties.Admins.Add(new Admin
                {

                    Email = model.Email,
                    LastName = model.LastName,
                    FirstName = model.FirstName,
                    Password = model.Password,
                    PhoneNumber = model.PhoneNumber,
                    Country = model.Country,
                    Governate = model.Governate,
                    City = model.City,
                    UserName = model.UserName,


                });
                _klickitTaskEnteties.SaveChanges();

                return new AuthModel
                {
                    
                    Email = user.Email,
                    ExpiresOn = jwtSecurityToken.ValidTo,
                    IsAuthenticated = true,
                    Roles = new List<string> { "Admin" },
                    Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),

                };

            }
            //customer 1
            if (role == UserType.Customer)
            {
                
                _klickitTaskEnteties.Customers.Add(new Customer
                {
                    Email = model.Email,
                    LastName = model.LastName,
                    FirstName = model.FirstName,
                    Password = model.Password,
                    PhoneNumber = model.PhoneNumber,
                    Country = model.Country,
                    Governate = model.Governate,
                    City = model.City,
                    UserName=model.UserName

                });
                _klickitTaskEnteties.SaveChanges();

                return new AuthModel
                {
                    Email = user.Email,
                    ExpiresOn = jwtSecurityToken.ValidTo,
                    IsAuthenticated = true,
                    Roles = new List<string> { "Customer" },
                    Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),

                };

            }

            return null;

        }





        //login
        public async Task<AuthModel> GetTokenAsync(TokenRequestModel model)
        {
            var authModel = new AuthModel();
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                authModel.Message = "Email or Password is incorrect!";
                return authModel;
            }

            var jwtSecurityToken = await CreateJwtToken(user);//
            var rolesList = await _userManager.GetRolesAsync(user);
            User loginuser = _klickitTaskEnteties.Users.FirstOrDefault(u => u.Email == model.Email);
            authModel.IsAuthenticated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);//
            authModel.Email = user.Email;
            authModel.ExpiresOn = jwtSecurityToken.ValidTo;
            authModel.Roles = rolesList.ToList();
            authModel.Id = loginuser.Id;
            return authModel;
        }

        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
                //jwt.claim(name: "role_id").rawValue as? String
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

       
    }
}
