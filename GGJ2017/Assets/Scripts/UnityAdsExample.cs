using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAdsExample : MonoBehaviour
{
    [SerializeField]
    private string
        androidGameId = "18658",
        iosGameId = "18660";

    [SerializeField]
    private bool testMode;

    void Start()
    {
        string gameId = null;

#if UNITY_ANDROID
        gameId = androidGameId;
#elif UNITY_IOS
        gameId = iosGameId;
#endif

        if (string.IsNullOrEmpty(gameId))
        { // Make sure the Game ID is set.
            Debug.LogError("Failed to initialize Unity Ads. Game ID is null or empty.");
        }
        else if (!Advertisement.isSupported)
        {
            Debug.LogWarning("Unable to initialize Unity Ads. Platform not supported.");
        }
        else if (Advertisement.isInitialized)
        {
            Debug.Log("Unity Ads is already initialized.");
        }
        else
        {
            Debug.Log(string.Format("Initialize Unity Ads using Game ID {0} with Test Mode {1}.",
                gameId, testMode ? "enabled" : "disabled"));
            Advertisement.Initialize(gameId, testMode);
        }
    }
}