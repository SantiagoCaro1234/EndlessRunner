using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Igloo : Obstacle
{
    protected override void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMovement player = collision.GetComponent<PlayerMovement>();
            if (player != null)
            {
                if (player.IsSliding)
                {
                    // prolongar slide mientras esté dentro
                    player.SetSlideProlongation(true);
                }
                else
                {
                    // delegar en la base: aplica daño, efectos y retorno con retraso
                    base.OnTriggerEnter2D(collision);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMovement player = collision.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player.SetSlideProlongation(false);
            }
        }
    }

    protected override void OnCollisionFX()
    {
        // sfx
    }
}
