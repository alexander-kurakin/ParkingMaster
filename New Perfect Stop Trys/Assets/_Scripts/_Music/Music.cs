using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Music : MonoBehaviour {

	public GameObject audioMain;
	public Sprite music_on, music_off;

	void Start(){
		if (PlayerPrefs.GetString ("Music") == "off") {
			transform.GetChild (0).gameObject.GetComponent<Image> ().sprite = music_off;
		} else {
			audioMain.GetComponent<AudioSource> ().Play ();
		}
	}


	// Update is called once per frame
	void OnMouseUpAsButton () {
		if (PlayerPrefs.GetString ("Music") == "off") {
			transform.GetChild (0).gameObject.GetComponent<Image> ().sprite = music_on;
			PlayerPrefs.SetString ("Music", "on");
			audioMain.GetComponent<AudioSource> ().Play ();
		}
		 else {
			transform.GetChild (0).gameObject.GetComponent<Image> ().sprite = music_off;
			PlayerPrefs.SetString ("Music", "off");
			audioMain.GetComponent<AudioSource> ().Pause ();
		}
	
	}
}
