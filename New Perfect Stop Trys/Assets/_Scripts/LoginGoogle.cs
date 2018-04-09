using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using UnityEngine.UI;

public class LoginGoogle : MonoBehaviour {


	// Use this for initialization
	void Start () {

        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().EnableSavedGames().Build();
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();

        PlayGamesPlatform.Instance.Authenticate(SignInCallBack, true);

        if (!Social.localUser.authenticated)
            PlayGamesPlatform.Instance.Authenticate(SignInCallBack, false);

        if (Social.localUser.authenticated)
        WriteResult();


    }

    private void SignInCallBack(bool success)
    {
        if (success)
        {
            Debug.Log  ("Signed in as: " + Social.localUser.userName);
        }
        else
        { 
                Debug.Log( "Not signed in");
            
        }
    }

    public void ShowAchievements()
    {
        ((PlayGamesPlatform)Social.Active).ShowAchievementsUI();
    }

    public void ShowLeaderboard()
    {
            ((PlayGamesPlatform)Social.Active).ShowLeaderboardUI(GPGSIds.leaderboard_top_parked_cars);
    }

    public void WriteResult()
    {
        if (Social.localUser.authenticated)
        {
            
            Social.ReportScore(PlayerPrefs.GetInt("Score"),
                GPGSIds.leaderboard_top_parked_cars,
                (bool success) =>
                {
                    Debug.Log ( "Success!" );
                });
        }
    }

}