using Core.Utilities.Result.Abstract;
using Core.Utilities.Results.Abstract;
using Entities.DTOs;
using Entities.DTOs.Auth;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Services.Abstract
{
    public interface IAuthService
    {
        public Task<IResult>Register(RegisterDto register);
        public Task <IDataResult<TokenDto>>Login(LoginDto login);
        public Task<IResult> AddAdmin(RegisterDto register);
    }
}
