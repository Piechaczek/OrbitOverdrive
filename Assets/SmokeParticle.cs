using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeParticle : MonoBehaviour
{

    private SpriteRenderer renderer;
    private int delay;
    private float burnoutTarget;
    private int burnoutTime;
    private int charringTime;
    private int vanishTime;

    private int timer = 0;
    private int state = -1;

    private float charringDeltaR;
    private float charringDeltaG;
    private float vanishingDeltaA;


    // Physics
    private Vector3 velocity = Vector3.zero;
    private Vector3 angularVelocity = Vector3.zero;
    private float drag = 0.1f;
    private float angularDrag = 0.1f;

    // Animation
    private List<(Color, float)> transitions;
    private Color sourceColor;
    private Color targetColor;
    private float prevTransition = -1f;
    private float nextTransition = -1f;
    private int animationState = -1;


    void Start()
    {
        renderer = gameObject.GetComponent<SpriteRenderer>();
        // burnoutTarget = Random.Range(10, 60);
        // delay = Mathf.FloorToInt(Random.Range(1, 5));
        // burnoutTime = Mathf.FloorToInt(Random.Range(1, 2));
        // // charringTime = Mathf.FloorToInt(Random.Range(1, 2));
        // vanishTime = Mathf.FloorToInt(Random.Range(1, 3));
    }

    // Update is called once per frame
    void Update()
    {
        if (nextTransition >= 0 && Time.time >= nextTransition) {
            // Transition
            animationState += 1;
            if (animationState >= transitions.Count) {
                // No more transitions, animation ended
                Destroy(gameObject);
                return;
            }
            prevTransition = nextTransition;
            nextTransition += transitions[animationState].Item2;
            sourceColor = targetColor;
            targetColor = transitions[animationState].Item1;
        }

        float progress = (Time.time - prevTransition) / (nextTransition - prevTransition);
        Color currentColor = sourceColor + new Color(targetColor.r - sourceColor.r, targetColor.g - sourceColor.g, targetColor.b - sourceColor.b) * progress;
        renderer.color = currentColor;

        // timer += 1;
        // if (state == -1) {
        //     // delay
        //     if (timer > delay) {
        //         state = 0;
        //         timer = 0;
        //     }
        // }
        // else if (state == 0) {
        //     // burnout
        //     renderer.color += new Color(0f, burnoutTarget / burnoutTime, 0f, 0f);
        //     if (timer >= burnoutTime) {
        //         state = 1;
        //         timer = 0;
        //         vanishingDeltaA = (0 - renderer.color.a) / vanishTime;
        //         // charringDeltaR = (5 - renderer.color.r) / charringTime; 
        //         // charringDeltaG = (5 - renderer.color.g) / charringTime; 
        //     }
        // } 
        // // else if (state == 1) {
        // //     // charring
        // //     renderer.color += new Color(charringDeltaR, charringDeltaG, 0f, 0f);
        // //     if (timer >= charringTime) {
        // //         state = 2;
        // //         timer = 0;
        // //         vanishingDeltaA = (0 - renderer.color.a) / vanishTime;
        // //     }
        // // } 
        // else {
        //     // vanishing
        //     renderer.color += new Color(0f, 0f, 0f, vanishingDeltaA);

        //     if (timer >= vanishTime) {
        //         Destroy(gameObject);
        //     }
        // }
    }

    public void SetAnimation(Color sourceColor, List<(Color, float)> transitions) {
        this.transitions = transitions;
        this.sourceColor = sourceColor;
        animationState = 0;
        prevTransition = Time.time;
        nextTransition = prevTransition + transitions[animationState].Item2;
        targetColor = transitions[animationState].Item1;
    }

    public void SetVelocity(Vector2 velocity, float angularVelocity) {
        this.velocity = new Vector3(velocity.x, velocity.y, 0f);
        this.angularVelocity = new Vector3(0f, 0f, angularVelocity);
    }
    
    void FixedUpdate() {
        transform.position = transform.position + velocity;
        transform.eulerAngles = transform.eulerAngles + angularVelocity;

        velocity *= 1f - drag;
        angularVelocity *= 1f - angularDrag;
    }

}
