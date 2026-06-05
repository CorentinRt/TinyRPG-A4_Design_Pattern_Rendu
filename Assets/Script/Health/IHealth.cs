using System;

public interface IHealth
{
    public event Action<float, float> onHealthChanged;
    public event Action onDie;

    public abstract void InitHealth(float maxHealth);

    public abstract void Heal(float amount);

    public abstract void Damage(float amount);

    public abstract void Die();

    public abstract void Revive(float reviveHealth);

    public abstract bool IsDead();

    public abstract float GetHealth();

    public abstract void SetHealth(float health, bool notifyEvent = true);
}
