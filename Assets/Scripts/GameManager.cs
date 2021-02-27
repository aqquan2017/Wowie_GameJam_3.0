using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefabs;
    public Transform playerStartPoint;

    public bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            StartCoroutine(RespawnPlayer());
            isDead = false;
        }
    }

    IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(3);
        GameObject player = Instantiate(playerPrefabs, playerStartPoint.position, playerPrefabs.transform.rotation);
        player.name = "Player";
    }
}
