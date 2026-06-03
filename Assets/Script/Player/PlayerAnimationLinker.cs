using System;
using UnityEngine;

public class PlayerAnimationLinker : MonoBehaviour
{
    #region Fields
    [Header("Animator")]
    [SerializeField] private Animator _animator;

    [Header("Movements")]
    [SerializeField] private PlayerMovementsBehaviour _movements;
    [SerializeField] private string _walkSpeedKey = "WalkSpeed";

    [Header("Attacks")]
    [SerializeField] private PlayerAttackBehaviour _attacks;
    [SerializeField] private string _attackKey = "Attack";

    [Header("Health")]
    [SerializeField] private HealthBehaviour _health;
    [SerializeField] private string _hitKey = "Hit";
    [SerializeField] private string _dieKey = "Die";

    #endregion


    private void Awake()
    {
        // Movements
        _movements.onVelocityChange += OnReceivePlayerVelocityChange;

        // Attacks
        _attacks.onTriggerNewAttack += OnReceivePlayerTriggerAttack;

        // Health
        _health.onHealthChanged += OnReceivePlayerHealthChanged;

    }

    private void OnDestroy()
    {
        // Movements
        _movements.onVelocityChange -= OnReceivePlayerVelocityChange;

        // Attacks
        _attacks.onTriggerNewAttack -= OnReceivePlayerTriggerAttack;

        // Health
        _health.onHealthChanged -= OnReceivePlayerHealthChanged;

    }

    private void OnReceivePlayerVelocityChange(Vector3 velocity)
    {
        _animator.SetFloat(_walkSpeedKey, velocity.magnitude);
    }

    private void OnReceivePlayerTriggerAttack(AttackParams attackParams, int index)
    {
        _animator.SetTrigger(_attackKey);
    }

    private void OnReceivePlayerHealthChanged(float currentHealth, float amountChanged)
    {
        if (amountChanged >= 0)
            return;

        if (currentHealth < 0 && amountChanged != 0)
        {
            _animator.SetTrigger(_dieKey);
            return;
        }

        _animator.SetTrigger(_hitKey);
    }
}
