using UnityEngine;

public class Command_Die : Command
{
    #region Fields
    private IHealth _health;

    #endregion

    public Command_Die(IHealth health)
    {
        _health = health;
    }

    public override void Do()
    {
        _health.Die();
    }

    public override void Undo()
    {
        _health.Revive(1f);
    }
}
