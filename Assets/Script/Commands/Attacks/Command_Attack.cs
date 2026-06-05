using UnityEngine;

public class Command_Attack : Command
{
    #region Fields
    private AttackBehaviourBase _attack;

    private Rigidbody _rb;

    private AttackParams _attackParam;

    private Vector3 _impulseDir;

    private int _attackIndex;

    #endregion


    public Command_Attack(AttackBehaviourBase attack, Rigidbody rb, AttackParams attackParam, int attackIndex, Vector3 ImpulseDir)
    {
        _attack = attack;
     
        _rb = rb;

        _attackParam = attackParam;

        _attackIndex = attackIndex;
    }

    public override void Do()
    {
        _rb.AddForce(_impulseDir * _attackParam.ImpulseForce, ForceMode.Impulse);

        _attack.NotifyAttackOnly(_attackParam, _attackIndex);   // only notify here to avoid collision to trigger damages again but to keep animation attack
    }

    public override void Undo()
    {
        _rb.AddForce(-_impulseDir * _attackParam.ImpulseForce, ForceMode.Impulse);

        _attack.NotifyAttackOnly(_attackParam, _attackIndex);   // only notify event here to avoid collision to trigger damages again but to keep animation attack
    }
}
