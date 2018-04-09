using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BestScore : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Text> ().text = "Best: " + PlayerPrefs.GetInt ("Score").ToString ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
