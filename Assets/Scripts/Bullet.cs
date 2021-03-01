using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public ParticleSystem littleExposePartical;
    public bool fromPlayer;

    void Start()
    {
        Destroy(this.gameObject, 5);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.IsTouchingLayers(LayerMask.NameToLayer("Enemy")))
        {
            SoundManager.Instance.PlaySound(SoundName.EnemyHit);
            ParticleSystem littleExpose = Instantiate(littleExposePartical, this.transform.position, Quaternion.identity);
            Destroy(littleExpose.gameObject, 4);
        }

        //if (collision.collider.IsTouchingLayers(LayerMask.NameToLayer("Enemy")))
        //{
        //    SoundManager.Instance.PlaySound(SoundName.EnemyHit);
        //    ParticleSystem littleExpose = Instantiate(littleExposePartical, this.transform.position, Quaternion.identity);
        //    Destroy(littleExpose.gameObject, 4);
        //}

        if (collision.collider.IsTouchingLayers(LayerMask.NameToLayer("Wall")))
        {
            SoundManager.Instance.PlaySound(SoundName.WallHit);
            ParticleSystem littleExpose = Instantiate(littleExposePartical, this.transform.position, Quaternion.identity);
            Destroy(littleExpose.gameObject, 4);
        }

        Destroy(gameObject);
    }


}
