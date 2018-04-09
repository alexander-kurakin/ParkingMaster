using UnityEngine;
using System.Collections;

public class PlayerWin : MonoBehaviour {


	public static bool win;

		void OnCollisionEnter (Collision other) {
			if (other.gameObject.tag == "Car") {
				print("Player win");
				win = true;
			}
		}

	}