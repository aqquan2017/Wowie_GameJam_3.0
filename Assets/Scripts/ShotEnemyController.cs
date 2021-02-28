using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotEnemyController : EnemyController
{
    public float enemyBulletSpeed = 3;
    public float shotPlayerDuration = 4f;
    private float rechargeDuration;

    public GameObject enemyBullet;


    protected override void Start()
    {
        base.Start();
        rechargeDuration = shotPlayerDuration;
    }

    protected override void Update()
    {
        base.Update();
        if (rechargeDuration > 0f)
            rechargeDuration -= Time.deltaTime; 
        //ChasePlayer();

        ShotPlayer();

    }

    protected override void FixedUpdate()
    {

    }
    void ShotPlayer()
    {
        Collider2D[] playerCol = Physics2D.OverlapCircleAll(this.transform.position, range, playerLayer);
        foreach (Collider2D col in playerCol)
        {
            if (col.gameObject.GetComponent<PlayerController>().playable)
            {
                Vector2 thisToPlayerDir = col.transform.position - transform.position;
                transform.up = thisToPlayerDir;

                if(rechargeDuration <= 0f)
                {
                    Debug.DrawLine(transform.position, transform.position + transform.up * range, Color.red);
                    //if(Physics.Raycast(transform.position, transform.up, detectRange, playerLayer))
                    //{
                        GameObject bullet = Instantiate(enemyBullet, transform.position + transform.up * 2, transform.rotation);

                        bullet.GetComponent<Rigidbody2D>().AddForce(transform.up * enemyBulletSpeed, ForceMode2D.Impulse);
                        print("ENEMY SHOT");

                        rechargeDuration = shotPlayerDuration;
                    //}

                }
            }
        }

    }
}
