using UnityEngine;

public class Command_ChangeHealth : Command
{
    #region Fields
    private float _beforeHealth;

    private float _afterHealth;

    private IHealth _health;

    #endregion

    public Command_ChangeHealth(IHealth health, float beforeHealth, float afterHealth)
    {
        _health = health;

        _beforeHealth = beforeHealth;

        _afterHealth = afterHealth;
    }

    public override void Do()
    {
        _health.SetHealth(_afterHealth);
    }

    public override void Undo()
    {
        _health.SetHealth(_beforeHealth);
    }

}
