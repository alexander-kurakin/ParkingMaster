using UnityEngine;
using System.Collections;

public class CreateBorder : MonoBehaviour {

	public GameObject borders, blinker, study, coin;
	public static GameObject blink;
    public static bool blink_destroyed;
	public static string side;
	public Color normalColor;
	private GameObject borderRight, borderLeft;
	private int randSpaces, randSide, randCoin;

	void Start()
	{
		randSpaces = Random.Range(10, 15);
		randSide = Random.Range(1, 3);
		randCoin = Random.Range (3, 7);

		if (PlayerPrefs.GetString ("Study") != "yes") {
			randSpaces = 4;
			study.SetActive (true);
		}
		for (int i = 0; i < 14; i++)
		{
			float xPos = borderRight == null ? -16f : borderRight.GetComponent<MeshRenderer>().bounds.size.x + Random.Range(1f, 3f) + borderRight.transform.position.x;
			borderRight = Instantiate(borders, new Vector3(xPos, -0.18f, -3.4f), Quaternion.identity) as GameObject;

			float yPos = borderLeft == null ? -16f : borderLeft.GetComponent<MeshRenderer>().bounds.size.x + Random.Range(1f, 3f) + borderLeft.transform.position.x;
			borderLeft = Instantiate(borders, new Vector3(yPos, -0.18f, -17f), Quaternion.identity) as GameObject;
		}
	}

	void Update()
	{
        if (borderRight.transform.position.x < 80f )
		{
			randSpaces--;
			float rand = randSpaces <= 0 && randSide == 1 ? Random.Range(10f, 15f) : Random.Range(1f, 3f);
			if (randSpaces <= 0 && randSide == 1 &&! GameCntrlr.inTurn) {
				RandomNums(rand, -4.11f, borderRight);
				side = "Right";
            }
			borderRight = Instantiate(borders, new Vector3(borderRight.GetComponent<MeshRenderer>().bounds.size.x + rand + borderRight.transform.position.x, -0.18f, -3.4f), Quaternion.identity) as GameObject;
		}

		if (borderLeft.transform.position.x < 80f )
		{
			float rand = randSpaces <= 0 && randSide == 2 ? Random.Range(10f, 15f) : Random.Range(1f, 3f);
			if (randSpaces <= 0 && randSide == 2 && !GameCntrlr.inTurn){
				RandomNums(rand, -15.85f, borderLeft);
				side = "Left";
            }
			borderLeft = Instantiate(borders, new Vector3(borderLeft.GetComponent<MeshRenderer>().bounds.size.x + rand + borderLeft.transform.position.x, -0.18f, -17f), Quaternion.identity) as GameObject;
		}


	}
	void RandomNums(float rand, float zPos, GameObject border)
	{
		randSpaces = Random.Range(12, 20);
		randSide = Random.Range (1, 3);

		if (rand > 6f)
		{
			randCoin--;
			blink = Instantiate(blinker, new Vector3(1f + border.transform.position.x + rand /2 , 0.43f, zPos), Quaternion.identity) as GameObject;
            blink_destroyed = false;
            blink.transform.localScale = new Vector3(rand, 0.23f, 1.76f);
			blinker.GetComponent<MeshRenderer> ().sharedMaterial.color = normalColor;
			if (randCoin == 0) {
				float z = zPos == -15.85f ? zPos = -14.97f : -4.98f;
				Instantiate (coin, new Vector3(1f + border.transform.position.x + rand /2, 1.67f, z), Quaternion.Euler(41,90,0));
				randCoin = Random.Range (3, 7);
			}
		}
	}

    private void OnDestroy()
    {
        blink_destroyed = true;
    }
}
