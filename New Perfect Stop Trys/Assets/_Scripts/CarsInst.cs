using UnityEngine;
using System.Collections;

public class CarsInst : MonoBehaviour {

    public GameObject[] cars;
    private GameObject carCurrentRight, carCurrentLeft;

    void Start() {
        for (int i = 0; i<10; i++)
        {
            float xPos = carCurrentRight == null ? -0.01f : carCurrentRight.GetComponent<MeshRenderer>().bounds.size.x + Random.Range(3f, 5f) + carCurrentRight.transform.position.x;
            carCurrentRight = Instantiate(cars[Random.Range(0, cars.Length)], new Vector3(xPos, -0.18f, -3.4f), Quaternion.Euler(new Vector3(-0.1f, 95, 0))) as GameObject;

            float yPos = carCurrentLeft == null ? -0.01f : carCurrentLeft.GetComponent<MeshRenderer>().bounds.size.x + Random.Range(3f, 5f) + carCurrentLeft.transform.position.x;
            carCurrentLeft = Instantiate(cars[Random.Range(0, cars.Length)], new Vector3(yPos, -0.18f, -17f), Quaternion.Euler(new Vector3(-0.1f, 95, 0))) as GameObject;
        }
    }
    void Update() {

        if (Input.GetKeyDown("escape"))
            Application.Quit();

        if (carCurrentRight.transform.position.x < 80f)
            carCurrentRight = Instantiate(cars[Random.Range(0, cars.Length)], new Vector3(carCurrentRight.GetComponent<MeshRenderer>().bounds.size.x + Random.Range(3f, 5f) + carCurrentRight.transform.position.x, -0.18f, -3.4f), Quaternion.Euler(new Vector3(-0.1f, 95, 0))) as GameObject;

        if (carCurrentLeft.transform.position.x < 80f)
            carCurrentLeft = Instantiate(cars[Random.Range(0, cars.Length)], new Vector3(carCurrentLeft.GetComponent<MeshRenderer>().bounds.size.x + Random.Range(3f, 5f) + carCurrentLeft.transform.position.x, -0.18f, -17f), Quaternion.Euler(new Vector3(-0.1f, 95, 0))) as GameObject;
    }
}
