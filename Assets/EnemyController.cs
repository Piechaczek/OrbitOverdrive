using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public SpawnerGroup spawnerGroupPrefab;
    public EnemySpawner bigEnemySpawerPrefab;
    public EnemySpawner enemySpawerPrefab;
    public EnemySpawner smallEnemySpawerPrefab;

    private int waveCount;
    private int waveNumber = 0;
    private List<(EnemySpawner, bool)>[] waves; // list of (enemy spawner, respawnable)
    private float[] waveTimes;


    void Start() {
        waveCount = 8;

        waves = new List<(EnemySpawner, bool)>[waveCount];
        waveTimes = new float[waveCount];

        // wave 1
        waveTimes[0] = 0;
        waves[0] = new List<(EnemySpawner, bool)>();
        waves[0].Add((enemySpawerPrefab, true));
        waves[0].Add((enemySpawerPrefab, true));
        waves[0].Add((enemySpawerPrefab, true));

        // wave 2
        waveTimes[1] = 13.5f;
        waves[1] = new List<(EnemySpawner, bool)>();
        waves[1].Add((enemySpawerPrefab, false));
        waves[1].Add((enemySpawerPrefab, false));

        // wave 3
        waveTimes[2] = 40;
        waves[2] = new List<(EnemySpawner, bool)>();
        waves[2].Add((smallEnemySpawerPrefab, true));
        waves[2].Add((smallEnemySpawerPrefab, true));

        // wave 4
        waveTimes[3] = 55;
        waves[3] = new List<(EnemySpawner, bool)>();
        waves[3].Add((bigEnemySpawerPrefab, true));

        // wave 5
        waveTimes[4] = 81;
        waves[4] = new List<(EnemySpawner, bool)>();
        waves[4].Add((smallEnemySpawerPrefab, false));

        // wave 6
        waveTimes[5] = 108;
        waves[5] = new List<(EnemySpawner, bool)>();
        waves[5].Add((enemySpawerPrefab, true));

        // wave 7
        waveTimes[6] = 121;
        waves[6] = new List<(EnemySpawner, bool)>();
        waves[6].Add((bigEnemySpawerPrefab, true));
        waves[6].Add((smallEnemySpawerPrefab, false));
        waves[6].Add((smallEnemySpawerPrefab, false));
        waves[6].Add((smallEnemySpawerPrefab, false));
        waves[6].Add((smallEnemySpawerPrefab, false));
        waves[6].Add((smallEnemySpawerPrefab, false));

        // wave 8
        waveTimes[7] = 134;
        waves[7] = new List<(EnemySpawner, bool)>();
        waves[7].Add((enemySpawerPrefab, false));
        waves[7].Add((enemySpawerPrefab, false));
        waves[7].Add((enemySpawerPrefab, false));
        waves[7].Add((smallEnemySpawerPrefab, false));
        waves[7].Add((smallEnemySpawerPrefab, false));
    }

    void Update()
    {
        if (waveNumber < waveCount){
            float elapsed = Time.time - MainController.INSTANCE.startTime;
            if (elapsed > waveTimes[waveNumber]) {
                // spawn wave
                foreach ((EnemySpawner, bool) entry in waves[waveNumber]) {
                    bool respawnable = entry.Item2;
                    EnemySpawner spawner = entry.Item1;
                    if (respawnable) {
                        SpawnerGroup spawnerGroup = Instantiate(spawnerGroupPrefab, transform);
                        spawnerGroup.spawnerPrefab = spawner.gameObject;
                    } else {
                        Instantiate(spawner, transform);
                    }
                }

                // next wave
                waveNumber++;
            }
        }
    }
}
