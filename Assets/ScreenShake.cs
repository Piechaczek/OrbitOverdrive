using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    
    public float endShake = -1f;

    void FixedUpdate() {
        if (Time.time < endShake) {
            // should shake
            transform.position = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), -10f);
        } else {
            transform.position = new Vector3(0f, 0f, -10f);
        }
    }

}
