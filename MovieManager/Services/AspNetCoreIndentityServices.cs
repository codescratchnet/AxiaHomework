using MovieManager.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieManager.Services
{
    public class AspNetCoreIndentityServices : IAspNetCoreIdentityServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AspNetCoreIndentityServices(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signinManager)
        {
            this._userManager = userManager;
            this._signInManager = signinManager;
        }

        public async Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure)
        {
            return await _signInManager.PasswordSignInAsync(userName, password, isPersistent, lockoutOnFailure);
        }


    }
}
