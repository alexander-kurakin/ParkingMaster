using UnityEngine;
using System.Collections;

public class CreateBorder : MonoBehaviour {

	public GameObject borders, blinker, study, coin;
	public static GameObject blink;
    public static bool blink_destroyed;
    public static bool blink_created;
	public static string side;
	public Color normalColor;
	private GameObject borderRight, borderLeft;
	private int randSpaces, randSide, randCoin;
    private int currCarLength;

	void Start()
	{
        randSpaces = Random.Range (10, 15); //частота дырки в блоках
		randSide   = Random.Range (1, 3); //стороны
		randCoin   = Random.Range (3, 7); //выдача монетков

        //пока что стандартно длина 5
        currCarLength =  PlayerPrefs.GetInt("CarLength", 5);

		if (PlayerPrefs.GetString ("Study") != "yes") {
			randSpaces = 4;
			study.SetActive (true);
		}

        //застройка бордюров
		for (int i = 0; i < 14; i++) //изначально блоков
		{
            //если первый, то ставим в +11х; иначе берем х от предыдущего и добавляем от 1 до 3
            float xPos = borderRight == null ? +11f : borderRight.GetComponent<MeshRenderer>().bounds.size.x + Random.Range(1f, 3f) + borderRight.transform.position.x;
			borderRight = Instantiate(borders, new Vector3(xPos, -0.18f, -3.4f), Quaternion.identity) as GameObject;
            
            //тоже самое но для лево. У и X -> путаница, на самом деле есть только Х
			float yPos = borderLeft == null ? +11f : borderLeft.GetComponent<MeshRenderer>().bounds.size.x + Random.Range(1f, 3f) + borderLeft.transform.position.x;
			borderLeft = Instantiate(borders, new Vector3(yPos, -0.18f, -17f), Quaternion.identity) as GameObject;
		}
	}

	void Update()
	{
        //правый уехал за экран
        if (borderRight.transform.position.x < 77f)
        {
            //уменьшаем генератор дырок только в половине случаев
            randSpaces--;

            //если пора делать дырку, и это сторона = право, тогда длина = от 2 до 3 корпуса машин, иначе стандарт
            float rand = randSpaces <= 0 && randSide == 1 ? (Random.Range(1.5f, 2.5f) * currCarLength) : Random.Range(1f, 3f);

            //закончились 
            if (randSpaces <= 0 && randSide == 1 && !GameCntrlr.inTurn) {

                //перегенерируем наш rand
                RandomNums(rand, -4.11f, borderRight);

                side = "Right";

            }

                //генерация дырок
                borderRight = Instantiate(borders, new Vector3(borderRight.GetComponent<MeshRenderer>().bounds.size.x + rand + borderRight.transform.position.x, -0.18f, -3.4f), Quaternion.identity) as GameObject;
                blink_created = true;
        }

        //тоже самое но налево

		if (borderLeft.transform.position.x < 77f )
		{

            float rand = randSpaces <= 0 && randSide == 2 ? (Random.Range(1.5f, 2.5f) * currCarLength) : Random.Range(1f, 3f);

            if (randSpaces <= 0 && randSide == 2 && !GameCntrlr.inTurn){
				RandomNums(rand, -15.85f, borderLeft);
                side = "Left";
            }

                borderLeft = Instantiate(borders, new Vector3(borderLeft.GetComponent<MeshRenderer>().bounds.size.x + rand + borderLeft.transform.position.x, -0.18f, -17f), Quaternion.identity) as GameObject;
                blink_created = true;
        }


	}
	void RandomNums(float rand, float zPos, GameObject border)
	{
		randSpaces = Random.Range(12, 20);
		randSide = Random.Range (1, 3);

		if (rand > 6f)
		{
            //инкрементально ждем монетку
			randCoin--;

            //генерим штуку под дырку
			blink = Instantiate(blinker, new Vector3(1f + border.transform.position.x + rand /2 , 0.43f, zPos), Quaternion.identity) as GameObject;

            blink_created = true;
            blink_destroyed = false;

            //еще и вытягиваем его к херам
            blink.transform.localScale = new Vector3(rand, 0.23f, 1.76f);

            //красим
			blinker.GetComponent<MeshRenderer> ().sharedMaterial.color = normalColor;

            //монеточка
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
