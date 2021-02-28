using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private SoundManager soundManager;

    void Start()
    {
        Destroy(this.gameObject, 5);
        soundManager = GameObject.Find("Sound Manager").GetComponent<SoundManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.IsTouchingLayers(LayerMask.NameToLayer("Wall"))){
            soundManager.PlaySound(SoundName.WallHit);
        }

        if (collision.collider.IsTouchingLayers(LayerMask.NameToLayer("Enemy")))
        {
            soundManager.PlaySound(SoundName.EnemyHit);
        }

        Destroy(gameObject);
    }


}
