using System;
using UnityEngine;

public class CharacterMovementsBehaviour : MonoBehaviour
{
    #region Fields

    [Header("Physics")]
    [SerializeField] private Rigidbody _rb;

    [Header("Anchor Direction")]
    [SerializeField] private Transform _anchorRotation;
    [SerializeField] private float _rotationMatchDirectionSpeed = 10f;

    private Vector2 _currentDir;

    #endregion

    #region Properties
    public Rigidbody RigidBody => _rb;
    public Transform AnchorRotation => _anchorRotation;

    #endregion

    public event Action<Vector3> onVelocityChange;



    public virtual void SetMoveDirection(Vector2 dir)
    {
        _currentDir = dir;
    }

    protected virtual void UpdateMovements(bool movementsEnabled, float acceleration, float maxVelocity, float turningBoost, float deceleration = 0.1f)
    {
        float boost = 1f;

        if (_currentDir != Vector2.zero && _rb.linearVelocity.magnitude > 10f && Vector2.Dot(_currentDir, new Vector2(_rb.linearVelocity.x, _rb.linearVelocity.z)) <= 0f)
        {
            boost = turningBoost;
        }

        Vector2 tempVelocity = Vector2.zero;
        tempVelocity.x = _rb.linearVelocity.x;
        tempVelocity.y = _rb.linearVelocity.z;

        if (_currentDir != Vector2.zero && movementsEnabled) // add velocity
        {
            tempVelocity += _currentDir * acceleration * Time.fixedDeltaTime * boost;
        }
        else   // deceleration
        {
            tempVelocity = Vector2.Lerp(tempVelocity, Vector2.zero, Time.fixedDeltaTime * deceleration);
        }

        tempVelocity = Vector2.ClampMagnitude(tempVelocity, maxVelocity);
        _rb.linearVelocity = new Vector3(tempVelocity.x, 0f, tempVelocity.y);

        onVelocityChange?.Invoke(_rb.linearVelocity);

        UpdateAnchorDirectionRotation();
    }

    public void UpdateAnchorDirectionRotation()
    {
        if (_currentDir == Vector2.zero)
            return;

        Quaternion currentRotation = _anchorRotation.rotation;

        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(_currentDir.x, 0f, _currentDir.y), _anchorRotation.up);

        Quaternion finalRotation = Quaternion.Lerp(currentRotation, targetRotation, _rotationMatchDirectionSpeed * Time.deltaTime);

        _anchorRotation.rotation = finalRotation;
    }

}
