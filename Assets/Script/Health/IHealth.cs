using System;

public interface IHealth
{
    public event Action<float, float> onHealthChanged;
    public event Action onDie;

    public abstract void InitHealth(float maxHealth);

    public abstract void Heal(float amount);

    public abstract void Damage(float amount);

    public abstract void Die();

    public abstract bool IsDead();

    public abstract float GetHealth();

    public abstract void SetHealth(float health);
}
