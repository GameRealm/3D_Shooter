using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float lifeTime = 3f;
    public float explosionForce = 10f;
    public float explosionRadius = 3f;
    public float bonusTimeAmount = 10f;

    // ДОДАЙ ЦІ ДВА РЯДКИ:
    public float flySpeed = 50f;
    private Rigidbody rb;

    void Start()
    {
        // Отримуємо посилання на Rigidbody відразу
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, lifeTime);
    }

    // ДОДАЙ ЦЕЙ МЕТОД ПОВНІСТЮ:
    public void Launch()
    {
        if (rb == null) rb = GetComponent<Rigidbody>();

        // Надаємо швидкість ТІЛЬКИ коли цей метод викликано
        rb.linearVelocity = transform.forward * flySpeed;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("SpawnZone")) return;

        if (other.gameObject.layer == LayerMask.NameToLayer("Target"))
        {
            if (other.CompareTag("Bonus"))
            {
                AddBonusTime();
            }
            else
            {
                if (other.CompareTag("Simple"))
                    ScoreManager.instance.AddScore(1, "Simple");
                else if (other.CompareTag("Medium"))
                    ScoreManager.instance.AddScore(2, "Medium");
                else if (other.CompareTag("Hard"))
                    ScoreManager.instance.AddScore(5, "Hard");
            }

            Destroy(other.gameObject);
        }

        Explode();
        Destroy(gameObject);
    }

    private void AddBonusTime()
    {
        Timer gameTimer = Object.FindFirstObjectByType<Timer>();
        if (gameTimer != null)
        {
            gameTimer.time += bonusTimeAmount;
            if (ScoreManager.instance != null) ScoreManager.instance.AddBonusCount();
        }
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider hit in colliders)
        {
            Rigidbody targetRb = hit.GetComponent<Rigidbody>();
            if (targetRb != null)
            {
                targetRb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
        }
    }
}