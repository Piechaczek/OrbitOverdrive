using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{

    public GameObject top;
    public GameObject bottom;
    public GameObject right;
    public GameObject left;
    public GameObject background;

    // Start is called before the first frame update
    void Start()
    {
        top.transform.position = new Vector3(0, Camera.main.orthographicSize, -1);
        top.transform.localScale = new Vector3(Camera.main.orthographicSize * 2, 1, 1);
        bottom.transform.position = new Vector3(0, -Camera.main.orthographicSize, -1);
        bottom.transform.localScale = new Vector3(Camera.main.orthographicSize * 2, 1, 1);
        left.transform.position = new Vector3(-Camera.main.orthographicSize, 0, -1);
        left.transform.localScale = new Vector3(0.5f, Camera.main.orthographicSize * 2, 1);
        right.transform.position = new Vector3(Camera.main.orthographicSize, 0, -1);
        right.transform.localScale = new Vector3(0.5f, Camera.main.orthographicSize * 2, 1);

        background.transform.localScale = new Vector3(Camera.main.orthographicSize * 2, Camera.main.orthographicSize * 2, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
