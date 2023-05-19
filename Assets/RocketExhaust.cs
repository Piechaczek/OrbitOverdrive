using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketExhaust : MonoBehaviour
{

    public GameObject smokePrefab;
    public Transform smokeHolder;


    private Rigidbody2D rocketRigidbody;
    private Transform rocketTransform;

    public float xSpread = 1f;
    public float ySpreadScale = 1f;
    public float smokeAmountScale = 0.2f;

    public float minSmoke = 0.003f;
    public float maxSmoke = 0.25f;
    private float nextSmoke = 0;
    private float smokeDeltaTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        rocketRigidbody = transform.parent.GetComponent<Rigidbody2D>();
        rocketTransform = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        if (MainController.PLAYING) {
            smokeDeltaTime += Time.deltaTime;
            if (smokeDeltaTime * rocketRigidbody.velocity.magnitude * smokeAmountScale > nextSmoke) {

                GameObject instance = Instantiate(smokePrefab, transform.position, transform.rotation, smokeHolder);
                SmokeParticle particle = instance.GetComponent<SmokeParticle>();

                // calculate random vector
                float force = rocketRigidbody.velocity.sqrMagnitude;
                Vector3 randVector = new Vector3(Random.Range(-xSpread, xSpread), force * Random.Range(0f, ySpreadScale), 0f);
                Vector3 rotated = new Vector3(randVector.x * Mathf.Cos(rocketTransform.eulerAngles.z), randVector.y * Mathf.Sin(rocketTransform.eulerAngles.z));

                particle.transform.localScale *= 2f;
                particle.SetVelocity(rotated, Random.Range(-10f, 10f));
                Color startColor = new Color(Random.Range(0.7f, 0.9f), Random.Range(0f, 0.3f), 0f);
                var transitions = new List<(Color, float)>{
                    (new Color(startColor.r * 0.3f, startColor.g * 0.5f, 0f, 1f), 0.1f),
                    (new Color(0.1f, 0.1f, 0.1f, 0f), 0.01f)
                };
                particle.SetAnimation(startColor, transitions);

            
                smokeDeltaTime = 0;
                nextSmoke = Random.Range(minSmoke, maxSmoke);
            }
        }
    }
}
