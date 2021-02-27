using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float range = 10;
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
        enemyRb.MovePosition(this.transform.position + (player.transform.position - this.transform.position).normalized * enemyMoveSpeed * Time.deltaTime);
    }
}
