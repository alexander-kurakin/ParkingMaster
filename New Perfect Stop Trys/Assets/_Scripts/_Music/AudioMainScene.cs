using UnityEngine;
using System.Collections;

public class AudioMainScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (PlayerPrefs.GetString ("Music") != "off")
			GetComponent<AudioSource> ().Play();
	}
	
}
