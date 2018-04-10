using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Coins : MonoBehaviour {

    public Text coinText;
	// Use this for initialization
	void Start () {
        coinText.text = CloudVariables.Coins.ToString ();
	}
	

}
