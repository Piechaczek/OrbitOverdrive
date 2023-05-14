using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float maxHealth = 200.0f;
    public float initialVelocity = 10f;
    public float scoreMultiplier = 1f;
    private float collisionResistance;
    private float health;
    public bool wasHit = false; // if false, enemy won't tumble
    public SpriteRenderer cabinRenderer;
    public SpriteRenderer bodyRenderer; 
    private Rigidbody2D rigidbody;
    private AudioSource hitAudioSource;

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

    public ScoreSheet scoreSheet;

    public GameObject smokePrefab;
    private float smokeTime = 0;
    private float minSmokeTime = 0.15f;
    private float maxSmokeTime = 0.15f; 

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        hitAudioSource = gameObject.GetComponent<AudioSource>();
        if (AudioController.INSTANCE.soundsOff) {
            hitAudioSource.volume = 0f;
        } else {
            hitAudioSource.volume = 1f;
        }

        health = maxHealth;
        collisionResistance = initialVelocity;

        // initial movement
        float randAngle = Random.Range(0.0f, 360.0f);
        rigidbody.velocity = new Vector2(initialVelocity * Mathf.Cos(randAngle), initialVelocity * Mathf.Sin(randAngle));
    }

    // Update is called once per frame
    void Update()
    {
        if (MainController.PLAYING){
            if (health <= 0.0f) {
                for (int i = 0; i < 15; i++) {
                    SummonSmokeParticle(1f);
                }
                for (int i = 0; i < 12; i++) {
                    SummonFireParticle();
                }
                Destroy(gameObject);
            }

            if (!wasHit){
                transform.localRotation = Quaternion.identity;
            }

            if (smokeTime > 0){
                if (Time.time >= smokeTime) {
                    smokeTime += Random.Range(minSmokeTime, maxSmokeTime);
                    SummonSmokeParticle(0.3f);
                }
            }
        } else {
            GetComponent<Rigidbody2D>().simulated = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (MainController.PLAYING){
            float collisionSpeed = collision.relativeVelocity.magnitude;
            float healthLoss = collisionSpeed;
            if (collision.gameObject.tag != "Player") {
                healthLoss = collisionSpeed - collisionResistance;
            }

            if (healthLoss > 5) {
                if (hitAudioSource.volume > 0) {
                    hitAudioSource.volume = Mathf.Clamp(healthLoss / maxHealth * 2f, 0.2f, 1f);
                }
                hitAudioSource.Play();
            }

            AddPoints(healthLoss / maxHealth, collision.gameObject.tag);
            TakeDamage(healthLoss);

            if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy") {
                wasHit = true;
            }
        } 
    }

    void AddPoints(float damageDealtPercent, string collidedWith) {
        ScoreSheet.Score score = scoreSheet.GetScore(health / maxHealth, damageDealtPercent, collidedWith);
        if (score != null) {
            int multipliedScore = Mathf.RoundToInt(score.score * scoreMultiplier);
            int lastDigitDelta = Mathf.RoundToInt((multipliedScore % 10) * 2 / 10.0f) * 5 - (multipliedScore % 10);
            multipliedScore += lastDigitDelta;

            if (MainController.PLAYING) {
                MainController.INSTANCE.AddScore(score.scoreText, multipliedScore);
            }
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
            smokeTime = Time.time - 5 * minSmokeTime;
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

    void SummonSmokeParticle(float time) {
        GameObject smokeParticleObject = Instantiate(smokePrefab, transform.position, Quaternion.identity);
        smokeParticleObject.transform.localScale /= transform.localScale.x; // adjust for enemy size
        SmokeParticle smokeParticle = smokeParticleObject.GetComponent<SmokeParticle>();
        smokeParticle.SetVelocity(new Vector2(Random.Range(-0.1f, 0.1f) * transform.localScale.x, Random.Range(-0.1f, 0.1f) * transform.localScale.x), Random.Range(-10f, 10f));
        var transitions = new List<(Color, float)>{
            (new Color(0.1f, 0.1f, 0.1f, 1f), time),
            (new Color(0.1f, 0.1f, 0.1f, 0f), 0.1f)
        };
        smokeParticle.SetAnimation(new Color(0.2f, 0.2f, 0.2f), transitions);
    }

    void SummonFireParticle() {
        GameObject smokeParticleObject = Instantiate(smokePrefab, transform.position, Quaternion.identity);
        smokeParticleObject.transform.localScale /= transform.localScale.x; // adjust for enemy size
        SmokeParticle smokeParticle = smokeParticleObject.GetComponent<SmokeParticle>();

        Color startColor = new Color(Random.Range(0.7f, 0.9f), Random.Range(0f, 0.3f), 0f);
        smokeParticle.SetVelocity(new Vector2(Random.Range(-0.08f, 0.08f) * transform.localScale.x, Random.Range(-0.08f, 0.08f) * transform.localScale.x), Random.Range(-10f, 10f));
        var transitions = new List<(Color, float)>{
            (new Color(startColor.r * 0.3f, startColor.g * 0.5f, 0f, 1f), 0.6f),
            (new Color(0.1f, 0.1f, 0.1f, 0f), 0.1f)
        };
        smokeParticle.SetAnimation(startColor, transitions);
    }

}
