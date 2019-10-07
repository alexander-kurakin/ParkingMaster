using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class prefs_saver : MonoBehaviour
{
    void Awake()
    {
        SceneManager.sceneUnloaded += MyFunction;
    }

    void MyFunction<Scene>(Scene scene)
    {
        PlayerPrefs.SetString("Coins", CloudVariables.Coins.ToString());
        PlayerPrefs.SetString("Score", CloudVariables.HighScore.ToString());
    }
}