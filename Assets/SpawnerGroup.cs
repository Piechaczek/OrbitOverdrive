using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerGroup : MonoBehaviour
{

    public GameObject spawnerPrefab;

    // Update is called once per frame
    void Update()
    {
        if (spawnerPrefab != null && transform.childCount == 0) {
            Instantiate(spawnerPrefab, transform);
        }
    }
}
