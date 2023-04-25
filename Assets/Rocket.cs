using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{

    public float gravityConstant = 1.0f;
    public float forceCap = 1.0f;
    public float speed = 1.0f;
    public BlackHole blackHole;
    public ScreenShake screenShake;
    private Vector3 velocity = new Vector3(0.0f, 0.0f, 0.0f);
    private Vector3 acceleration = new Vector3(0.0f, 0.0f, 0.0f);
    private Rigidbody2D rb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, 1);
    }

    void FixedUpdate()
    {
        if (blackHole.visible){
            Vector2 diff = new Vector2(blackHole.transform.position.x - transform.position.x, blackHole.transform.position.y - transform.position.y);
            float r  = diff.magnitude;
            rb.AddForce(new Vector2(Mathf.Clamp(gravityConstant * diff.x / Mathf.Pow(r, 3.0f), -forceCap, forceCap), Mathf.Clamp(gravityConstant * diff.y / Mathf.Pow(r, 3.0f), -forceCap, forceCap)));
        }
    
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x);
        rb.AddForce(new Vector2(speed*Mathf.Cos(angle), speed*Mathf.Sin(angle)));
    }

    void Update() {
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x);
        transform.localEulerAngles = new Vector3(0.0f, 0.0f, angle * 180.0f / Mathf.PI - 90.0f);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        screenShake.endShake = Time.time + 0.1f;
    }
}
