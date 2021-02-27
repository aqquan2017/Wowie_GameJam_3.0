using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemyPrefabs;
    [SerializeField] private float timeToSpawn;
    private float time = 0;

    void Start()
    {
        time = timeToSpawn;
    }

    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            GameObject enemy = Instantiate(enemyPrefabs, this.transform.position, Quaternion.identity);
            time = timeToSpawn;
        }
    }

}
