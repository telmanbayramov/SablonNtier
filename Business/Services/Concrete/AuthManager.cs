using AutoMapper;
using Business.Services.Abstract;
using Core.Entities.Concrete.Auth;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Entities.DTOs;
using Entities.DTOs.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Business.Services.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        IMapper _mapper;
        private IConfiguration _configuration;
        private readonly TokenOption _tokenOption;

        public AuthManager(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _configuration = configuration;
            _tokenOption = _configuration
                    .GetSection("TokenOptions")
                    .Get<TokenOption>();
        }


        public async Task<IResult> Register(RegisterDto dto)
        {
            var user = _mapper.Map<AppUser>(dto);
            var resultUser = await _userManager.CreateAsync(user, dto.Password);
            if (!resultUser.Succeeded)
            {
                return new ErrorResult("Register olunmadi");
            }
            await _roleManager.CreateAsync(new IdentityRole("User"));
            var resultRole = await _userManager.AddToRoleAsync(user, "User");
            if (!resultRole.Succeeded)
            {
                return new ErrorResult("Role elave olunmadi");
            }
            return new SuccessResult("User registered successfully");
        }
        public async Task<IDataResult<TokenDto>> Login(LoginDto dto)
        {
            AppUser user = await _userManager.FindByNameAsync(dto.UserName);
            if (user is null)
            {
                return new ErrorDataResult<TokenDto>("User not found");
            }
            bool isValidPassword = await _userManager.CheckPasswordAsync(user, dto.Password);
            if (!isValidPassword)
            {
                return new ErrorDataResult<TokenDto>("Invalid password");
            }

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOption.SecurityKey));
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            JwtHeader header = new JwtHeader(signingCredentials);

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim("FullName",user.FullName)
            };
            foreach (var userRole in await _userManager.GetRolesAsync(user))
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            JwtPayload payload = new JwtPayload(audience: _tokenOption.Audience, issuer: _tokenOption.Issuer,
                claims: claims, expires: DateTime.UtcNow.AddMinutes(_tokenOption.AccessTokenExpiration), notBefore: DateTime.UtcNow);
            JwtSecurityToken token = new JwtSecurityToken(header, payload);
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            string jwt = tokenHandler.WriteToken(token);
            var tokenDto = new TokenDto
            {
                Token = jwt
            };
            return new SuccessDataResult<TokenDto>(tokenDto,"token elde edildi");
        }

        public async Task<IResult> AddAdmin(RegisterDto register)
        {
            var admin =_mapper.Map<AppUser>(register);
            var result = await _userManager.CreateAsync(admin, register.Password);
            if(!result.Succeeded)
            {
                return new ErrorResult(result.Errors.ToString());
            }
            await _userManager.AddToRoleAsync(admin, "Admin");
            return new SuccessResult();
        }
    }
}
