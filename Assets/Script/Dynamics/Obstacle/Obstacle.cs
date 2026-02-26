using System.Collections;
using UnityEngine;

public abstract class Obstacle : MonoBehaviour
{
    [SerializeField] protected int obstacleDamage;
    [SerializeField] protected float timeUntilDespawn = 3f; // tiempo maximo de vida
    [SerializeField] protected float timeUntilDespawnAfterHit = 0.5f;

    protected BoxCollider2D boxCollider;
    protected Rigidbody2D rb;
    private ObstaclePool pool;
    private Coroutine despawnCoroutine;
    private Coroutine returnAfterHitCoroutine;

    protected virtual void Awake()
    {
        // obtener referencias de componentes
        boxCollider = GetComponentInChildren<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    // llamado por el pool cuando se instancia o se reutiliza
    public void Init(ObstaclePool owner)
    {
        pool = owner;
    }

    // llamado por el pool cuando se activa el obstaculo
    public virtual void OnSpawn()
    {
        // habilitar collider
        if (boxCollider != null)
            boxCollider.enabled = true;

        if (returnAfterHitCoroutine != null)
        {
            StopCoroutine(returnAfterHitCoroutine);
            returnAfterHitCoroutine = null;
        }

        // iniciar corrutina de despawn
        if (despawnCoroutine != null)
            StopCoroutine(despawnCoroutine);
        despawnCoroutine = StartCoroutine(DespawnAfterTime());
    }

    // cuando el obstaculo sale de la pantalla
    private void OnBecameInvisible()
    {
        ReturnToPool();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var damageable = collision.GetComponent<IDamageable>();
            if (damageable != null)
                damageable.TakeDamage(obstacleDamage);

            // desactivar collider para evitar multiples golpes
            if (boxCollider != null)
                boxCollider.enabled = false;

            // detener la corrutina de despawn por tiempo (ya no es necesaria)
            if (despawnCoroutine != null)
            {
                StopCoroutine(despawnCoroutine);
                despawnCoroutine = null;
            }

            // efectos de colision
            OnCollisionFX();

            // iniciar corrutina para devolver al pool despues de un breve retraso
            if (returnAfterHitCoroutine != null)
                StopCoroutine(returnAfterHitCoroutine);
            returnAfterHitCoroutine = StartCoroutine(ReturnAfterHit());
        }
    }

    protected abstract void OnCollisionFX();

    private IEnumerator DespawnAfterTime()
    {
        yield return new WaitForSeconds(timeUntilDespawn);
        ReturnToPool();
    }

    private IEnumerator ReturnAfterHit()
    {
        yield return new WaitForSeconds(timeUntilDespawnAfterHit);
        returnAfterHitCoroutine = null;
        ReturnToPool();
    }

    protected void ReturnToPool()
    {
        // detener cualquier corrutina activa
        if (despawnCoroutine != null)
        {
            StopCoroutine(despawnCoroutine);
            despawnCoroutine = null;
        }
        if (returnAfterHitCoroutine != null)
        {
            StopCoroutine(returnAfterHitCoroutine);
            returnAfterHitCoroutine = null;
        }

        if (rb != null)
            rb.velocity = Vector2.zero;

        pool?.ReturnObstacle(this);
    }

    private void OnDestroy()
    {
        if (despawnCoroutine != null)
            StopCoroutine(despawnCoroutine);
        if (returnAfterHitCoroutine != null)
            StopCoroutine(returnAfterHitCoroutine);
    }
}