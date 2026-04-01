using UnityEngine;

public class AwaySpiralTarget : BaseTarget
{
    [Header("Налаштування спіралі від гравця")]
    public float spiralSpeed = 5f;     
    public float moveAwaySpeed = 4f;    
    public float spiralRadius = 2f;  
    public bool shrinkOverTime = true; 

    private Vector3 startPos;
    private Vector3 moveDirection;     
    private float timer = 0f;

    protected override void Start()
    {
        lifetime = 3f;
        base.Start();

        startPos = transform.position;

        if (Camera.main != null)
        {
            moveDirection = (transform.position - Camera.main.transform.position).normalized;
        }
        else
        {
            moveDirection = Vector3.forward; 
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        float currentRadius = spiralRadius;
        if (shrinkOverTime)
        {
            currentRadius = spiralRadius * (1f - (timer / lifetime));
        }

        float xOffset = Mathf.Cos(timer * spiralSpeed) * currentRadius;
        float yOffset = Mathf.Sin(timer * spiralSpeed) * currentRadius;

        Vector3 forwardMovement = moveDirection * (timer * moveAwaySpeed);

        Quaternion rotationToMoveDir = Quaternion.LookRotation(moveDirection);
        Vector3 sideOffset = rotationToMoveDir * new Vector3(xOffset, yOffset, 0);

        transform.position = startPos + forwardMovement + sideOffset;
        transform.Rotate(Vector3.forward * 300f * Time.deltaTime);
    }
}