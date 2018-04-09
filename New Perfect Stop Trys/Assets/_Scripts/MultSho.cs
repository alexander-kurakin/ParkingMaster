using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultSho : MonoBehaviour {

    public Text texter;
    public Text texter2;
    private int transSpeed;
	// Use this for initialization
	void Start () {

        texter.text = "x" + GameCntrlr.multiplier.ToString();
        texter2.text = "Speed: 0 km/h" ;
    }
	
	// Update is called once per frame
	void Update () {
        switch (GameCntrlr.multiplier)
        {
            case 1:
                texter.color = Color.white;
                break;
            case 2:
                texter.color = Color.green;
                break;
            case 4:
                texter.color = Color.yellow;
                break;
            case 8:
                texter.color = Color.magenta;
                break;
            case 16:
                texter.color = Color.red;
                break;
        }
        
        texter.text = "x" + GameCntrlr.multiplier.ToString();
        if (MoveObjects.speed >= 20f && MoveObjects.speed < 25.01f)
            transSpeed = 55;
        if (MoveObjects.speed >= 25f && MoveObjects.speed < 26.01f)
            transSpeed = 60;
        if (MoveObjects.speed >= 26f && MoveObjects.speed < 27.01f)
            transSpeed = 68;
        if (MoveObjects.speed >= 27f && MoveObjects.speed < 28.01f)
            transSpeed = 76;
        if (MoveObjects.speed >= 28f && MoveObjects.speed < 29.01f)
            transSpeed = 84;
        if (MoveObjects.speed >= 29f && MoveObjects.speed < 30.01f)
            transSpeed = 92;
        if (MoveObjects.speed >= 30f && MoveObjects.speed < 31.01f)
            transSpeed = 100;
        if (MoveObjects.speed >= 31f && MoveObjects.speed < 32.01f)
            transSpeed = 108;
        if (MoveObjects.speed >= 32f && MoveObjects.speed < 33.01f)
            transSpeed = 116;
        if (MoveObjects.speed >= 33f && MoveObjects.speed < 34.01f)
            transSpeed = 124;
        if (MoveObjects.speed >= 34f && MoveObjects.speed < 35.01f)
            transSpeed = 132;
        if (MoveObjects.speed >= 35f && MoveObjects.speed < 36.01f)
            transSpeed = 140;
        if (MoveObjects.speed >= 36f && MoveObjects.speed < 37.01f)
            transSpeed = 148;
        if (MoveObjects.speed >= 37f && MoveObjects.speed < 38.01f)
            transSpeed = 156;
        if (MoveObjects.speed >= 38f && MoveObjects.speed < 39.01f)
            transSpeed = 164;
        if (MoveObjects.speed >= 39f && MoveObjects.speed < 40.01f)
            transSpeed = 172;
        if (MoveObjects.speed >= 40f && MoveObjects.speed < 41.01f)
            transSpeed = 180;
        if (MoveObjects.speed >= 41f && MoveObjects.speed < 42.01f)
            transSpeed = 188;
        if (MoveObjects.speed >= 42f && MoveObjects.speed < 43.01f)
            transSpeed = 196;
        if (MoveObjects.speed >= 43f && MoveObjects.speed < 44.01f)
            transSpeed = 204;
        texter2.text = "Speed:" +transSpeed+ " km/h";
    }
}
