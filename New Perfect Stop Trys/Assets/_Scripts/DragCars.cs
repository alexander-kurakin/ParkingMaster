using UnityEngine;
using System.Collections;

public class DragCars : MonoBehaviour {

	public GameObject cars;
	private Vector3 screenPoint, offset;
	private float lockedYPos;

    private void Start()
    {
        screenPoint = new Vector3 (0, 0, 0);
    }

    void Update(){
		if (cars.transform.position.x > 0) 
			cars.transform.position = Vector3.MoveTowards (cars.transform.position, new Vector3 (0f, cars.transform.position.y, cars.transform.position.z), 10 * Time.deltaTime);
		else if (cars.transform.position.x < -139f)
			cars.transform.position = Vector3.MoveTowards (cars.transform.position, new Vector3 (-138.5f, cars.transform.position.y, cars.transform.position.z), 10 * Time.deltaTime);
	}

	// Use this for initialization
	void OnMouseDown () {
		lockedYPos = screenPoint.x;
		offset = cars.transform.position - Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x,Input.mousePosition.y,screenPoint.z));
		Cursor.visible = false;
        Debug.Log(screenPoint.x);
        Debug.Log(screenPoint.y);
        Debug.Log(screenPoint.z);
    }

	void OnMouseDrag(){
		Vector3 curScreenPoint = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 curPosition = Camera.main.ScreenToWorldPoint (curScreenPoint) + offset;
		curPosition.y = lockedYPos;
		cars.transform.position = curPosition;
	}

	void OnMouseUp(){
		Cursor.visible = true;
	}

}
