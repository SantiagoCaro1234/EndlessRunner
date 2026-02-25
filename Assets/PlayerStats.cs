using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IDamageable
{
    [SerializeField] private int healthPoints = 5;
    [SerializeField] private bool isAlive = true;

    public void TakeDamage(int damageTaken)
    {
        if (healthPoints <= 0) Die();
        else healthPoints -= damageTaken;
    }

    public void Die()
    {
        Debug.Log($"{gameObject.name} has Died");
    }
}
