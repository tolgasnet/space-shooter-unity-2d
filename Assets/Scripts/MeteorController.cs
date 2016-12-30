using System;
using System.Collections;
using UnityEngine;

public class MeteorController : MonoBehaviour
{
    public Animator explosion;
    public Rigidbody2D player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "Player")
        {
            Destroy(gameObject);
        }
        else if (other.name.Contains("Bullet"))
        {
            MeteorExplosion(other);
        }
    }

    private void MeteorExplosion(Collider2D bulletCollider)
    {
        Destroy(gameObject);
        Destroy(bulletCollider.gameObject);

        CreateExplosion(gameObject);
    }

    private void CreateExplosion(GameObject gameObject, int explosionCount = 1)
    {
        var explosionClone = Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);

        var explosionLength = explosionClone.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        Destroy(explosionClone.gameObject, explosionLength * explosionCount);
    }
}
