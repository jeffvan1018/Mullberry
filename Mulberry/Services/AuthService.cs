using System;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using Xamarin.Essentials;

namespace Mulberry.Services
{
    public class AuthService
    {
        private string RedirectUrl
        {
            get
            {
                string result = string.Empty;
                if (DeviceInfo.Platform == DevicePlatform.Android)
                {
                    result = $"msauth://{AppId}/{{SIGNATURE}}";
                }
                else if (DeviceInfo.Platform == DevicePlatform.iOS)
                {
                    result = $"msauth.{AppId}://auth";
                }
                return result;
            }
        }

        public static object ParentWindow { get; set; }

        public AuthService()
        {
            pca = PublicClientApplicationBuilder.Create(ClientID)
                .WithIosKeychainSecurityGroup(AppId)
                .WithRedirectUri(RedirectUrl)
                .WithAuthority("https://logon.microsoftonline.com/common")
                .Build();
        }

        public async Task<bool> SignInAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SignOutAsync()
        {
            throw new NotImplementedException();
        }

        private readonly string AppId = "";
        private readonly string ClientID = "";
        private readonly string[] Scopes = { "" };
        private readonly IPublicClientApplication pca;
    }
}
