using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    private static float SETTLE_TIME_THRESHOLD = 0.2f;
    private static float POSITION_BOUND = 18f;

    private float settleTime = 0f;
    private bool settled = false;

    public GameObject enemyPrefab;
    private Enemy instance;
    private float scalingStart;
    private float scalingDuration;
    private float desiredEnemyScale;

    void Start()
    {
        desiredEnemyScale = enemyPrefab.transform.localScale.x;
        transform.localScale = new Vector3(desiredEnemyScale, desiredEnemyScale, 1f);
    }

    void Update()
    {
        if (!settled) {
            settleTime += Time.deltaTime;
            if (settleTime > SETTLE_TIME_THRESHOLD) {
                settled = true;
                
                GameObject instanceObject = Instantiate(enemyPrefab, transform.position, transform.rotation, transform.parent);
                instance = instanceObject.GetComponent<Enemy>();
                instance.transform.localScale = new Vector3(0f, 0f, 0f);
                scalingStart = Time.time;
                scalingDuration = 0.3f;
            }
        } else {
            float delta = Time.time - scalingStart;
            float currentScale = desiredEnemyScale * Mathf.Min(delta / scalingDuration, 1f);
            instance.transform.localScale = new Vector3(currentScale, currentScale, 1f);

            if (delta / scalingDuration > 1) {
                // That means we're done with the animation
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (!settled){
            settleTime = 0f;
            transform.localPosition = new Vector3(Random.Range(-POSITION_BOUND, POSITION_BOUND), Random.Range(-POSITION_BOUND, POSITION_BOUND), 0f);
        }
    }



}
