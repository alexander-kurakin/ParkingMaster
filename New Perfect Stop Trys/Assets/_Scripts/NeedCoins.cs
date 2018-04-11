using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NeedCoins : MonoBehaviour {

	public Text needMore;

	// Use this for initialization
	void Start () {
		if (CloudVariables.Coins >= 100) {
			transform.GetChild (0).gameObject.SetActive (false);
            needMore.text = "You are damn rich! You have " + CloudVariables.Coins.ToString();
        } else {
			transform.GetChild (1).gameObject.SetActive (true);
			transform.GetChild (0).gameObject.SetActive (true);

			needMore.text = "You need " + (100 - CloudVariables.Coins) + " more";
		}
	}
	
}
