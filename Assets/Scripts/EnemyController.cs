using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float range = 20;
    public LayerMask playerLayer;
    private Rigidbody2D enemyRb;
    private float enemyMoveSpeed = 3;

    public float health = 100;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
        
    }

    private void FixedUpdate()
    {
        ChasingPlayer();

    }

    void ChasingPlayer()
    {
        /*if (player == null)
            return;*/

        Collider2D[] playerCol = Physics2D.OverlapCircleAll(this.transform.position, range, playerLayer);

        foreach (Collider2D col in playerCol)
        {
            enemyRb.MovePosition(this.transform.position + (col.gameObject.transform.position - this.transform.position).normalized * enemyMoveSpeed * Time.deltaTime);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, range);
    }
}
