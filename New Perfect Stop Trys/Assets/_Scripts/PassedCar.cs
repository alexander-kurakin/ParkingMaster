using UnityEngine;
using System.Collections;

public class PassedCar : MonoBehaviour {

    void Update() {
        if (GameCntrlr.stop) {
            foreach (BoxCollider coll in GetComponents<BoxCollider>()) {
                coll.enabled = false;
            }
        }
    }
}
