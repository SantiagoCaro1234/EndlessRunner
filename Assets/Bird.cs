using UnityEngine;

public class Bird : Obstacle
{
    [Header("Movimiento sinusoidal")]
    [SerializeField] private float amplitude = 1f;   // altura de la onda
    [SerializeField] private float frequency = 2f;   // velocidad de la onda

    private float startY;
    private float time;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void OnSpawn()
    {
        base.OnSpawn();
        startY = transform.position.y;
        time = 0f;
    }

    private void Update()
    {
        if (!GameManager.Instance.isPlaying) return;

        time += Time.deltaTime * frequency;
        float yOffset = Mathf.Sin(time) * amplitude;
        Vector3 pos = transform.position;
        pos.y = startY + yOffset;
        transform.position = pos;
    }

    protected override void OnCollisionFX()
    {
        Debug.Log("Bird collision FX");
    }
}