using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FreshMvvm;
using MvvmHelpers.Commands;
using Mulberry.Services;

namespace Mulberry.PageModels
{
    public class MainPageModel : FreshBasePageModel
    {
        public bool IsSignedIn { get; set; }

        public bool IsSigningIn { get; set; }

        public string Name { get; set; }

        public AsyncCommand SignInCommand { get; set; }

        public AsyncCommand SignOutCommand { get; set; }
        
        public MainPageModel()
        {
            authService = new AuthService();
            simpleGraphService = new SimpleGraphService();

            SignInCommand = new AsyncCommand(SignInAsync);
            SignOutCommand = new AsyncCommand(SignOutAsync);
        }

        public async Task SignInAsync()
        {
            IsSigningIn = true;

            if (await authService.SignInAsync())
            {
                Name = await simpleGraphService.GetNameAsync();
                IsSignedIn = true;
            }

            IsSigningIn = false;
        }

        public async Task SignOutAsync()
        {
            if (await authService.SignOutAsync())
            {
                IsSignedIn = false;
            }
        }

        private readonly AuthService authService;
        private readonly SimpleGraphService simpleGraphService;
    }
}
