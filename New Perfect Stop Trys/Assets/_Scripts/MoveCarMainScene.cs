using UnityEngine;
using System.Collections;

public class MoveCarMainScene : MonoBehaviour {

	private bool play;

	// Update is called once per frame
	void Update () {
		if (Buttons.moveCar) {
			transform.position += new Vector3 (0.3f, 0, 0);
		
			if (PlayerPrefs.GetString ("Music") != "off" && !play) {
				GetComponent<AudioSource> ().Play ();
				play = true;
			}
		
		}
	}
	void OnDestroy(){
		Buttons.moveCar = false;
	}
}
