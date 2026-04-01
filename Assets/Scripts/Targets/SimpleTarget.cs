using UnityEngine;

public class SimpleTarget : BaseTarget
{
    private Vector3 initialScale;

    protected override void Start()
    {
        base.Start();
        initialScale = transform.localScale;
    }

    void Update()
    {
        float timeToLive = lifetime; 
        lifetime -= Time.deltaTime;

        if (lifetime <= 1f && lifetime > 0)
        {
            transform.localScale = Vector3.Lerp(Vector3.zero, initialScale, lifetime);
        }
    }

    public override void OnHit()
    {
        base.OnHit(); 
    }
}