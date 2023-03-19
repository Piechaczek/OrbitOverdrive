using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{

    public GameObject coinPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount < 1){
            GameObject child = Instantiate(coinPrefab, transform);
            float cameraSize = Camera.main.orthographicSize;
            float randX = Random.Range(-cameraSize + coinPrefab.transform.localScale.x, cameraSize - coinPrefab.transform.localScale.x);
            float randY = Random.Range(-cameraSize + coinPrefab.transform.localScale.y, cameraSize - coinPrefab.transform.localScale.y);
            child.transform.position = new Vector3(randX, randY, 0.0f);
        } 
    }
}
