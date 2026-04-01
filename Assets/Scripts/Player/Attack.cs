using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    [Header("Налаштування магії")]
    public GameObject fireballPrefab;
    public Transform spawnPoint;
    public float fireRate = 0.5f;

    [Header("Налаштування звуку")]
    public AudioSource audioSource;    
    public AudioClip fireballSound;  

    private float nextFireTime;
    private InputActions controls;
    private Animator anim;

    private void Awake()
    {
        controls = new InputActions();
        anim = GetComponent<Animator>();
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        controls.Enable();
        controls.Player.Fire.performed += context => StartAttack();
        controls.Player.Fire.canceled += context => EndAttack();
    }

    private void OnDisable() => controls.Disable();

    private void StartAttack()
    {
        if (Time.time >= nextFireTime)
        {
            anim.SetFloat("IsCasting", 1f);
            anim.SetTrigger("Cast");
            nextFireTime = Time.time + fireRate;
        }
    }

    private void EndAttack()
    {
        anim.SetFloat("IsCasting", 0f);
    }

    public void ShootFireball()
    {
        if (audioSource != null && fireballSound != null)
        {
            audioSource.PlayOneShot(fireballSound);
        }

        float screenHeight = Screen.height;
        float aimY = 0.5f + (250f / screenHeight);

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, aimY, 0));
        RaycastHit hit;
        Vector3 targetPoint;

        if (Physics.Raycast(ray, out hit, 100f))
        {
            if (Vector3.Distance(spawnPoint.position, hit.point) < 2f)
                targetPoint = ray.GetPoint(100f);
            else
                targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(100f);
        }

        GameObject ball = Instantiate(fireballPrefab, spawnPoint.position, Quaternion.identity);
        ball.transform.LookAt(targetPoint);
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = ball.transform.forward * 40f;
        }
    }
}