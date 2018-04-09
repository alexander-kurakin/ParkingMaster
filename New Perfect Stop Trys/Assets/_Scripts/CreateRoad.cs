using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;

public class CreateRoad : MonoBehaviour {

    public GameObject[] roadInst;
    private GameObject currentRoad;
    public GameObject border;
    private int nextLocation, fromLocation = 0, toLocation = 3; //начало в первых 4-х

    void Start() {
        nextLocation = Random.Range(4, 6); //Выбираем число
        currentRoad = Instantiate(roadInst[Random.Range(fromLocation, toLocation)], new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
    }

    void FixedUpdate() {
        if (currentRoad.transform.position.x < 0)
        {
            currentRoad = Instantiate(roadInst[Random.Range(fromLocation, toLocation)], new Vector3(currentRoad.transform.position.x + 80f, 0, 0), Quaternion.identity) as GameObject;
            nextLocation--; //Уменьшаем число при пропаже дороги

        }
        if (nextLocation == 0) //Как дошли до нуля 
        {
            nextLocation = Random.Range(4, 6); //След число уже с большей вариативностью
            Instantiate(border, new Vector3(currentRoad.transform.position.x + 17.94f, 4.22f, -4.33f), Quaternion.identity);

            int rand = Random.Range(1, 5); //Определяем следующие локации
            switch (rand)
            {
                case 1:                
                    fromLocation = 0;
                    toLocation = 3;
                    break;
                case 2:               
                    fromLocation = 4;
                    toLocation = 7;
                    break;
			    case 3:
				    fromLocation = 8;
				    toLocation = 11;
				    break;
			    case 4:
				    fromLocation = 12;
				    toLocation = 15;
					break;
            }
            currentRoad = Instantiate(roadInst[Random.Range(fromLocation, toLocation)], new Vector3(currentRoad.transform.position.x + 99f, 0, 0), Quaternion.identity) as GameObject;
        }
    }
}
