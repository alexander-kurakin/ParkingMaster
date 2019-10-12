using UnityEngine;
using System.Collections;

public class CarBehaviour : MonoBehaviour {

    public float speed = 10f;
	public bool onPlace;
    public static bool restrictStops;
    public Color passedColor;
	private Rigidbody rb;
    private int passedCount;

	void Start(){
		rb = GetComponent<Rigidbody> ();
	}

    void Update() {

        if (!onPlace)
        {
            //куда двигаем, при изначальной загрузке тачки
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(20f, 0, -9.9f), speed * Time.deltaTime);
            CheckClick.click = false;
            restrictStops = true;
        }

        if (transform.position.x == 20f)
        {
            onPlace = true;
            restrictStops = false;
        }

    }

	void FixedUpdate(){
		float moveHor = Input.GetAxis("Vertical");
		float moveVer = Input.GetAxis("Horizontal");

		Vector3 movement = new Vector3 (moveHor, 0.0f, -moveVer);
		rb.AddForce(movement * speed*2);
        CheckClick.click = false;
    }

    void OnTriggerExit(Collider other) {
        if (other.tag == "Passed") {
            passedCount++;
            other.GetComponent<Renderer>().material.color = passedColor;

            if ((PlayerPrefs.GetString("Music") != "off") && (other.GetComponent<AudioSource>()))
            {
                other.GetComponent<AudioSource>().Play();
            }

            if (passedCount % 5 == 0 && !(GameCntrlr.multiplier == 1))
            {
                GameCntrlr.multiplier /= 2;
            }
        }
    }
}
