using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class LoginGoogle : MonoBehaviour {

    public static LoginGoogle Instance { get; private set; }

    public Text coinsText;

    const string SAVE_NAME = "Score";
    const string SAVE_NAME2 = "Coins";
    bool isSaving;
    bool isCloudDataLoaded = false;
    bool isSaving2;
    bool isCloudDataLoaded2 = false;

    // Use this for initialization
    void Start() {

        Instance = this;

        if (!PlayerPrefs.HasKey(SAVE_NAME))
            PlayerPrefs.SetString(SAVE_NAME, "0");

        if (!PlayerPrefs.HasKey(SAVE_NAME2))
            PlayerPrefs.SetString(SAVE_NAME2, "0");

        if (!PlayerPrefs.HasKey("IsFirstTime"))
            PlayerPrefs.SetInt("IsFirstTime", 1);

        LoadLocal();
        Debug.Log("local HS:"+ PlayerPrefs.GetString(SAVE_NAME));
        Debug.Log("cloud HS:"+ CloudVariables.HighScore.ToString());
        LoadLocal2();
        Debug.Log("local coins:" + PlayerPrefs.GetString(SAVE_NAME2));
        Debug.Log("cloud coins:" + CloudVariables.Coins.ToString());

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
            LoadData2();
            Debug.Log("local coins:" + PlayerPrefs.GetString(SAVE_NAME2));
            Debug.Log("cloud coins:" + CloudVariables.Coins.ToString());
        }
        else
        {
            Debug.Log("Not signed in");
            LoadData();
            LoadData2();
            Debug.Log("local coins:" + PlayerPrefs.GetString(SAVE_NAME2));
            Debug.Log("cloud coins:" + CloudVariables.Coins.ToString());

        }
    }

    public void ShowAchievements()
    {
        ((PlayGamesPlatform)Social.Active).ShowAchievementsUI();
    }

    public void ShowLeaderboard()
    {
        PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_top_parked_cars);
    }

    public void WriteResult(int score)
    {
        if (Social.localUser.authenticated)
        {

            Social.ReportScore(score,
                GPGSIds.leaderboard_top_parked_cars,
                (bool success) =>
                {
                    Debug.Log("Score Reported");
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
            ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithManualConflictResolution(SAVE_NAME, DataSource.ReadNetworkOnly, true, ResolveConflict, OnSavedGameOpened);
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
            ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithManualConflictResolution(SAVE_NAME, DataSource.ReadNetworkOnly, true, ResolveConflict, OnSavedGameOpened);
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

            if (cloudDataString.EndsWith("?"))
                cloudDataString = "0";

            string localDataString = PlayerPrefs.GetString(SAVE_NAME);

            Debug.Log("cloudSTR="+cloudDataString+" , localSTR="+localDataString);

            StringToGameData(cloudDataString, localDataString);
        }
    }

    private void OnSavedGameDataWritten(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            Debug.Log("Saved HighScore!");
        }
        else
        {
            Debug.Log("Fail!");
        }
    }

    #endregion /Saved Games
    #region Saved Games2
    string GameDataToString2()
    {
        return CloudVariables.Coins.ToString();
    }

    void StringToGameData2(string cloudData, string localData)
    {
        if (PlayerPrefs.GetInt("IsFirstTime") == 1)
        {
            PlayerPrefs.SetInt("IsFirstTime", 0);
            if (int.Parse(cloudData) > int.Parse(localData))
            {
                PlayerPrefs.SetString(SAVE_NAME2, cloudData);
            }
        }
        else
        {
            if (int.Parse(localData) > int.Parse(cloudData))
            {
                CloudVariables.Coins = int.Parse(localData);
                isCloudDataLoaded2 = true;
                SaveData2();
                return;
            }
        }
        CloudVariables.Coins = int.Parse(cloudData);
        isCloudDataLoaded2 = true;
    }

    void StringToGameData2(string localData)
    {
        CloudVariables.Coins = int.Parse(localData);
    }

    public void LoadData2()
    {
        if (Social.localUser.authenticated)
        {
            isSaving2 = false;
            ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithManualConflictResolution(SAVE_NAME2, DataSource.ReadNetworkOnly, true, ResolveConflict2, OnSavedGameOpened2);
        }
        else
        {
            LoadLocal2();
        }
    }

    private void LoadLocal2()
    {
        StringToGameData2(PlayerPrefs.GetString(SAVE_NAME2));
    }

    public void SaveData2()
    {
        if (!isCloudDataLoaded2)
        {
            SaveLocal2();
            return;
        }

        if (Social.localUser.authenticated)
        {
            isSaving2 = true;
            ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithManualConflictResolution(SAVE_NAME2, DataSource.ReadNetworkOnly, true, ResolveConflict2, OnSavedGameOpened2);
        }
        else
        {
            SaveLocal2();
        }
    }

    private void SaveLocal2()
    {
        PlayerPrefs.SetString(SAVE_NAME2, GameDataToString2());
    }

    private void ResolveConflict2(IConflictResolver resolver, ISavedGameMetadata original, byte[] originalData, ISavedGameMetadata unmerged, byte[] unmergedData)
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

    private void OnSavedGameOpened2(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            if (!isSaving2)
                LoadGame2(game);
            else
                SaveGame2(game);

        }
        else
        {
            if (!isSaving2)
                LoadLocal2();
            else
                SaveLocal2();
        }
    }

    private void LoadGame2(ISavedGameMetadata game)
    {
        ((PlayGamesPlatform)Social.Active).SavedGame.ReadBinaryData(game, OnSavedGameDataRead2);
    }

    private void SaveGame2(ISavedGameMetadata game)
    {
        string stringToSave = GameDataToString2();
        SaveLocal2();
        byte[] dataToSave = Encoding.ASCII.GetBytes(stringToSave);

        SavedGameMetadataUpdate update = new SavedGameMetadataUpdate.Builder().Build();

        ((PlayGamesPlatform)Social.Active).SavedGame.CommitUpdate(game, update, dataToSave, OnSavedGameDataWritten2);
    }

    private void OnSavedGameDataRead2(SavedGameRequestStatus status, byte[] savedData)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            string cloudDataString;
            if (savedData.Length == 0)
                cloudDataString = "0";
            else
                cloudDataString = Encoding.ASCII.GetString(savedData);

            if (cloudDataString.EndsWith("?"))
                cloudDataString = "0";

            string localDataString = PlayerPrefs.GetString(SAVE_NAME2);

            Debug.Log("cloudSTR2=" + cloudDataString + " , localSTR2=" + localDataString);

            StringToGameData2(cloudDataString, localDataString);
        }
    }

    private void OnSavedGameDataWritten2(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            Debug.Log("Saved COINS!");
        }
        else
        {
            Debug.Log("Fail!");
        }
    }

    #endregion /Saved Games2

}