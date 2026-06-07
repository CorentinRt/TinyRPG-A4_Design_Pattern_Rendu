using System;
using UnityEngine;

public class HealthRewindableRegistering : MonoBehaviour
{
    #region Fields

    [Header("Health")]
    [SerializeField] private HealthBehaviour _health;
    [SerializeField] private RewindCommandEntity _rewind;

    #endregion

    private void Awake()
    {
        _health.onDie += ReceiveOnDie;

        _health.onHealthChanged += ReceiveOnHealthChanged;
    }

    private void OnDestroy()
    {
        _health.onDie -= ReceiveOnDie;

        _health.onHealthChanged -= ReceiveOnHealthChanged;
    }

    private void ReceiveOnDie()
    {
        Command_Die commandDie = new Command_Die(_health);

        _rewind.RegisterCommand(commandDie);
    }

    private void ReceiveOnHealthChanged(float currentHealth, float amountChanged)
    {
        Command_ChangeHealth commandChangeHealth = 
            new Command_ChangeHealth(_health, currentHealth - amountChanged, currentHealth);

        _rewind.RegisterCommand(commandChangeHealth);
    }



}
