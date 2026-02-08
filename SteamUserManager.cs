using System;  
using System.Net.Http;  
using System.Threading.Tasks;  
using UnityEngine;  

public class SteamUserManager : MonoBehaviour  
{  
    private static readonly HttpClient client = new HttpClient();  
    private string steamApiKey = "YOUR_API_KEY"; // Add your API key here  
    private string steamId;  
    private string userName;  
    private string avatarUrl;  
    private int steamLevel;  
    private string[] dlcList;  

    public SteamUserManager(string steamId)  
    {  
        this.steamId = steamId;  
    }  

    public async Task GetUserInformationAsync()  
    {  
        string url = $"https://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key={steamApiKey}&steamids={steamId}";  
        var response = await client.GetStringAsync(url);  
        ParseUserInformation(response);  
    }  

    private void ParseUserInformation(string jsonResponse)  
    {  
        // Parse the JSON response to extract user information  
        // Here you would deserialize the JSON and assign values to userName, avatarUrl, etc.  
    }  

    public async Task DownloadAvatarAsync()  
    {  
        if (!string.IsNullOrEmpty(avatarUrl))  
        {  
            var imageResponse = await client.GetByteArrayAsync(avatarUrl);  
            // Convert byte array to texture and use in your game  
        }  
    }  

    public async Task GetSteamLevelAsync()  
    {  
        string url = $"https://api.steampowered.com/IPlayerService/GetSteamLevel/v1/?key={steamApiKey}&steamid={steamId}";  
        var response = await client.GetStringAsync(url);  
        steamLevel = ParseSteamLevel(response);  
    }  

    private int ParseSteamLevel(string jsonResponse)  
    {  
        // Parse JSON to extract steam level  
        return steamLevel;  
    }  

    public async Task GetDlcInformationAsync()  
    {  
        string url = $"https://api.steampowered.com/IPlayerService/GetOwnedGames/v1/?key={steamApiKey}&steamid={steamId}&include_appinfo=true&include_free_games=true";  
        var response = await client.GetStringAsync(url);  
        dlcList = ParseDlcInformation(response);  
    }  

    private string[] ParseDlcInformation(string jsonResponse)  
    {  
        // Parse the JSON to extract DLC information  
        return dlcList;  
    }  
}  
