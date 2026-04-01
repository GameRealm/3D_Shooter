using UnityEngine;

public class BonusTarget : BaseTarget
{
    [Header("Налаштування бонусу")]
    public float secondsToAdd = 10f;

    protected override void Start() { }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("SpawnZone")) return;

        if (other.gameObject.layer == LayerMask.NameToLayer("Target"))
        {
            BaseTarget target = other.GetComponent<BaseTarget>();

            if (target != null)
            {
                target.OnHit();
            }
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}