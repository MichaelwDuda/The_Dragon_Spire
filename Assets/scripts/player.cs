using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement Config")]
    [SerializeField] private float _movementSpeed = 5f;
    [SerializeField] private float _sprintSpeed = 10f;

    [Tooltip("Higher = slower acceleration/deceleration.")]
    [SerializeField] private float _accelerationDivisor = 5f;

    private Rigidbody rb;
    private Vector2 _moveVector = Vector2.zero;
    private float _rotateSpeedDegPerSec = 360f;

    private bool _isSprinting = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        UpdateMovement();
        UpdateRotation();
    }

    private void UpdateMovement()
    {
        float moveSpeed = _isSprinting ? _sprintSpeed : _movementSpeed;

        Vector3 targetVelocity = new Vector3(_moveVector.x, 0f, _moveVector.y) * moveSpeed;

        rb.linearVelocity = Vector3.Lerp(
            rb.linearVelocity,
            targetVelocity,
            Time.fixedDeltaTime * (1f / _accelerationDivisor)
        );
    }

    private void UpdateRotation()
    {
        if (_moveVector.sqrMagnitude < 0.0001f) return;

        float targetAngle = Mathf.Atan2(_moveVector.x, _moveVector.y) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);

        rb.MoveRotation(Quaternion.RotateTowards(
            rb.rotation,
            targetRotation,
            _rotateSpeedDegPerSec * Time.fixedDeltaTime
        ));
    }

    public void SetMoveVector(Vector2 moveVector) => _moveVector = moveVector;
    public void ResetVelocity() => rb.linearVelocity = Vector3.zero;
    public void ToggleSprintSpeed(bool toggle) => _isSprinting = toggle;
}
