using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float repeatSpawnDelay = 3;
    [SerializeField] private float startSpawningDelay = 3;
    void Start()
    {
        InvokeRepeating("Spawn", startSpawningDelay, repeatSpawnDelay);
    }

    void Spawn()
    {
        Instantiate(enemyPrefab, transform.position, transform.rotation);
    }
    
}
