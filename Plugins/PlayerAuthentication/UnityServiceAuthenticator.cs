using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

namespace Unity.Services.Samples
{
    public static class UnityServiceAuthenticator
    {
        public static async Task<bool> InitServices(string profileName = null)
        {
            if (UnityServices.State != ServicesInitializationState.Uninitialized)
                return false;

            if (profileName != null)
            {
                var rgx = new Regex("[^a-zA-Z0-9 -]");
                profileName = rgx.Replace(profileName, "");
                var authProfile = new InitializationOptions().SetProfile(profileName);

                await UnityServices.InitializeAsync(authProfile);
            }
            else
                await UnityServices.InitializeAsync();

            return UnityServices.State == ServicesInitializationState.Initialized;
        }

        public static async Task SignIn(string profileName = null)
        {
            if (!await InitServices(profileName))
                return;

            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
    }
}