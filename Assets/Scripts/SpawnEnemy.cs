using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemyPrefabs;
    private float timeToSpawn = 5;
    public float timeRemaining;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeRemaining += Time.deltaTime;
        if (timeRemaining >= timeToSpawn)
        {
            GameObject enemy = Instantiate(enemyPrefabs, this.transform.position, Quaternion.identity);
            timeRemaining = 0;
        }
    }

}
