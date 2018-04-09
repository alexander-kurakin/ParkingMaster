using UnityEngine;
using System.Collections;

public class Blinking_cube : MonoBehaviour {
	public Color normalColor;

    void Start() {
		gameObject.GetComponent<Renderer> ().sharedMaterial.color = normalColor;
    }

	void OnCreate(){
		gameObject.GetComponent<Renderer> ().sharedMaterial.color = normalColor;
	}

}
