using ApiRestBilling.Models;
using ApiRestBilling.Models.DTOs;
using AutoMapper;

namespace ApiRestBilling.Mappers
{
    public class BillingMapper : Profile
    {
        public BillingMapper()
        {
            CreateMap<AppUser, UserDTO>().ReverseMap();
            CreateMap<UserRegisterDTO, AppUser>(); // For registration
            // No direct mapping needed for UserLoginDTO to AppUser, as it's typically used just for authentication
            CreateMap<UserLoginResponseDTO, UserDTO>(); // Assuming UserLoginResponseDTO contains user data and token
        }


    }
    
}
