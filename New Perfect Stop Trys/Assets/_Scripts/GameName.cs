using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class GameName : MonoBehaviour {

    private float speed = 0.0001f, yPos;

    void Start() {
        yPos = transform.position.y;
    }


	void Update () {

        if (transform.position.y >= yPos + 0.005f || transform.position.y <= yPos - 0.005f)
            speed = -speed;
        transform.position += new Vector3(0, speed, 0);
	}
}
