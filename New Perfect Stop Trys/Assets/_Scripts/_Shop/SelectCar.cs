using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SelectCar : MonoBehaviour {

	public Sprite check;
	public Text carName;
	// Use this for initialization
	void OnMouseUp () {
		PlayerPrefs.SetString ("Current car", carName.text);
		gameObject.transform.GetChild (0).GetComponent<Image> ().sprite = check;
	
	}

}
