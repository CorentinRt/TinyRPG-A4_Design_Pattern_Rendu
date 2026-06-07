using System;
using System.Collections;
using UnityEngine;

public class PlayerAttackBehaviour : AttackBehaviourBase
{
    #region Fields

    [Header("Init")]
    [SerializeField] private bool _awakeEnable = true;

    [Header("Physics")]
    [SerializeField] private Rigidbody _rb;

    [Header("Anchor Direction")]
    [SerializeField] private Transform _anchorDirection;

    private float _lastAttackTime = -1f;

    private bool _isAttacking;

    private bool _attackEnabled;

    private Coroutine _attackDurationCoroutine;

    #endregion

    public event Action<AttackParams, int, Vector3> onPlayerAttack;

    protected override void Awake()
    {
        base.Awake();

        SetAttackEnable(_awakeEnable);

    }

    public void SetAttackEnable(bool enable)
    {
        _attackEnabled = enable;
    }
    public bool IsAttackEnabled()
    {
        return _attackEnabled;
    }

    protected virtual bool IsInCooldown()
    {
        if (_lastAttackTime < 0f)
            return false;

        if (Time.time - _lastAttackTime <= LastTriggeredAttackParams.Cooldown)
            return true;

        return false;
    }

    protected virtual bool IsAttacking()
    {
        return _isAttacking;
    }


    public override void TriggerNewAttack(AttackParams attackParams, int index)
    {
        if (IsInCooldown() || IsAttacking() || !IsAttackEnabled())
            return;

        base.TriggerNewAttack(attackParams, index);

        ApplyImpuleForce(attackParams.ImpulseForce);

        _lastAttackTime = Time.time;

        StartAttackDurationCoroutine(attackParams);

        onPlayerAttack?.Invoke(attackParams, index, _anchorDirection.forward);
    }

    private void ApplyImpuleForce(float impulseForce)
    {
        if (_anchorDirection == null)
        {
            Debug.LogError("ERROR : no _anchorDirection set in playerAttackBehaviour ! Attack impulse won't work !", this);
            return;
        }

        if (_rb == null)
        {
            Debug.LogError("ERROR : no Rigidbody set in playerAttackBehaviour ! Attack impulse won't work !", this);
            return;
        }

        _rb.AddForce(_anchorDirection.forward * impulseForce, ForceMode.Impulse);
    }

    private void StartAttackDurationCoroutine(AttackParams attackParams)
    {
        StopAttackDurationCoroutine();

        _attackDurationCoroutine = StartCoroutine(AttackDurationCoroutine(attackParams));
    }

    private void StopAttackDurationCoroutine()
    {
        if (_attackDurationCoroutine != null)
        {
            StopCoroutine(_attackDurationCoroutine);
            _attackDurationCoroutine = null;
        }
    }

    private IEnumerator AttackDurationCoroutine(AttackParams attackParams)
    {
        _isAttacking = true;

        yield return new WaitForSeconds(attackParams.Duration);

        NotifyEndAttack();

        _isAttacking = false;
    }

}
