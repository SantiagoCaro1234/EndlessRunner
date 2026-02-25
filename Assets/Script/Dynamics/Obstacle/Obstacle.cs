using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacle : MonoBehaviour
{
    [SerializeField] protected int obstacleDamage;

    [SerializeField] protected float timeUntilDespawn = 3f;

    BoxCollider2D boxCollider;

    private void Awake()
    {
        Initialize();
    }

    protected virtual void Initialize()
    {
        boxCollider = GetComponentInChildren<BoxCollider2D>();
        DespawnAfterSeconds(timeUntilDespawn);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<IDamageable>().TakeDamage(obstacleDamage);
        }

        boxCollider.enabled = false;
        OnCollisionVFX();
    }

    protected virtual void DespawnAfterSeconds(float seconds)
    {
        StartCoroutine(DespawnCoroutine(seconds));
    }
    protected abstract void OnCollisionVFX();
    protected IEnumerator DespawnCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(this.gameObject);
    }
}
