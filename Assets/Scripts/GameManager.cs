using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

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

        if (target.gameObject.GetComponent<PlayerController>().deadByEnemy)
        {
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
        else
        {
            GameObject player = Instantiate(playerPrefabs, playerStartPoint.position, playerPrefabs.transform.rotation);
            player.name = "Player";
            players.Add(player);

            Destroy(players[0]);
            players.RemoveAt(0);

            SetTarget();
            canSwitchPlayable = true;
        }
    }

    void SetPlayerPlayable()
    {
        if (Input.GetKeyDown(KeyCode.E) && players.Count >= 2 && canSwitchPlayable)
        {
            SoundManager.Instance.PlaySound(SoundName.PlayerChange);

            foreach (GameObject player in players)
            {
                //player.GetComponent<PlayerController>().playable = !player.GetComponent<PlayerController>().playable;

                if (player.layer == 6)
                {
                    player.layer = LayerMask.NameToLayer("Player");
                    player.GetComponent<PlayerController>().playable = true;
                    player.GetComponent<PlayerController>().health = 100;
                }
                else
                {
                    player.layer = LayerMask.NameToLayer("Corpse");
                    player.GetComponent<PlayerController>().playable = false;
                    player.GetComponent<PlayerController>().health = 0;
                }
            }

            SetTarget();
        }
    }
}
