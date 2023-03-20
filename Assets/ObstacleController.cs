using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{

    public readonly static int ROW_SIZE = 3;
    public readonly static int COL_SIZE = 3;

    public GameObject obstaclePrefab;
    private List<Obstacle> obstacles = new List<Obstacle>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddObstacle() {
        GameObject child = Instantiate(obstaclePrefab, transform);
        Obstacle obstacle = child.GetComponent<Obstacle>();
        obstacles.Add(obstacle);
        obstacle.SetObstaclePos(Mathf.FloorToInt(Random.Range(0, ROW_SIZE)), Mathf.FloorToInt(Random.Range(0, COL_SIZE)));
    }
}
