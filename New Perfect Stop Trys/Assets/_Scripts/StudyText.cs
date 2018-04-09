using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StudyText : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		GetComponent<Text> ().color = Color.Lerp (GetComponent<Text> ().color, Color.white, 0.8f * Time.deltaTime);
		if (PlayerLose.lose||PlayerPrefs.GetString ("Study") == "yes")
			gameObject.SetActive (false);
	}
}
