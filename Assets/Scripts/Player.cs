using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpForce = 4f;
    public float flySpeed = 4f;

    bool tapped = false;
    Rigidbody2D _rb2D;

    public ParticleSystem prefabExplosion;

    // Start is called before the first frame update
    void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();
    }

    public void OnTap()
    {
        tapped = true;
        _rb2D.velocity = new Vector2(_rb2D.velocity.x, jumpForce);
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(transform.position.y) > 7)
        {
            OnCrash();
        }

        var vel = _rb2D.velocity;
        if(tapped)
        {
            vel.y = jumpForce;
            tapped = false;
        }

        vel.x = flySpeed;

        _rb2D.velocity = vel;
    }

    void OnCrash()
    {
        Instantiate(prefabExplosion, transform.position, transform.rotation);
        Destroy(gameObject, 0.1f);
        Game.current.GameOver();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Obstacle"))
        {
            OnCrash();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            other.GetComponent<CollectableBase>()?.Collect();
        }
    }
}
