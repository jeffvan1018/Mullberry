using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FreshMvvm;
using MvvmHelpers;
using MvvmHelpers.Commands;

namespace Mulberry.PageModels
{
    public class MainPageModel : FreshBasePageModel
    {
        public AsyncCommand SignInCommand { get; set; }

        public AsyncCommand SignOutCommand { get; set; }
        
        public MainPageModel()
        {
            authService = new object();

            SignInCommand = new AsyncCommand(SignInAsync);
            SignOutCommand = new AsyncCommand(SignOutAsync);
        }

        public async Task SignInAsync()
        {
            await Task.Delay(500);
        }

        public async Task SignOutAsync()
        {
            await Task.Delay(500);
        }

        private readonly object authService;
    }
}
