using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SelectCar : MonoBehaviour {

	public Sprite check;
	public Text carName;
    private static int lengthOfCar;
    // Use this for initialization
    void OnMouseUp () {
		PlayerPrefs.SetString ("Current car", carName.text);

        PlayerPrefs.SetInt("CarLength", getLengthByName(carName.text));

        gameObject.transform.GetChild (0).GetComponent<Image> ().sprite = check;
	
	}
    public static int getLengthByName(string in_name)
    {
        if (in_name == "Pizza car")
        {
            lengthOfCar = 5;
        }
        else if (in_name == "Hotdog truck")
        {
            lengthOfCar = 6;
        }
        else if (in_name == "Ambulance")
        {
            lengthOfCar = 7;
        }
        else if (in_name == "Green car")
        {
            lengthOfCar = 5;
        }
        else if (in_name == "Cop")
        {
            lengthOfCar = 5;
        }
        else if (in_name == "Taxi")
        {
            lengthOfCar = 5;
        }
        else if (in_name == "Empty pickup")
        {
            lengthOfCar = 6;
        }
        else if (in_name == "Yellow pickup")
        {
            lengthOfCar = 6;
        }
        else if (in_name == "Blue van")
        {
            lengthOfCar = 6;
        }
        else if (in_name == "ice-cream truck")
        {
            lengthOfCar = 7;
        }
        else if (in_name == "Mail truck")
        {
            lengthOfCar = 7;
        }
        else if (in_name == "Milk truck")
        {
            lengthOfCar = 7;
        }
        else if (in_name == "Blue old car")
        {
            lengthOfCar = 6;
        }
        else if (in_name == "Black old car")
        {
            lengthOfCar = 6;
        }
        else if (in_name == "Green sedan")
        {
            lengthOfCar = 5;
        }
        else if (in_name == "Small 4x4")
        {
            lengthOfCar = 4;
        }
        else if (in_name == "Swat")
        {
            lengthOfCar = 7;
        }
        else if (in_name == "Tow")
        {
            lengthOfCar = 6;
        }
        return lengthOfCar;
    }
}
