using UnityEngine;
using Steamworks;

public class SteamManager : MonoBehaviour
{
    private static SteamManager _instance;
    public static SteamManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject steamManagerObj = new GameObject("SteamManager");
                _instance = steamManagerObj.AddComponent<SteamManager>();
                DontDestroyOnLoad(steamManagerObj);
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        InitializeSteam();
    }

    private void InitializeSteam()
    {
        if (!SteamAPI.Init())
        {
            Debug.LogError("SteamAPI could not be initialized!");
            return;
        }
        Debug.Log("SteamAPI initialized successfully.");
    }

    private void Update()
    {
        SteamAPI.RunCallbacks();
    }

    private void OnDestroy()
    {
        SteamAPI.Shutdown();
        Debug.Log("SteamAPI shutdown executed.");
    }
}