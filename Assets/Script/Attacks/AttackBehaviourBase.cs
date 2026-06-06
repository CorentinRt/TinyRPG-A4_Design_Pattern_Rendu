using System;
using System.Collections.Generic;
using UnityEngine;



[Serializable]
public struct AttackParams
{
    #region Fields
    [SerializeField] private float _duration;
    [SerializeField] private float _cooldown;
    [SerializeField] private float _comboBuffer;
    [SerializeField] private float _damages;
    [SerializeField] private bool _canBeBlocked;
    [SerializeField] private float _impulseForce;

    #endregion

    #region Properties
    public float Duration => _duration;
    public float Cooldown => _cooldown;
    public float ComboBuffer => _comboBuffer;
    public float Damage => _damages;
    public bool CanBeBlocked => _canBeBlocked;
    public float ImpulseForce => _impulseForce;

    #endregion

}

public class AttackBehaviourBase : MonoBehaviour
{

    #region Fields
    [Header("Hit Boxes")]
    [SerializeField] private List<TriggersListener> _hitBoxesTriggersListeners;

    private AttackParams _lastTriggeredAttackParams;

    private int _lastTriggeredAttackIndex;

    #endregion

    #region Properties
    public AttackParams LastTriggeredAttackParams => _lastTriggeredAttackParams;
    public int LastTriggeredAttackIndex => _lastTriggeredAttackIndex;

    #endregion


    public event Action<AttackParams, int> onTriggerNewAttack;

    protected virtual void Awake()
    {
        for (int i = 0; i < _hitBoxesTriggersListeners.Count; ++i)
        {
            _hitBoxesTriggersListeners[i].onTriggerEnter += ReceiveOnTriggerEnterHitBox;
            _hitBoxesTriggersListeners[i].gameObject.SetActive(false);
        }
    }

    protected virtual void OnDestroy()
    {
        for (int i = 0; i < _hitBoxesTriggersListeners.Count; ++i)
        {
            _hitBoxesTriggersListeners[i].onTriggerEnter -= ReceiveOnTriggerEnterHitBox;
        }
    }


    private void ReceiveOnTriggerEnterHitBox(Collider collider)
    {
        if (!collider.gameObject.TryGetComponent<IHealth>(out IHealth health))
            return;

        health.Damage(_lastTriggeredAttackParams.Damage);
    }

    public virtual void TriggerNewAttack(AttackParams attackParams, int index)
    {
        _lastTriggeredAttackParams = attackParams;

        _lastTriggeredAttackIndex = index;

        EnableAttackIndexCollider(index);

        onTriggerNewAttack?.Invoke(attackParams, index);
    }

    public void NotifyAttackOnly(AttackParams attackParams, int index)
    {
        onTriggerNewAttack?.Invoke(attackParams, index);
    }

    public virtual void NotifyEndAttack()
    {
        DisableAllAttackColliders();
    }


    private void EnableAttackIndexCollider(int index)
    {
        if (index < 0 || index >= _hitBoxesTriggersListeners.Count)
        {
            Debug.LogError("Error : index of attack higher than hit boxes collider max index -> No Attack colliders will be triggered !", this);
            return;
        }

        _hitBoxesTriggersListeners[index].gameObject.SetActive(true);
    }

    private void DisableAllAttackColliders()
    {
        for (int i = 0; i < _hitBoxesTriggersListeners.Count; ++i)
        {
            _hitBoxesTriggersListeners[i].gameObject.SetActive(false);
        }
    }
}
