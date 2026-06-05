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
    [SerializeField] private string _reviveKey = "Revive";

    #endregion


    private void Awake()
    {
        // Movements
        _movements.onVelocityChange += OnReceivePlayerVelocityChange;

        // Attacks
        _attacks.onTriggerNewAttack += OnReceivePlayerTriggerAttack;

        // Health
        _health.onHealthChanged += OnReceivePlayerHealthChanged;
        _health.onDie += OnReceivePlayerDies;
        _health.onRevive += OnReceivePlayerRevive;

    }

    private void OnDestroy()
    {
        // Movements
        _movements.onVelocityChange -= OnReceivePlayerVelocityChange;

        // Attacks
        _attacks.onTriggerNewAttack -= OnReceivePlayerTriggerAttack;

        // Health
        _health.onHealthChanged -= OnReceivePlayerHealthChanged;
        _health.onDie -= OnReceivePlayerDies;
        _health.onRevive -= OnReceivePlayerRevive;

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

        _animator.SetTrigger(_hitKey);
    }

    private void OnReceivePlayerDies()
    {
        _animator.SetTrigger(_dieKey);
    }

    private void OnReceivePlayerRevive()
    {
        _animator.SetTrigger(_reviveKey);
    }
}
