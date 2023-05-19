using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketExhaustOld : MonoBehaviour
{

    public GameObject smokePrefab;
    public GameObject smokeAnchor;


    private Rigidbody2D rocketRigidbody;
    public float velocityMagScale = 0.1f;
    public float posRand = 0.1f;
    public float scaleRandMin = 0.1f;
    public float scaleRandMax = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        rocketRigidbody = transform.parent.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 velocity = rocketRigidbody.velocity;
        float mag = velocity.magnitude;
        float scaledMag = mag * velocityMagScale;
        for (int i = 0; i < Mathf.Clamp(velocity.magnitude, 1, 3); i++){
            GameObject obj = Instantiate(smokePrefab, smokeAnchor.transform);
            obj.transform.localPosition = transform.position + new Vector3(velocity.x * Random.Range(-posRand*scaledMag, posRand*scaledMag), velocity.y * Random.Range(-posRand*scaledMag, posRand*scaledMag));
            obj.transform.localScale = new Vector3(Random.Range(scaleRandMin, scaleRandMax), Random.Range(scaleRandMin, scaleRandMax), 1);
            obj.transform.localEulerAngles = new Vector3(0, 0, Random.Range(0f, 360f));
        }
    }


}
