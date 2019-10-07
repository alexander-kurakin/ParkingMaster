using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prefs_setter : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        CloudVariables.Coins = Convert.ToInt32(PlayerPrefs.GetString("Coins","0"));
        CloudVariables.HighScore = Convert.ToInt32(PlayerPrefs.GetString("Score", "0"));
    }

}
