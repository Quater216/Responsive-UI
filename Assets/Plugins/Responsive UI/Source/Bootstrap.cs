using System;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Economy;
using UnityEngine;

namespace Plugins.Responsive_UI.Source
{
    public class Bootstrap : MonoBehaviour
    {
        private async void Start()
        {
            try
            {
                await UnityServices.InitializeAsync();
                
                if (this == null) 
                    return;

                if (AuthenticationService.Instance.IsSignedIn == false)
                {
                    await AuthenticationService.Instance.SignInAnonymouslyAsync();
                    
                    if (this == null) 
                        return;
                }

                await EconomyService.Instance.Configuration.SyncConfigurationAsync();
                await EconomyManager.Instance.RefreshInventory();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}
