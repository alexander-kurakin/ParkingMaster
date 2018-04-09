using UnityEngine;
using System.Collections;

public class SetCarAtStart : MonoBehaviour {

	public GameObject[] cars;

	// Use this for initialization
	void Awake () {
		if (PlayerPrefs.GetString("Current car") ==""){
			PlayerPrefs.SetString ("Current car", "Pizza car");
			PlayerPrefs.SetString ("Pizza car", "Unlocked");
														}
		}

	void Start(){
		for (int i = 0; i < cars.Length; i++) {
		if (cars[i].name == PlayerPrefs.GetString ("Current car")){
			cars[i].SetActive (true);
			break;
		}
		}
	}
}
	
