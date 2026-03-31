using UnityEngine;

public class BaseTarget : MonoBehaviour
{
    public int scoreValue = 1;
    public float lifetime = 5f;

    protected virtual void Start()
    {
        // Всі цілі зникають через певний час
        Destroy(gameObject, lifetime);
    }

    // Метод, який викликає фаєрбол
    public virtual void OnHit()
    {
        Destroy(gameObject);
    }

}