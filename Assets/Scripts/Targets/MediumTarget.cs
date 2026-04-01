using UnityEngine;

public class MediumTarget : BaseTarget
{
    [Header("Налаштування руху")]
    public float speed = 2f;
    public float radius = 3f;

    [Header("Ефект лопання")]
    public float popScaleMultiplier = 1.5f;
    public float popDuration = 0.5f;

    private Vector3 startPos;
    private float currentLifetime;

    protected override void Start()
    {
        lifetime = 3f;
        currentLifetime = lifetime;
        base.Start();
        startPos = transform.position;
    }

    void Update()
    {

        float x = Mathf.Sin(Time.time * speed) * radius;
        float y = Mathf.Cos(Time.time * speed) * radius;
        transform.position = startPos + new Vector3(x, y, 0);

        currentLifetime -= Time.deltaTime;
        if (currentLifetime <= popDuration && currentLifetime > 0)
        {
            float progress = 1f - (currentLifetime / popDuration);
            transform.localScale = Vector3.one * Mathf.Lerp(1f, popScaleMultiplier, progress);
        }
    }
}