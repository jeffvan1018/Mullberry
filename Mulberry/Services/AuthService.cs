using System;
using System.Threading.Tasks;
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
    }
}
