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
        if (collision.gameObject.tag == "Wall")
        {
            soundManager.PlaySound(2);
        }
        if (collision.gameObject.tag == "Enemy")
        {
            soundManager.PlaySound(3);
        }

        Destroy(gameObject);
    }


}
