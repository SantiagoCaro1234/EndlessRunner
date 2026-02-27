using UnityEngine;
using System;

public class PlayerStats : MonoBehaviour, IDamageable
{
    [SerializeField] public int healthPoints = 3;
    [SerializeField] private bool isAlive = true;

    public event Action OnDamageTaken;

    public void TakeDamage(int damageTaken)
    {
        if (healthPoints <= 0) return;

        healthPoints -= damageTaken;
        OnDamageTaken?.Invoke();

        if (healthPoints <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        if (!isAlive) return;
        isAlive = false;
        GameManager.Instance.GameOver();
        Debug.Log($"{gameObject.name} has Died");
    }
}