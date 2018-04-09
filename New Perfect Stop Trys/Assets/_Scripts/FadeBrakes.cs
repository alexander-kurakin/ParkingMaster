using UnityEngine;
using System.Collections;

public class FadeBrakes : MonoBehaviour {
	
	public Color col;

	void Start(){
		Destroy (gameObject, 2f);
		if (PlayerPrefs.GetString ("Music") != "off") {
			GetComponent<AudioSource> ().Play ();
		}
	}

	// Update is called once per frame
	void Update () {
		GetComponent<Renderer> ().material.color = Color.Lerp (GetComponent<Renderer> ().material.color, col, 1.4f * Time.deltaTime);
	}
}
