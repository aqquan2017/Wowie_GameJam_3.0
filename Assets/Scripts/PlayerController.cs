using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector2 playerDir;
    private Rigidbody2D playerRb;
    public float moveSpeed = 5;

    private Vector2 mousePos;
    public Camera cam;

    public GameObject bulletPrefabs;
    public Transform bulletStartPoint;
    private float bulletMoveSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        playerDir.x = Input.GetAxisRaw("Horizontal");
        playerDir.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        Shooting();
    }

    private void FixedUpdate()
    {
        playerRb.MovePosition(playerRb.position + playerDir * moveSpeed * Time.deltaTime);

        Vector2 lookDir = mousePos - (Vector2)this.transform.position;
        float lookAngle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90;

        playerRb.rotation = lookAngle;
    }

    void Shooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject bullet = Instantiate(bulletPrefabs, bulletStartPoint.position, this.transform.rotation);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.AddForce(bullet.transform.up * bulletMoveSpeed, ForceMode2D.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            playerRb.AddForce(Vector2.up * 15, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Player Death");
        playerRb.AddForce(Vector2.up * 150, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Electric Wall")
        {

        }
        else if (collision.gameObject.tag == "Enemy")
        {

        }

    }

    void ReSpawnPlayer()
    {

    }
}
