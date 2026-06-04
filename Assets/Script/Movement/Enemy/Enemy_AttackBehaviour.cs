using System.Collections;
using UnityEngine;

public class Enemy_AttackBehaviour : AttackBehaviourBase
{
    private Coroutine _attackDurationCoroutine;
    private bool _isAttacking;
    private float _lastAttackTime = -1f;

    public bool IsAttacking { get => _isAttacking; }

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

    public override void TriggerNewAttack(AttackParams attackParams, int index)
    {
        base.TriggerNewAttack(attackParams, index);
        StartAttackDurationCoroutine(attackParams);
        _lastAttackTime = Time.time;
    }

    public bool IsInCoolDown()
    {
        if (_lastAttackTime < 0)
            return false;

        if (Time.time - _lastAttackTime <= LastTriggeredAttackParams.Cooldown)
            return true;

        return false;
    }
}
