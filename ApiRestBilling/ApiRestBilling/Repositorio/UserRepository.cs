using ApiRestBilling.Data;
using ApiRestBilling.Models;
using ApiRestBilling.Models.DTOs;
using ApiRestBilling.Repositorio.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiRestBilling.Repositorio
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private string secretKey;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public UserRepository(ApplicationDbContext context, IConfiguration config, UserManager<AppUser> userManager,
           RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
             _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            secretKey = config.GetValue<string>("ApiSettings:Secreta");
        }

        public async Task<ICollection<UserDTO>> GetAllUsersAsync()
        {
            var users = _context.AppUser.OrderBy(u => u.UserName).ToList();
            var userDtos = new List<UserDTO>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var userDto = _mapper.Map<UserDTO>(user);
                userDto.Roles = roles.ToList();
                userDtos.Add(userDto);
            }

            return userDtos;
        }


        public async Task<UserDTO> GetByIdAsync(string id)
        {
            var user = await _context.AppUser.FindAsync(id);
            if (user == null)
            {
                return null;
            }

            var roles = await _userManager.GetRolesAsync(user);

            var userWithRolesDTO = _mapper.Map<UserDTO>(user);
            userWithRolesDTO.Roles = roles.ToList();

            return userWithRolesDTO;
        }

        public bool IsUnique(string userName)
        {
           var userDb = _context.AppUser.FirstOrDefault(u=>u.UserName == userName);
            if (userDb == null)
            {
               return true;
            }
            return false;
        }

       

        public async Task<UserLoginResponseDTO> LoginAsync(UserLoginDTO userLoginDTO)
        {
            //var user = _context.AppUser.FirstOrDefault(u => u.UserName.ToLower() == userLoginDTO.UserName.ToLower());

            //if (user == null || !await _userManager.CheckPasswordAsync(user, userLoginDTO.Password))
            //{
            //    return new UserLoginResponseDTO { Token = "", User = null };
            //}

            //var roles = await _userManager.GetRolesAsync(user);
            //var manageToken = new JwtSecurityTokenHandler();
            //var key = Encoding.ASCII.GetBytes(secretKey);

            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Subject = new ClaimsIdentity(new[]
            //{
            //    new Claim(ClaimTypes.Name, user.UserName),
            //    // Add each role as a separate claim
            //}.Concat(roles.Select(role => new Claim(ClaimTypes.Role, role)))),
            //            Expires = DateTime.UtcNow.AddDays(1),
            //            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            //        };


            //var token = manageToken.CreateToken(tokenDescriptor);

            //return new UserLoginResponseDTO
            //{
            //    Token = manageToken.WriteToken(token),
            //    User = _mapper.Map<UserDTO>(user)
            //};

            var user = _context.AppUser.FirstOrDefault(u => u.UserName.ToLower() == userLoginDTO.UserName.ToLower());

            if (user == null || !await _userManager.CheckPasswordAsync(user, userLoginDTO.Password))
            {
                return new UserLoginResponseDTO { Token = "", User = null };
            }

            var roles = await _userManager.GetRolesAsync(user);
            var manageToken = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.Name, user.UserName)
        }.Concat(roles.Select(role => new Claim(ClaimTypes.Role, role)))),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = manageToken.CreateToken(tokenDescriptor);
            var userDto = _mapper.Map<UserDTO>(user);
            userDto.Roles = roles.ToList(); // Set the roles in the DTO

            return new UserLoginResponseDTO
            {
                Token = manageToken.WriteToken(token),
                User = userDto
            };
        }


        public async Task<UserDTO> RegisterAsync(UserRegisterDTO userRegisterDTO)
        {
            AppUser user = new AppUser()
            {
                UserName = userRegisterDTO.UserName,
                Email = userRegisterDTO.UserName,
                NormalizedEmail = userRegisterDTO.UserName.ToUpper(),
                FirstName = userRegisterDTO.FirstName.ToLower(),
                LastName = userRegisterDTO.LastName.ToLower()
            };
            var result = await _userManager.CreateAsync(user, userRegisterDTO.Password);

            if (result.Succeeded)
            {
                if (!_roleManager.RoleExistsAsync("admin").GetAwaiter().GetResult())
                {
                    await _roleManager.CreateAsync(new IdentityRole("admin"));
                    await _roleManager.CreateAsync(new IdentityRole("registrado"));
                }

                await _userManager.AddToRoleAsync(user, "registrado");

                var userReturn = _context.AppUser.FirstOrDefault(u=>u.UserName == userRegisterDTO.UserName);
                return _mapper.Map<UserDTO>(userReturn);
            }

            return new UserDTO();
        }
    }
}
