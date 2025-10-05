using UnityEngine;
public class CameraMove : MonoBehaviour
{
    [SerializeField] private Vector2 _limitX;
    [SerializeField] private Vector2 _limitY;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] float smoothTime = 0.2f;
    [SerializeField] float maxVelocity = 20f;

    private Vector3 targetPosition;
    private Vector3 currentVelocity;
    private Vector3 velocity;

    private bool _isDrag = false;
    void Start()
    {
        targetPosition = transform.position;
    }

    void Update()
    {
        if (!G.IsCanInteract) return;
        HandleKeyboardInput();

        MoveXY();
    }

    private void HandleKeyboardInput()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 move = new Vector3(h, v, 0);

        if (move.sqrMagnitude > 1f) // если есть диагональ
            move.Normalize();

        targetPosition += move * moveSpeed * Time.deltaTime;

        targetPosition = new Vector3(
            Mathf.Clamp(targetPosition.x, _limitX.x, _limitX.y),
            Mathf.Clamp(targetPosition.y, _limitY.x, _limitY.y),
            transform.position.z
        );
    }

    private void MoveXY()
    {
        if (Vector3.Distance(transform.position, targetPosition) > 0.01f || velocity.magnitude > 0.01f)
        {
            Vector3 newPosition = Vector3.SmoothDamp(
                transform.position,
                targetPosition,
                ref currentVelocity,
                smoothTime,
                maxVelocity,
                Time.deltaTime
            );

            velocity = (newPosition - transform.position) / Time.deltaTime;

            if (velocity.sqrMagnitude > maxVelocity * maxVelocity)
                velocity = velocity.normalized * maxVelocity;

            transform.position = newPosition + velocity * Time.deltaTime;

            if (Vector3.Distance(transform.position, targetPosition) < 0.01f && velocity.magnitude < 0.01f)
            {
                transform.position = new Vector3(targetPosition.x, targetPosition.y, transform.position.z);
                velocity = Vector3.zero;
            }
        }
    }
}
