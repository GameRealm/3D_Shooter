using UnityEngine;

public class BaseTarget : MonoBehaviour
{
    public int scoreValue = 1;
    public float lifetime = 5f;
    [Header("Звук при влучанні")]
    public AudioClip hitSound;
    [Range(0f, 1f)]
    public float volume = 0.7f;

    protected virtual void Start()
    {
        Destroy(gameObject, lifetime);
    }

    public virtual void OnHit()
    {
        PlayHitSound(); 
        Destroy(gameObject);
    }

    protected void PlayHitSound()
    {
        if (hitSound != null)
        {
            AudioSource.PlayClipAtPoint(hitSound, transform.position, volume);
        }
    }
}