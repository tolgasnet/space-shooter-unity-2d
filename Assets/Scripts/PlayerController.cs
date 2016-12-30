using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const float bulletOffset = 0.1f;
    private const float bulletDestroyTime = 0.7f;

    public float speed = 7.0F;
    public float bulletSpeed = 15f;
    public Animator explosion;
    public Rigidbody2D bullet;
    private Rigidbody2D body;
    private Vector3 offSet = new Vector3(0, 15f, 0);
    private bool canMove = true;

    // Use this for initialization
    private void Start ()
    {
        Debug.Log("Started game");
        body = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate ()
    {
        if(canMove)
        {
            float horizontalMove = Input.GetAxis("Horizontal");
            float verticalMove = Input.GetAxis("Vertical");
            var movement = new Vector2(horizontalMove, verticalMove);
            body.velocity = movement * speed;
        }
        else
        {
            body.velocity = new Vector2(0,0);
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            FireBullet();
        }
    }

    private void FireBullet()
    {
        var bulletPosition = transform.position + new Vector3(0, bulletOffset);
        var bulletClone = Instantiate(bullet, bulletPosition, transform.rotation);
        bulletClone.velocity = new Vector2(0, bulletSpeed);
        Destroy(bulletClone.gameObject, bulletDestroyTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name.Contains("Meteor"))
        {
            PlayerExplosion(other);
        }
    }

    private void PlayerExplosion(Collider2D enemy)
    {
        CreateExplosion(gameObject, explosionCount: 3);

        gameObject.transform.position += offSet;
        canMove = false;

        StartCoroutine(RespawnPlayer());
    }

    private IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(3);
        gameObject.transform.position -= offSet;
        canMove = true;
        Debug.Log("respawn");
    }

    private void CreateExplosion(GameObject gameObject, int explosionCount = 1)
    {
        var explosionClone = Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);

        var animationLength = explosionClone.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        Destroy(explosionClone.gameObject, animationLength * explosionCount);
    }
}
