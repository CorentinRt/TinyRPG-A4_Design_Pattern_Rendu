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

    public void SetHealth(float health)
    {
        _proxy.SetHealth(health);
    }
}
