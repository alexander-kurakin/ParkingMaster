using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class LoginGoogle : MonoBehaviour {

    public static LoginGoogle Instance { get; private set; }

    const string SAVE_NAME = "Score";
    bool isSaving;
    bool isCloudDataLoaded = false;

    // Use this for initialization
    void Start() {

        Instance = this;

        //clearTST
        //CloudVariables.HighScore = 0;
        //PlayerPrefs.SetString(SAVE_NAME, "0");

        if (!PlayerPrefs.HasKey(SAVE_NAME))
            PlayerPrefs.SetString(SAVE_NAME, "0");

        if (!PlayerPrefs.HasKey("IsFirstTime"))
            PlayerPrefs.SetInt("IsFirstTime", 1);

        Debug.Log(int.Parse(PlayerPrefs.GetString(SAVE_NAME)));
        Debug.Log(CloudVariables.HighScore);

        LoadLocal();
        Debug.Log(int.Parse(PlayerPrefs.GetString(SAVE_NAME)));
        Debug.Log(CloudVariables.HighScore);

        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().EnableSavedGames().Build();
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();

        PlayGamesPlatform.Instance.Authenticate(SignInCallBack, true);

        if (!Social.localUser.authenticated)
            PlayGamesPlatform.Instance.Authenticate(SignInCallBack, false);

        if (Social.localUser.authenticated)
            WriteResult(CloudVariables.HighScore);


    }

    private void SignInCallBack(bool success)
    {
        if (success)
        {
            Debug.Log("Signed in as: " + Social.localUser.userName);
            LoadData();
        }
        else
        {
            Debug.Log("Not signed in");

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

    public void WriteResult(int score)
    {
        if (Social.localUser.authenticated)
        {

            Social.ReportScore(score,
                GPGSIds.leaderboard_top_parked_cars,
                (bool success) =>
                {
                    Debug.Log("Success!");
                });
        }
    }

    #region Saved Games
    string GameDataToString() {
        return CloudVariables.HighScore.ToString();
    }

    void StringToGameData(string cloudData, string localData)
    {
        if (PlayerPrefs.GetInt("IsFirstTime") == 1)
        {
            PlayerPrefs.SetInt("IsFirstTime", 0);
            if (int.Parse(cloudData) > int.Parse(localData))
            {
                PlayerPrefs.SetString(SAVE_NAME, cloudData);
            }
        }
        else
        {
            if (int.Parse(localData) > int.Parse(cloudData))
            {
                CloudVariables.HighScore = int.Parse(localData);
                WriteResult(CloudVariables.HighScore);
                isCloudDataLoaded = true;
                SaveData();
                return;
            }
        }
        CloudVariables.HighScore = int.Parse(cloudData);
        isCloudDataLoaded = true;
    }

    void StringToGameData(string localData)
    {
        CloudVariables.HighScore = int.Parse(localData);
    }

    public void LoadData()
    {
        if (Social.localUser.authenticated)
        {
            isSaving = false;
            ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithManualConflictResolution(SAVE_NAME, DataSource.ReadCacheOrNetwork, true, ResolveConflict, OnSavedGameOpened);
        }
        else
        {
            LoadLocal();
        }
    }

    private void LoadLocal()
    {
        StringToGameData(PlayerPrefs.GetString(SAVE_NAME));
    }

    public void SaveData()
    {
        if (!isCloudDataLoaded)
        {
            SaveLocal();
            return;
        }

        if (Social.localUser.authenticated)
        {
            isSaving = true;
            ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithManualConflictResolution(SAVE_NAME, DataSource.ReadCacheOrNetwork, true, ResolveConflict, OnSavedGameOpened);
        }
        else
        {
            SaveLocal();
        }
    }

    private void SaveLocal()
    {
        PlayerPrefs.SetString(SAVE_NAME, GameDataToString());
    }

    private void ResolveConflict(IConflictResolver resolver, ISavedGameMetadata original, byte[] originalData, ISavedGameMetadata unmerged, byte[] unmergedData)
    {
        if (originalData == null)
            resolver.ChooseMetadata(unmerged);
        else if (unmergedData == null)
            resolver.ChooseMetadata(original);
        else
        {
            string originalStr = Encoding.ASCII.GetString(originalData);
            string unmergedStr = Encoding.ASCII.GetString(unmergedData);

            int originalNum = int.Parse(originalStr);
            int unmergedNum = int.Parse(unmergedStr);

            if (originalNum > unmergedNum)
            {
                resolver.ChooseMetadata(original);
                return;
            }
            else if (unmergedNum > originalNum)
            {
                resolver.ChooseMetadata(unmerged);
            }
            resolver.ChooseMetadata(original);
        }
    }

    private void OnSavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            if (!isSaving)
                LoadGame(game);
            else
                SaveGame(game);

        }
        else
        {
            if (!isSaving)
                LoadLocal();
            else
                SaveLocal();
        }
    }

    private void LoadGame(ISavedGameMetadata game)
    {
        ((PlayGamesPlatform)Social.Active).SavedGame.ReadBinaryData(game, OnSavedGameDataRead);
    }

    private void SaveGame(ISavedGameMetadata game)
    {
        string stringToSave = GameDataToString();
        SaveLocal();
        byte[] dataToSave = Encoding.ASCII.GetBytes(stringToSave);

        SavedGameMetadataUpdate update = new SavedGameMetadataUpdate.Builder().Build();

        ((PlayGamesPlatform)Social.Active).SavedGame.CommitUpdate(game, update, dataToSave, OnSavedGameDataWritten);
    }

    private void OnSavedGameDataRead(SavedGameRequestStatus status, byte[] savedData)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            string cloudDataString;
            if (savedData.Length == 0)
                cloudDataString = "0";
            else 
                cloudDataString = Encoding.ASCII.GetString(savedData);

            string localDataString = PlayerPrefs.GetString(SAVE_NAME);

            StringToGameData(cloudDataString, localDataString);
        }
    }

    private void OnSavedGameDataWritten(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            Debug.Log("Success!");
        }
        else
        {
            Debug.Log("Fail!");
        }
    }

    #endregion /Saved Games

}