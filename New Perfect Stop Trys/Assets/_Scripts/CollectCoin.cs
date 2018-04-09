using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CollectCoin : MonoBehaviour {
	public GameObject collectCoin;
	void OnTriggerEnter (Collider other){
		if (other.tag == "Car") {
			if (PlayerPrefs.GetString ("Music") != "off") {
				Instantiate (collectCoin, new Vector3 (0, 0, 0), Quaternion.identity);
			}
			PlayerPrefs.SetInt ("Coins", PlayerPrefs.GetInt ("Coins") + 1);
			GameObject.Find ("Text coin").GetComponent<Text>().text = PlayerPrefs.GetInt ("Coins").ToString();
			Destroy (transform.parent.gameObject);

		}
	}

}
