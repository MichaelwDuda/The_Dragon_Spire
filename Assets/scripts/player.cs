using UnityEngine;
using System;

public class player : MonoBehaviour
{
   [Header("Movement Config")] [SerializeField]
    private float _movementSpeed = 5f;

    [SerializeField] private float _sprintSpeed = 10f;

    [Tooltip(
        "The divisor of the interpolation speed between stopping and moving.\nThe higher the value, the slower the player accelerates/deceleration.")]
    [SerializeField]
    private float _accelerationDivisor = 5f;

    private Rigidbody2D rb;
    private Vector2 _moveVector = Vector2.zero;
    private float _rotateSpeedDegPerSec = 360f;

    private bool _isSprinting = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        UpdateMovement();
        UpdateRotation();
    }

    private void UpdateMovement()
    {
        var moveSpeed = _isSprinting ? _sprintSpeed : _movementSpeed;
        rb.linearVelocity =
            Vector2.Lerp(rb.linearVelocity, _moveVector * moveSpeed, Time.timeScale / _accelerationDivisor);
    }

    private void UpdateRotation()
    {
        if (_moveVector.sqrMagnitude < 0.0001f) return;

        var targetAngle = Mathf.Atan2(_moveVector.y, _moveVector.x) * Mathf.Rad2Deg;

        var newAngle = Mathf.MoveTowardsAngle(rb.rotation, targetAngle, _rotateSpeedDegPerSec * Time.fixedDeltaTime);
        rb.MoveRotation(newAngle);
    }

    #region PlayerAction Events

    public void SetMoveVector(Vector2 moveVector)
    {
        _moveVector = moveVector;
    }

    public void ResetVelocity()
    {
        rb.linearVelocity = Vector2.zero;
    }

    public void ToggleSprintSpeed(bool toggle)
    {
        _isSprinting = toggle;
    }

    #endregion
}
