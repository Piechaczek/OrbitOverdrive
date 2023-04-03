using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float maxHealth = 200.0f;
    public float initialVelocity = 10f;
    private float collisionResistance;
    private float health;
    public bool wasHit = false; // if false, enemy won't tumble
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        rigidbody = gameObject.GetComponent<Rigidbody2D>();

        health = maxHealth;
        collisionResistance = initialVelocity;

        // initial movement
        float randAngle = Random.Range(0.0f, 360.0f);
        rigidbody.velocity = new Vector2(initialVelocity * Mathf.Cos(randAngle), initialVelocity * Mathf.Sin(randAngle));
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0.0f) {
            Destroy(gameObject);
        }

        if (!wasHit){
            transform.localRotation = Quaternion.identity;
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        float collisionSpeed = collision.relativeVelocity.magnitude;
        float healthLoss = collisionSpeed;
        if (collision.gameObject.tag != "Player") {
            healthLoss = collisionSpeed - collisionResistance;
        }

        health -= Mathf.Max(0, healthLoss);
        spriteRenderer.color = new Color((health / maxHealth), spriteRenderer.color.g, spriteRenderer.color.b, spriteRenderer.color.a);

        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy") {
            wasHit = true;
        }
    }
}
