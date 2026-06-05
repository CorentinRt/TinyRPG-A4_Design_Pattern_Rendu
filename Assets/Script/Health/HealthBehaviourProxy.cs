using System;
using UnityEngine;

public class HealthBehaviourProxy : MonoBehaviour, IHealth
{
    #region Fields
    [Header("Proxy")]
    [SerializeField] private HealthBehaviour _proxy;

    #endregion

    public event Action<float, float> onHealthChanged;
    public event Action onDie;

    public void Damage(float amount)
    {
        _proxy.Damage(amount);
    }

    public void Die()
    {
        _proxy.Die();
    }

    public void Revive(float reviveHealth)
    {
        _proxy.Revive(reviveHealth);
    }

    public float GetHealth()
    {
        return _proxy.GetHealth();
    }

    public void Heal(float amount)
    {
        _proxy.Heal(amount);
    }

    public void InitHealth(float maxHealth)
    {
        _proxy.InitHealth(maxHealth);
    }

    public bool IsDead()
    {
        return _proxy.IsDead();
    }

    public void SetHealth(float health, bool notifyEvent = true)
    {
        _proxy.SetHealth(health);
    }
}
