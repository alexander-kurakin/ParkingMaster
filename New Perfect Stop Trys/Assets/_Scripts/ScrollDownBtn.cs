using UnityEngine;
using System.Collections;

public class ScrollDownBtn : MonoBehaviour {

	private float speed = 5f;
	private RectTransform rec;

	void Start (){
		rec = GetComponent<RectTransform> ();
	}

	// Update is called once per frame
	void Update () {
	
		if (rec.offsetMin.y != 0) {
			rec.offsetMin += new Vector2 (rec.offsetMin.x, speed);
			rec.offsetMax += new Vector2 (rec.offsetMax.x, speed);
		}
	}
}
