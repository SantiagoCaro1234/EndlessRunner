using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleSpike : Obstacle
{

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
    protected override void OnCollisionFX()
    {
        print($"{gameObject.name} VFX's");
    }
}


