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
    public SpriteRenderer cabinRenderer;
    public SpriteRenderer bodyRenderer; 
    private Rigidbody2D rigidbody;

    public Sprite cabin1;
    public Sprite cabin2;
    public Sprite cabin3;
    public Sprite cabin4;
    public Sprite cabin5;
    public Sprite body1;
    public Sprite body2;
    public Sprite body3;
    public Sprite body4;
    public Sprite body5;

    // Start is called before the first frame update
    void Start()
    {
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

        TakeDamage(healthLoss);

        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy") {
            wasHit = true;
        }
    }

    void TakeDamage(float damage) {
        health -= Mathf.Max(0, damage);
        
        if (health / maxHealth < 0.1){
            bodyRenderer.sprite = body5;
        } else if (health / maxHealth < 0.2){
            cabinRenderer.sprite = cabin5;
        } else if (health / maxHealth < 0.3){
            bodyRenderer.sprite = body4;
        } else if (health / maxHealth < 0.4){
            cabinRenderer.sprite = cabin4;
        } else if (health / maxHealth < 0.5){
            bodyRenderer.sprite = body3;
        } else if (health / maxHealth < 0.6){
            cabinRenderer.sprite = cabin3;
        } else if (health / maxHealth < 0.7){
            bodyRenderer.sprite = body2;
        } else if (health / maxHealth < 0.8){
            cabinRenderer.sprite = cabin2;
        } else if (health / maxHealth < 0.9){
            bodyRenderer.sprite = body1;
        } else {
            cabinRenderer.sprite = cabin1;
        }
    }
}
