using UnityEngine;
using System.Collections;

public class CarBehaviour : MonoBehaviour {

    public float speed = 10f;
	public bool onPlace;
    public Color passedColor;
	private Rigidbody rb;

	void Start(){
		rb = GetComponent<Rigidbody> ();
	}

    void Update() {
        if (!onPlace)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(20f, 0, -9.9f), speed * Time.deltaTime);
            CheckClick.click = false;
        }
        if (transform.position.x == 20f)
            onPlace = true;
	}

	void FixedUpdate(){
		float moveHor = Input.GetAxis("Vertical");
		float moveVer = Input.GetAxis("Horizontal");

		Vector3 movement = new Vector3 (moveHor, 0.0f, -moveVer);
		rb.AddForce(movement * speed*2);
        CheckClick.click = false;
    }

    /*void OnTriggerExit(Collider other) {
        if (other.tag == "Passed") {
            PlayerLose.lose = true;
            CheckClick.click = true;
            other.GetComponent<Renderer>().material.color = passedColor;
			print ("Car passed, lose!");
			MoveObjects.speed = 0;
        }
    }*/
}
