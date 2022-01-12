using BookStore.API.DTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.API.Repository
{
    public interface IAccountRepository
    {
        public Task<IdentityResult> RegisterAsync(RegisterDto registerDto);
        public Task<string> LoginAsync(Login login);

    }
}
