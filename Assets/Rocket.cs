using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{

    public float gravityConstant = 1.0f;
    public float forceCap = 1.0f;
    public float speed = 1.0f;
    public BlackHole blackHole;
    private Vector3 velocity = new Vector3(0.0f, 0.0f, 0.0f);
    private Vector3 acceleration = new Vector3(0.0f, 0.0f, 0.0f);
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (blackHole.visible){
            Vector2 diff = new Vector2(blackHole.transform.position.x - transform.position.x, blackHole.transform.position.y - transform.position.y);
            float r  = diff.magnitude;
            rb.AddForce(new Vector2(Mathf.Clamp(gravityConstant * diff.x / Mathf.Pow(r, 3.0f), -forceCap, forceCap), Mathf.Clamp(gravityConstant * diff.y / Mathf.Pow(r, 3.0f), -forceCap, forceCap)));
        }
        
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x);
        transform.localEulerAngles = new Vector3(0.0f, 0.0f, angle * 180.0f / Mathf.PI);
        rb.AddForce(new Vector2(speed*Mathf.Cos(angle), speed*Mathf.Sin(angle)));

        // Vector3 diff = blackHole.transform.position - transform.position;
        // float magnitude = Mathf.Min(1.0f, gravityConstant * mass / diff.sqrMagnitude);
        // float angle = Mathf.Atan2(diff.y, diff.x);
        // acceleration = new Vector3(magnitude * Mathf.Cos(angle), magnitude * Mathf.Sin(angle), 0);

        // // physics update
        // velocity += acceleration;
        // transform.localPosition += velocity;

        // transform.localEulerAngles = new Vector3(0.0f, 0.0f, angle * 180.0f / Mathf.PI);

        // Debug.Log("Diff: " + diff.ToString() + " Magnitude: " + magnitude.ToString() +  " Angle: " + angle.ToString() +  " Acc: " + acceleration.ToString() + " Vel: " + acceleration.ToString() + " Pos: " + acceleration.ToString());
    }

    void OnCollisionEnter2D(Collision2D collision) {
        // TODO
    }

    void OnTriggerEnter2D(Collider2D colider) {
        BlackHole hole = colider.gameObject.GetComponent<BlackHole>();
        if (hole != null && hole.visible){
            Destroy(gameObject);
        }

        Coin coin = colider.gameObject.GetComponent<Coin>();
        if (coin != null) {
            Destroy(coin.gameObject);
        }

    }
}
