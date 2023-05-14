using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public bool visible = false;
    public bool active = false;
    public float moveDelayMillis = 200.0f;


    private Vector3 initialScale = Vector3.zero;
    private Vector2 targetPos = Vector2.zero;
    private float animationDuration = 0.0f;
    private bool animating = false;

    private float inflateStart = -1;

    // Start is called before the first frame update
    void Start()
    {
        SetVisible(visible);
    }

    // Update is called once per frame
    void Update()
    {   
        if (MainController.PLAYING) {
            if (animating) {
                Animate();
            }   

            if (MainController.PLAYING) {
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
        }

        if (inflateStart > 0) {
            float progress = (Time.time - inflateStart) * 100;
            transform.localScale = new Vector3(1f + progress, 1f + progress, 1);
        }
    }

    public void SetVisible(bool visible) {
        for (int i = 0; i < transform.childCount; i++){
            transform.GetChild(i).gameObject.SetActive(visible);
        }
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
    public void Inflate() {
        transform.position = new Vector3(transform.position.x, transform.position.y, -5f);
        inflateStart = Time.time;
    }
}
