using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimationLinker : MonoBehaviour
{
    #region Fields

    [Header("Animator")]
    [SerializeField] private Animator _animator;

    [Header("Movements")]
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private string _walkSpeedKey = "WalkSpeed";

    [Header("Attacks")]
    [SerializeField] private AttackBehaviourBase _attacks;
    [SerializeField] private string _attackKey = "Attack";

    [Header("Health")]
    [SerializeField] private HealthBehaviour _health;
    [SerializeField] private string _hitKey = "Hit";
    [SerializeField] private string _dieKey = "Die";
    [SerializeField] private string _reviveKey = "Revive";

    #endregion

    private void Awake()
    {
        // Attacks
        _attacks.onTriggerNewAttack += OnReceiveTriggerAttack;

        // Health
        _health.onHealthChanged += OnReceiveHealthChanged;
        _health.onDie += OnReceiveDies;
        _health.onRevive += OnReceiveRevive;

    }

    private void OnDestroy()
    {
        // Attacks
        _attacks.onTriggerNewAttack -= OnReceiveTriggerAttack;

        // Health
        _health.onHealthChanged -= OnReceiveHealthChanged;
        _health.onDie -= OnReceiveDies;
        _health.onRevive -= OnReceiveRevive;

    }

    private void Update()
    {
        if (_agent != null)
        {
            OnReceiveVelocityChange(_agent.velocity);
        }
    }

    private void OnReceiveVelocityChange(Vector3 velocity)
    {
        _animator.SetFloat(_walkSpeedKey, velocity.magnitude);
    }

    private void OnReceiveTriggerAttack(AttackParams attackParams, int index)
    {
        _animator.SetTrigger(_attackKey);
    }

    private void OnReceiveHealthChanged(float currentHealth, float amountChanged)
    {
        if (amountChanged >= 0)
            return;

        _animator.SetTrigger(_hitKey);
    }

    private void OnReceiveDies()
    {
        _animator.SetTrigger(_dieKey);
    }

    private void OnReceiveRevive()
    {
        _animator.SetTrigger(_reviveKey);
    }
}
