using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{

    public ObstacleController obstacleController;

    private float startTime;
    private float threshold = 10;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - startTime > threshold) {
            obstacleController.AddObstacle();
            threshold += 10;
        }
    }
}
