using NaughtyAttributes;
using System;
using UnityEngine;

public class HealthBehaviour : MonoBehaviour, IHealth
{
    #region Fields

    [Header("Init")]
    [SerializeField] private bool _autoInitOnStart;

    [ShowIf("_autoInitOnStart")]
    [SerializeField] private int _startHealth = 100;

    protected float _currentHealth;
    protected float _maxHealth;
    private bool _isDead;

    #endregion

    #region Properties


    #endregion

    public event Action<float, float> onHealthChanged;
    public event Action onDie;
    public event Action onRevive;
    public event Action onTakeDamage;

    public event Action<float> onMaxHealthChanged;

    [Button]
    private void DebugDamage10() => Damage(10);
    [Button]
    private void DebugHeal10() => Heal(10);


    private void Start()
    {
        if (_autoInitOnStart)
        {
            InitHealth(_startHealth);
        }
    }

    public void InitHealth(float maxHealth)
    {
        SetMaxHealth(maxHealth);

        SetHealth(_maxHealth);
    }

    public void SetMaxHealth(float maxHealth)
    {
        _maxHealth = maxHealth;

        onMaxHealthChanged?.Invoke(_maxHealth);
    }

    public void Damage(float amount)
    {
        SetHealth(_currentHealth - amount);

        onTakeDamage?.Invoke();
    }

    public void Die()
    {
        if (IsDead())
            return;

        _isDead = true;

        onDie?.Invoke();
    }

    public void Revive(float reviveHealth)
    {
        if (!IsDead())
            return;

        _isDead = false;

        SetHealth(reviveHealth);

        onRevive?.Invoke();
    }

    public float GetHealth()
    {
        return _currentHealth;
    }

    public void Heal(float amount)
    {
        SetHealth(_currentHealth + amount);
    }
    public bool IsDead()
    {
        return _isDead;
    }

    public void SetHealth(float health)
    {
        if (IsDead())
            return;

        float previousCurrentHealth = _currentHealth;

        _currentHealth = Mathf.Clamp(health, 0f, _maxHealth);

        float amountChanged = _currentHealth - previousCurrentHealth;

        onHealthChanged?.Invoke(_currentHealth, amountChanged);

        if (_currentHealth == 0f)
        {
            Die();
        }
    }
}
