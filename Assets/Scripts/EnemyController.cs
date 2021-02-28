using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    protected float range = 20;
    public LayerMask playerLayer;
    protected Rigidbody2D enemyRb;
    [SerializeField] protected float enemyMoveSpeed = 3;

    protected SpriteRenderer sprite;

    protected float health = 100;
    private GameObject target;

    public Vector3 spawnPos;

    protected Color currentColor;
    public ParticleSystem bloodPartical;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        spawnPos = transform.position;
        sprite = GetComponent<SpriteRenderer>();
        currentColor = sprite.color;
        enemyRb = GetComponent<Rigidbody2D>();

    }

    protected virtual void Update()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
            SoundManager.Instance.PlaySound(SoundName.EnemyDie);
            Instantiate(bloodPartical, this.transform.position, Quaternion.identity);
            //bloodPartical.Play();
        }

        enemyMoveSpeed = Mathf.Lerp(enemyMoveSpeed, 3, 0.5f * Time.deltaTime);
        sprite.color = Color.Lerp(sprite.color, currentColor, 10f * Time.deltaTime);
        enemyRb.velocity = Vector2.Lerp(enemyRb.velocity, Vector2.zero, 0.9f);
    }

    protected virtual void FixedUpdate()
    {
        ChasingPlayer();
        if (!GameManager.Instance.target.GetComponent<PlayerController>().playable)
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
        if ((spawnPos - this.transform.position).magnitude <= 0.1f)
            return;

        enemyRb.MovePosition(this.transform.position + (spawnPos - this.transform.position).normalized * enemyMoveSpeed * Time.deltaTime);

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
            sprite.color = Color.white;
            enemyRb.AddForce(collision.relativeVelocity * 5, ForceMode2D.Impulse);

            enemyMoveSpeed = 0;
            health -= 20;

        }
    }
}
