using System.Threading.Tasks;  
using Unity.Services.Leaderboards;  
using Unity.Services.Core;  
using System.Collections.Generic;  

public class SteamLeaderboardManager  
{  
    public async Task Initialize()  
    {  
        await UnityServices.InitializeAsync();  
        // Additional initialization if needed  
    }  

    public async Task<List<LeaderboardEntry>> GetLeaderboard(string leaderboardId, int topN)  
    {  
        var leaderboard = await LeaderboardService.Instance.GetLeaderboardAsync(leaderboardId, topN);  
        return leaderboard.Entries;  
    }  

    public async Task<ScoreEntry> GetUserScore(string leaderboardId, string userId)  
    {  
        var entry = await LeaderboardService.Instance.GetUserScoreAsync(leaderboardId, userId);  
        return entry;  
    }  

    public async Task<List<ScoreEntry>> GetFriendScores(string leaderboardId, string userId)  
    {  
        var scores = await LeaderboardService.Instance.GetFriendScoresAsync(leaderboardId, userId);  
        return scores;  
    }  

    public async Task SubmitScore(string leaderboardId, int score)  
    {  
        await LeaderboardService.Instance.SubmitScoreAsync(leaderboardId, score);  
    }  
}  

public class LeaderboardEntry  
{  
    public string UserId { get; set; }  
    public int Score { get; set; }  
    public string AttachedData { get; set; }  
}  

public class ScoreEntry  
{  
    public string UserId { get; set; }  
    public int Score { get; set; }  
}  
