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
    [SerializeField] private int playersPlayable;
    private bool canSwitchPlayable = true;

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

        SetPlayerPlayable();
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
        canSwitchPlayable = false;
        yield return new WaitForSeconds(3);
        GameObject player = Instantiate(playerPrefabs, playerStartPoint.position, playerPrefabs.transform.rotation);
        player.name = "Player";
        players.Add(player);

        if (players.Count > playersPlayable)
        {
            
            Destroy(players[0]);
            players.RemoveAt(0);
        }

        SetTarget();
        canSwitchPlayable = true;
    }

    void SetPlayerPlayable()
    {
        if (Input.GetKeyDown(KeyCode.E) && players.Count >= 2 && canSwitchPlayable)
        {
            foreach (GameObject player in players)
            {
                player.GetComponent<PlayerController>().playable = !player.GetComponent<PlayerController>().playable;
            }

            SetTarget();
        }
    }
}
