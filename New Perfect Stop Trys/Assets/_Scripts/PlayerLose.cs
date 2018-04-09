using UnityEngine;
using System.Collections;

public class PlayerLose : MonoBehaviour {

    public static bool lose;
	public Color loseColor;
	public GameObject blink, explosion;


	void OnCollisionEnter (Collision other) {
        if (other.gameObject.tag == "Car") {
			print("Player lose by hittin a car!");
			blink.GetComponent<MeshRenderer> ().sharedMaterial.color = loseColor;
            lose = true;
			other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0,0,-other.gameObject.transform.position.z) * 10);
			MoveObjects.speed = 0;

			Vector3 pos = Vector3.Lerp (gameObject.transform.position, other.transform.position, 0.5f);
			GameObject exp = Instantiate(explosion, new Vector3 (pos.x, 2.82f, pos.z), Quaternion.Euler(41,90,0)) as GameObject;
			Destroy (exp, 1.5f);

            GameCntrlr.multiplier = 1;

			if ((PlayerPrefs.GetString ("Music") != "off")&&(gameObject.GetComponent<AudioSource>())) {
				GetComponent<AudioSource> ().Play ();
			}
        }
	}

	void OnDestroy(){
		lose = false;
	}
	
}