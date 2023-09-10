using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Plugins.Responsive_UI.Source
{
    public static class Utils
    {
        public static async Task<T> RetryEconomyFunction<T>(Func<Task<T>> functionToRetry, int retryAfterSeconds)
        {
            if (retryAfterSeconds > 60)
            {
                Debug.Log("Economy returned a rate limit exception with an extended Retry After time " +
                          $"of {retryAfterSeconds} seconds. Suggest manually retrying at a later time.");
                return default;
            }

            try
            {
                using var cancellationTokenHelper = new CancellationTokenHelper();
                var cancellationToken = cancellationTokenHelper.CancellationToken;

                await Task.Delay(retryAfterSeconds * 1000, cancellationToken);
                
                var result = await functionToRetry();

                return cancellationToken.IsCancellationRequested ? default : result;
            }
            catch (OperationCanceledException)
            {
                return default;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }

            return default;
        }
    }
}
