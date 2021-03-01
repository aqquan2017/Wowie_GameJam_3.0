using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector2 playerDir;
    private Rigidbody2D playerRb;
    public float moveSpeed = 5;

    private Vector2 mousePos;
    private Camera cam;

    public GameObject bulletPrefabs;
    public Transform bulletStartPoint;
    private float bulletMoveSpeed = 10;

    public bool playable = true;
    public bool deadByEnemy;
    public bool enemyCanAttack = true;

    public GameObject globalLight;
    public GameObject pointLight;

    public float health = 100;
    public ParticleSystem bloodPartical;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        playerDir.x = Input.GetAxisRaw("Horizontal");
        playerDir.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        Shooting();

        

        if (playable)
        {
            globalLight.SetActive(true);
        }
        else
        {
            globalLight.SetActive(false);
        }
            

    }

    private void FixedUpdate()
    {
        if (playable)
        {
            playerRb.MovePosition(playerRb.position + playerDir * moveSpeed * Time.deltaTime);

            Vector2 lookDir = mousePos - (Vector2)this.transform.position;
            transform.up = lookDir;
        }
        playerRb.velocity = Vector2.Lerp(playerRb.velocity, Vector2.zero, Time.deltaTime);
    }

    void Shooting()
    {
        if (playable)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SoundManager.Instance.PlaySound(SoundName.PlayerShot);

                CameraCinemachineShake.Instance.SetShake(150f, 0.7f);
                GameObject bullet = Instantiate(bulletPrefabs, bulletStartPoint.position, this.transform.rotation);
                bullet.GetComponent<Bullet>().fromPlayer = true;
                Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

                Vector2 bulletDir = (Vector2) bullet.transform.up;

                //get random direction;
                if(bulletDir.x < bulletDir.y)
                {
                    bulletDir.x = Random.Range(bulletDir.x - 0.1f, bulletDir.x + 0.1f);
                }
                else
                {
                    bulletDir.y = Random.Range(bulletDir.y - 0.1f, bulletDir.y + 0.1f);
                }

                bulletRb.AddForce(bulletDir * bulletMoveSpeed, ForceMode2D.Impulse);
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playable)
        {
            if (collision.gameObject.tag == "Electric Wall")
            {
                GameManager.Instance.isDead = true;
                Destroy(this.gameObject);
            }
        }
        else
        {
            if (collision.gameObject.tag == "Bullet")
            {
                Debug.Log("GO");
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (playable)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                health = 0;
                SoundManager.Instance.PlaySound(SoundName.PlayerDie);

                GameManager.Instance.isDead = true;
                deadByEnemy = true;

                Color deadColor = this.GetComponent<SpriteRenderer>().color;
                deadColor.a = 30;
                this.GetComponent<SpriteRenderer>().color = deadColor;

                playable = false;

                gameObject.layer = LayerMask.NameToLayer("Corpse");
            }

            if (collision.gameObject.tag == "Bullet")
            {
                health -= 20;
                if (health <= 0)
                {
                    SoundManager.Instance.PlaySound(SoundName.PlayerDie);

                    GameManager.Instance.isDead = true;
                    deadByEnemy = true;

                    Color deadColor = this.GetComponent<SpriteRenderer>().color;
                    deadColor.a = 30;
                    this.GetComponent<SpriteRenderer>().color = deadColor;

                    playable = false;

                    gameObject.layer = LayerMask.NameToLayer("Corpse");
                }
            }

            if (collision.gameObject.tag == "Electric Wall")
            {
                SoundManager.Instance.PlaySound(SoundName.PlayerDie);

                GameManager.Instance.isDead = true;
                deadByEnemy = false;

                playable = false;

                gameObject.layer = LayerMask.NameToLayer("Corpse");
            }
        }
        else
        {
            
        }
    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.gameObject.name == "Box1")
    //    {
    //        enemyCanAttack = collision.gameObject.name;
    //    }
    //    else if (collision.gameObject.name == "Box2")
    //    {
    //        enemyCanAttack = collision.gameObject.name;
    //    }
    //}

}
