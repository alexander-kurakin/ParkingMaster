using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NowCar : MonoBehaviour {

	public Sprite thisOne, selectCar;
	public Text carName;
	public GameObject openCar, buyCar;
	public static bool inCollision;

	private bool firstCar;

	void OnTriggerEnter (Collider other){

		print ("Music " + PlayerPrefs.GetString ("Music"));
		if (other.tag == "Car") {

			if (PlayerPrefs.GetString ("Music") != "off"&&firstCar)
				GetComponent<AudioSource> ().Play();

			firstCar = true;

			other.transform.localScale += new Vector3 (0.2f, 0.2f, 0.2f);
			carName.text = other.name;
			if (PlayerPrefs.GetString (other.name) == "Unlocked") {
				openCar.SetActive (true);
				buyCar.SetActive (false);
				if (PlayerPrefs.GetString ("Current car") == other.name) {
					openCar.transform.GetChild (0).GetComponentInChildren<Image> ().sprite = thisOne;
				} else {
					openCar.transform.GetChild (0).GetComponentInChildren<Image> ().sprite = selectCar;
				}
			}else{
				buyCar.SetActive (true);
				openCar.SetActive (false);
		}

	}
}
	void OnTriggerExit (Collider other){
		if (other.tag == "Car") {
			other.transform.localScale -= new Vector3 (0.2f, 0.2f, 0.2f);
		}
	}

}
