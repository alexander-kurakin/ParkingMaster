using UnityEngine;
using System.Collections;

public class TimeChange : MonoBehaviour {

    public float scrollSpeed = 0.2f;
    public Material mat;

    void Start ()
    {
        mat.mainTextureOffset = new Vector2(Random.Range(-0.1f, 0.2f), 0);
    }

	void Update () {
        float offset = Time.time * scrollSpeed;
        mat.mainTextureOffset = new Vector2(offset, 0);
	}
}
