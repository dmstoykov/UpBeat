﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using UpBeat.Auth.Models;

namespace UpBeat.Auth.Contracts
{
    public interface ISignInService : IDisposable
    {
        Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout);

        Task<bool> HasBeenVerifiedAsync();

        Task SignInAsync(ApplicationUser user, bool isPersistent, bool rememberBrowser);

        Task<SignInStatus> TwoFactorSignInAsync(string provider, string code, bool isPersistent, bool rememberBrowser);

        Task<string> GetVerifiedUserIdAsync();

        Task<bool> SendTwoFactorCodeAsync(string provider);

        Task<SignInStatus> ExternalSignInAsync(ExternalLoginInfo loginInfo, bool isPersistent);
    }
}
