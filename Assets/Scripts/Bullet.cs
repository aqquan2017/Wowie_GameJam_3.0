﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void Start()
    {
        Destroy(this.gameObject, 5);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.IsTouchingLayers(LayerMask.NameToLayer("Wall"))){
            SoundManager.Instance.PlaySound(SoundName.WallHit);
        }

        if (collision.collider.IsTouchingLayers(LayerMask.NameToLayer("Enemy")))
        {
            SoundManager.Instance.PlaySound(SoundName.EnemyHit);
        }

        Destroy(gameObject);
    }


}
