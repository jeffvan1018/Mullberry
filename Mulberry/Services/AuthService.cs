using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using Xamarin.Essentials;

namespace Mulberry.Services
{
    public partial class AuthService
    {
        private string RedirectUrl
        {
            get
            {
                string result = string.Empty;
                if (DeviceInfo.Platform == DevicePlatform.Android)
                {
                    result = ANDROID_APP_REDIRECT;
                }
                else if (DeviceInfo.Platform == DevicePlatform.iOS)
                {
                    result = IOS_APP_REDIRECT;
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
            bool result = false;
            try
            {
                var accounts = await pca.GetAccountsAsync();
                var firstAccount = accounts.FirstOrDefault();
                var authResult = await pca.AcquireTokenSilent(Scopes, firstAccount).ExecuteAsync();

                await SecureStorage.SetAsync("AccessToken", authResult?.AccessToken);
                result = true;
            }
            catch(MsalUiRequiredException)
            {
                try
                {
                    var authResult = await pca.AcquireTokenInteractive(Scopes)
                        .WithParentActivityOrWindow(ParentWindow)
                        .WithUseEmbeddedWebView(true)
                        .ExecuteAsync();

                    await SecureStorage.SetAsync("AccessToken", authResult?.AccessToken);
                    result = true;
                }
                catch(Exception interactiveEx)
                {
                    Debug.WriteLine(interactiveEx.ToString());
                }
            }
            catch(Exception silentEx)
            {
                Debug.WriteLine(silentEx.ToString());
            }

            return result;
        }

        public async Task<bool> SignOutAsync()
        {
            bool result = false;

            try
            {
                var accounts = await pca.GetAccountsAsync();
                while (accounts.Any())
                {
                    await pca.RemoveAsync(accounts.FirstOrDefault());
                    accounts = await pca.GetAccountsAsync();
                }

                SecureStorage.Remove("AccessToken");
                result = true;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            return result;
        }

        private readonly string AppId = "";
        private readonly string ClientID = "";
        private readonly string[] Scopes = { "" };
        private readonly IPublicClientApplication pca;
    }
}
