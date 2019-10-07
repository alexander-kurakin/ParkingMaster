using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prefs_setter : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        CloudVariables.Coins = int.Parse(PlayerPrefs.GetString("Coins"));
        CloudVariables.HighScore = int.Parse(PlayerPrefs.GetString("Score"));
    }

}
