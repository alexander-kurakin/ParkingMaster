using UnityEngine;
using System.Collections;

public class CollectOneCoin : MonoBehaviour {

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (gameObject);	
		StartCoroutine (DeleteObj());
	}

	IEnumerator DeleteObj(){
		yield return new WaitForSeconds (GetComponent<AudioSource> ().clip.length);
		Destroy (gameObject);
	}

}
