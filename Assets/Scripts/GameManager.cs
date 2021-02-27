using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefabs;
    public Transform playerStartPoint;

    public bool isDead = false;

    [SerializeField] private CameraCinemachineTrack cameraCinemachineTrack;

    public List<GameObject> players = new List<GameObject>();
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        SetTarget();
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

    void SetTarget()
    {
        foreach (GameObject player in players)
        {
            if (player.GetComponent<PlayerController>().playable)
            {
                target = player;
            }
        }
    }

    IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(3);
        GameObject player = Instantiate(playerPrefabs, playerStartPoint.position, playerPrefabs.transform.rotation);
        player.name = "Player";
        players.Add(player);

        SetTarget();
    }
}
