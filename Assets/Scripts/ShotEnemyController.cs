using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotEnemyController : MonoBehaviour
{
    [SerializeField] private float range = 20;
    public LayerMask playerLayer;
    private Rigidbody2D enemyRb;
    [SerializeField] private float enemyMoveSpeed = 3;

    public float shotPlayerDuration { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent <Rigidbody2D > ();
    }

    // Update is called once per frame
    void Update()
    {
        ChasePlayer();

        ShotPlayer();
    }

    void ShotPlayer()
    {

        Collider2D[] playerCol = Physics2D.OverlapCircleAll(this.transform.position, range, playerLayer);
        foreach (Collider2D col in playerCol)
        {
            if (col.gameObject.GetComponent<PlayerController>().playable)
            {
                enemyRb.MovePosition(this.transform.position + (col.gameObject.transform.position - this.transform.position).normalized * enemyMoveSpeed * Time.deltaTime);

            }
        }
    }

    void ChasePlayer()
    {
    }
}
