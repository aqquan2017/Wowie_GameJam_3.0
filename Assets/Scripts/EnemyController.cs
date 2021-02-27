using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float range = 20;
    public LayerMask playerLayer;
    private Rigidbody2D enemyRb;
    [SerializeField] private float enemyMoveSpeed = 3;

    private SpriteRenderer spite;

    private float health = 100;
    private GameManager gameManager;
    private GameObject target;

    public Transform spawnPos;

    // Start is called before the first frame update
    void Start()
    {
        spite = GetComponent<SpriteRenderer>();
        enemyRb = GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }

        enemyMoveSpeed = Mathf.Lerp(enemyMoveSpeed, 3, 0.2f);
        
    }

    private void FixedUpdate()
    {
        ChasingPlayer();
        if (!gameManager.target.GetComponent<PlayerController>().playable)
        {
            ReturnToStartPos();
        }
    }

    void ChasingPlayer()
    {
        /*if (player == null)
            return;*/

        Collider2D[] playerCol = Physics2D.OverlapCircleAll(this.transform.position, range, playerLayer);

        foreach (Collider2D col in playerCol)
        {
            if (col.gameObject.GetComponent<PlayerController>().playable)
            {
                enemyRb.MovePosition(this.transform.position + (col.gameObject.transform.position - this.transform.position).normalized * enemyMoveSpeed * Time.deltaTime);

            }
        }
    }

    public void ReturnToStartPos()
    {
        if ((spawnPos.position - this.transform.position).magnitude <= 0.1f)
            return;

        enemyRb.MovePosition(this.transform.position + (spawnPos.position - this.transform.position).normalized * enemyMoveSpeed * Time.deltaTime);

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, range);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            spite.color = new Color(Random.value, Random.value, Random.value, 1);
            enemyRb.AddForce(collision.relativeVelocity * 20);

            enemyMoveSpeed = 0;
            health -= 20;

        }
    }
}
