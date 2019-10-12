using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BuyCar : MonoBehaviour {

	public GameObject buyCarAudio;
	public GameObject selectBtn;
	public Text coins, carName;
    private int lengthOfCar;

	void OnMouseUp(){
        if (CloudVariables.Coins < 100) {
            Debug.Log("You can't buy this." + "You need to collect " + (100 - CloudVariables.Coins) + " more coins to buy this car.");

            if (PlayerPrefs.GetString("Music") != "off") {
                transform.GetChild(0).GetComponent<AudioSource>().Play();
            }
        } else {

            PlayerPrefs.SetString("Current car", carName.text);
            PlayerPrefs.SetString(carName.text, "Unlocked");

            PlayerPrefs.SetInt("CarLength", SelectCar.getLengthByName(carName.text));

            CloudVariables.Coins -= 100;
            GameObject.Find (carName.text).GetComponent<Animation> ().Play ();
            coins.text = CloudVariables.Coins.ToString();
            //LoginGoogle.Instance.SaveData2();
            if (PlayerPrefs.GetString ("Music") != "off") {
				Instantiate (buyCarAudio, new Vector3 (0, 0, 0), Quaternion.identity);
			}
			selectBtn.SetActive (true);
			gameObject.SetActive (false);
		}
}
}