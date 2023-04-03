using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public bool visible = false;
    public bool active = false;
    public float moveDelayMillis = 200.0f;


    private Vector3 initialScale = Vector3.zero;
    private Vector2 targetPos = Vector2.zero;
    private float animationDuration = 0.0f;
    private bool animating = false;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        SetVisible(visible);
    }

    // Update is called once per frame
    void Update()
    {   
        if (animating) {
            Animate();
        }   

        if (Input.GetMouseButtonDown(0)) {
            Vector2 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float cameraSize = Camera.main.orthographicSize;
            if (newPos.x > -cameraSize && newPos.x < cameraSize && newPos.y > -cameraSize && newPos.y < cameraSize){
                if (!animating) {
                    StartAnimation(newPos);
                }
            }
        }
    }

    void SetVisible(bool visible) {
        spriteRenderer.enabled = visible;
        this.visible = visible;
        this.active = visible;
    }

    void StartAnimation(Vector2 targetPos) {
        this.animating = true;
        this.animationDuration = 0.0f;
        this.initialScale = transform.localScale;
        this.targetPos = targetPos;
        // this.active = false;
    }

    void StopAnimation() {
        this.animating = false;
        // this.active = true;
    }


    void Animate() {
        animationDuration += Time.deltaTime * 1000.0f;

        if (animationDuration > moveDelayMillis){
            StopAnimation();
            animationDuration = moveDelayMillis;
        }

        if (animationDuration < this.moveDelayMillis / 2.0f) {
            float factor = 1.0f - (animationDuration * 2.0f / this.moveDelayMillis);
            transform.localScale = new Vector3(factor * initialScale.x, factor * initialScale.y, initialScale.z);
        } else {
            SetVisible(true);
            transform.position = new Vector3(targetPos.x, targetPos.y, transform.position.z);
            float factor = (animationDuration * 2.0f / this.moveDelayMillis) - 1.0f;
            transform.localScale = new Vector3(factor * initialScale.x, factor * initialScale.y, initialScale.z);
        }
    }
}
