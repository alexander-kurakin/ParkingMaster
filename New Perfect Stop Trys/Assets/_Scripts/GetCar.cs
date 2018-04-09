using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GetCar : MonoBehaviour {
		
	public GameObject cars,btn;
	private GameObject[] lockedCars, allCars;
	private int x;
	private string carName;
	public Text textGet, carsLeft;
	private ArrayList arr = new ArrayList ();

	void Start (){
		FillDataInLoop ();

		carsLeft.text = ("Cars left: "+arr.Count);
	}


	void OnMouseUpAsButton(){

		if (arr.Count == 0) {
			textGet.text = "You have opened all the cars";
			btn.SetActive (false);
			carsLeft.GetComponent<Text>().enabled= false;

		}
		else {
			carName = lockedCars [Random.Range (0, lockedCars.Length)].name;
			textGet.text = "You have unlocked " + carName;
			PlayerPrefs.SetString (carName, "Unlocked");
			PlayerPrefs.SetString ("Current Car", carName);
			StartCoroutine (loadScene ("game"));
		}
}

	IEnumerator loadScene(string scene)
	{
		float fadeTime = Camera.main.GetComponent<Fading>().BeginFade(1);
		yield return new WaitForSeconds(fadeTime);
		SceneManager.LoadScene(scene);
	}

	void FillDataInLoop(){
		lockedCars = new GameObject[cars.GetComponent<GameCntrlr> ().car.Length];
		allCars = new GameObject[cars.GetComponent<GameCntrlr> ().car.Length];

		allCars = cars.GetComponent<GameCntrlr> ().car;
		x = 0;
		for (int i = 0; i < allCars.Length; i++) {
			if (PlayerPrefs.GetString (allCars [i].name) == "Unlocked") {
				x++;
				carName = PlayerPrefs.GetString ("Current car");
			} else {
				lockedCars [x] = allCars [i];
				arr.Add (lockedCars [i]);
				x++;
			}

		}

	}

}
