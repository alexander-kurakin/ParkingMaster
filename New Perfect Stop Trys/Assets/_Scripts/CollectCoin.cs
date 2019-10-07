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
            CloudVariables.Coins = CloudVariables.Coins + 1;
            GameObject.Find ("Text coin").GetComponent<Text>().text = CloudVariables.Coins.ToString();
            //LoginGoogle.Instance.SaveData2();
            Destroy (transform.parent.gameObject);

		}
	}

}
