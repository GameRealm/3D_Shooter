using UnityEngine;
using UnityEngine.AI;

public class NavMeshBoundary : MonoBehaviour
{
    private CharacterController controller;

    [Header("Налаштування кордону")]
    public float checkRadius = 0.5f;
    public float searchRadius = 10.0f; 
    public float pushForce = 8.0f;    

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        NavMeshHit hit;

        if (!NavMesh.SamplePosition(transform.position, out hit, checkRadius, NavMesh.AllAreas))
        {
            if (NavMesh.SamplePosition(transform.position, out hit, searchRadius, NavMesh.AllAreas))
            {
                Vector3 directionToLand = (hit.position - transform.position).normalized;
                directionToLand.y = 0;

                controller.Move(directionToLand * pushForce * Time.deltaTime);

                Debug.DrawLine(transform.position, hit.position, Color.red);
            }
        }
    }
}