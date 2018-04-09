using UnityEngine;
using System.Collections;

public class ScrollLeft : MonoBehaviour {

	public float speed;
		private RectTransform rec;

		void Start (){
			rec = GetComponent<RectTransform> ();
		}

		// Update is called once per frame
		void Update () {

			if (rec.offsetMin.x != 0) {
				rec.offsetMin += new Vector2 (speed, rec.offsetMin.y);
				rec.offsetMax += new Vector2 (speed, rec.offsetMax.y);
			}
		}
	}
