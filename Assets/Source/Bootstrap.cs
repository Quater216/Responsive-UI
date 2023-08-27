using System;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

namespace Source
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

                await EconomyManager.Instance.RefreshInventory();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}
