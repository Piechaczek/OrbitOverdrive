using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public bool visible = false;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        SetVisible(visible);
    }

    // Update is called once per frame
    void Update()
    {      
        if (Input.GetMouseButtonDown(0)) {
            SetVisible(true);
            Vector2 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float cameraSize = Camera.main.orthographicSize;
            if (newPos.x > -cameraSize && newPos.x < cameraSize && newPos.y > -cameraSize && newPos.y < cameraSize){
                transform.position = new Vector3(newPos.x, newPos.y, 0.0f);
            }
        }
    }

    void SetVisible(bool visible) {
        spriteRenderer.enabled = visible;
        this.visible = visible;
    }
}
