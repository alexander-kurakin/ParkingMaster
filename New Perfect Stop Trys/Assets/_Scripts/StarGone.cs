using UnityEngine;
using System.Collections;

public class StarGone : MonoBehaviour {


	// Update is called once per frame
	void Update () {

		if (transform.position.y != 10f)
			transform.position = Vector3.MoveTowards (transform.position, new Vector3 (transform.position.x, 10f, transform.position.z), 10 * Time.deltaTime);
				else
				Destroy(gameObject);
				}
	}

