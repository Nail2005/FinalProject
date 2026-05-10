using Business.Abstract;
using Business.DTOs.AuthDto;
using DAL.Abstract;
using Entity.Entities;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;  
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;       
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DAL.Concrete
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessageService _messageService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration; 
        public AuthService(IUnitOfWork unitOfWork, IMessageService messageService, UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _messageService = messageService;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task CreateParentStudentAsync(CreateParentStudentDto dto)
        {
            var user = new AppUser
            {
                UserName = dto.PhoneNumber,
                PhoneNumber = dto.PhoneNumber,
                FullName = dto.ParentName,
                IsActive = false
            };

            var createResult = await _userManager.CreateAsync(user);

            if (!createResult.Succeeded)
            {
                throw new Exception(string.Join(" | ", createResult.Errors.Select(x => x.Description)));
            }

            var roleResult = await _userManager.AddToRoleAsync(user, "Parent");

            if (!roleResult.Succeeded)
            {
                throw new Exception(string.Join(" | ", roleResult.Errors.Select(x => x.Description)));
            }

            var parent = new Parent
            {
                UserId = user.Id,
                PhoneNumber = dto.PhoneNumber,
                FullName = dto.ParentName
            };

            await _unitOfWork.Parents.AddAsync(parent);
            await _unitOfWork.SaveAsync();   

            var student = new Student
            {
                FullName = dto.StudentName,
                ParentId = parent.Id          
            };

            await _unitOfWork.Students.AddAsync(student);
            await _unitOfWork.SaveAsync();   

            await GenerateOtpAsync(dto.PhoneNumber);
        }

        public async Task<string> GenerateOtpAsync(string phone)
        {
            var code = new Random().Next(100000, 999999).ToString();

            var otp = new OtpCode
            {
                PhoneNumber = phone,
                Code = code,
                ExpirationTime = DateTime.UtcNow.AddMinutes(2)
            };

            await _unitOfWork.OtpCodes.AddAsync(otp);
            await _unitOfWork.SaveAsync();
            await _messageService.SendOtpAsync(phone, $"OTP code: {code}");
            return code;

        }

        public async Task<LoginDto> LoginAsync(string phone)
        {
            var user = await _userManager.Users
        .FirstOrDefaultAsync(x => x.PhoneNumber == phone);

            if (user == null)
                throw new Exception("User tapilmadi");

            if (!user.IsActive)
                throw new Exception("OTP tesdiqlenmeyib");

            var token = await GenerateJwtToken(user);    

            return new LoginDto
            {
                PhoneNumber = user.PhoneNumber,
                Token = token 
            };
        }

        public async Task<bool> VerifyOtpAsync(VerifyOptDto dto)
        {
            var otp = (await _unitOfWork.OtpCodes
        .GetWhereAsync(x => x.PhoneNumber == dto.PhoneNumber && !x.IsUsed))
        .OrderByDescending(x => x.Id)
        .FirstOrDefault();

            if (otp == null || otp.Code != dto.Code || otp.ExpirationTime < DateTime.UtcNow)
            {
                return false;
            }

            otp.IsUsed = true;

            var parent = (await _unitOfWork.Parents
            .GetWhereAsync(x => x.PhoneNumber == dto.PhoneNumber))
            .FirstOrDefault();

            if (parent != null)
            {
                var user = await _userManager.FindByIdAsync(parent.UserId.ToString());
                user.IsActive = true;
                await _userManager.UpdateAsync(user);
            }

            await _unitOfWork.SaveAsync();
            return true;
        }

        private async Task<string> GenerateJwtToken(AppUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            { 
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber)     
            };

            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));    
            }
        
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));  

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token); 

        }
    }
}
