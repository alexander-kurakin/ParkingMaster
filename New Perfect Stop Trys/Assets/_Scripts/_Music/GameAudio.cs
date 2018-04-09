using UnityEngine;
using System.Collections;

public class GameAudio : MonoBehaviour {

	public AudioClip slow, middle, fast;
	void Start () {
		if (PlayerPrefs.GetString ("Music") != "off")
			GetComponent<AudioSource> ().clip = slow;
			GetComponent<AudioSource> ().Play();
	}

	void Update () {
		if (PlayerPrefs.GetString ("Music") != "off") {
			GetComponent<AudioSource> ().clip = slow;
			GetComponent<AudioSource> ().Play ();
		}
		if (GameCntrlr.stop)
			GetComponent<AudioSource> ().Pause();
	}
}
