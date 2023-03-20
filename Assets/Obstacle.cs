using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    public readonly int obstaclePosX;
    public readonly int obstaclePosY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetObstaclePos(int x, int y) {
        float xMargin = Camera.main.orthographicSize * 2 / ObstacleController.ROW_SIZE;
        float yMargin = Camera.main.orthographicSize * 2 / ObstacleController.COL_SIZE;
        float posX = x * xMargin + (xMargin / 2.0f) - Camera.main.orthographicSize;
        float posY = y * yMargin + (yMargin / 2.0f) - Camera.main.orthographicSize;
        transform.position = new Vector3(posX, posY);
    }
}
