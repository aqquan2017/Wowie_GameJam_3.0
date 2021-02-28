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

                    Debug.DrawLine(transform.position, transform.position + transform.up * range, Color.red);
                if(rechargeDuration <= 0f)
                {
                    Ray r = new Ray(transform.position, transform.up);
                    //if (Physics.Raycast(r, range, playerLayer))
                    //{
                        Vector3 rotateVectorLeft = Quaternion.Euler(0, 0, 45) * transform.up;  
                        Vector3 rotateVectorRight = Quaternion.Euler(0, 0, -45) * transform.up;

                        GameObject bullet1 = Instantiate(enemyBullet, transform.position + rotateVectorLeft * 2, transform.rotation);
                        GameObject bullet2 = Instantiate(enemyBullet, transform.position + rotateVectorRight * 2, transform.rotation);
                        GameObject bullet3 = Instantiate(enemyBullet, transform.position + transform.up * 2, transform.rotation);

                    bullet1.GetComponent<Bullet>().fromPlayer = false;
                    bullet2.GetComponent<Bullet>().fromPlayer = false;
                    bullet3.GetComponent<Bullet>().fromPlayer = false;

                    bullet1.layer = LayerMask.NameToLayer("TransparentFX");
                    bullet2.layer = LayerMask.NameToLayer("TransparentFX");
                    bullet3.layer = LayerMask.NameToLayer("TransparentFX");

                    bullet1.GetComponent<Rigidbody2D>().AddForce(rotateVectorLeft * enemyBulletSpeed, ForceMode2D.Impulse);
                    bullet2.GetComponent<Rigidbody2D>().AddForce(rotateVectorRight * enemyBulletSpeed, ForceMode2D.Impulse);
                    bullet3.GetComponent<Rigidbody2D>().AddForce(transform.up * enemyBulletSpeed, ForceMode2D.Impulse);
                    print("ENEMY SHOT");

                        rechargeDuration = shotPlayerDuration;
                    //}
                    //else
                    //{
                    //    print("ERR");
                    //}

                }
            }
        }

    }
}
