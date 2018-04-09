using UnityEngine;
using System.Collections;

public class MoveObjects : MonoBehaviour {

    public float delete = -70f;
    [HideInInspector]
    public static float speed = 25f;
	
	// Update is called once per frame
	void Update () {
        transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);

        if (transform.position.x < delete-35f)
            Destroy(gameObject);
	}
}
