using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {
	public static float speed = 20f;

	void Update () {
		if (Input.GetKey("w"))
			transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);

		if (Input.GetKey("s"))
			transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);

		if (Input.GetKey ("d")) {
			float rot_r = 150;
			float z_r = -20f;
			transform.position = Vector3.MoveTowards (transform.position, new Vector3 (transform.position.x, transform.position.y, z_r), 10 * Time.deltaTime); 
			transform.Rotate (Vector3.up * rot_r * Time.deltaTime);
		}

			if (Input.GetKey ("a")) {
			float rot_l = -150;
				float z_l = 0.1f;
				transform.position = Vector3.MoveTowards (transform.position, new Vector3 (transform.position.x, transform.position.y, z_l), 10 * Time.deltaTime); 
				transform.Rotate (Vector3.up * rot_l * Time.deltaTime);
		}
	}
}
