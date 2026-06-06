using System;
using UnityEngine;

public class PlayerHealthRewindableRegistering : MonoBehaviour
{
    #region Fields

    [Header("Health")]
    [SerializeField] private HealthBehaviour _health;

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
        if (!PlayerController.Exist)
            return;

        Command_Die commandDie = new Command_Die(_health);

        PlayerController.Instance.Rewind.RegisterCommand(commandDie);
    }

    private void ReceiveOnHealthChanged(float currentHealth, float amountChanged)
    {
        if (!PlayerController.Exist)
            return;

        Command_ChangeHealth commandChangeHealth = 
            new Command_ChangeHealth(_health, currentHealth - amountChanged, currentHealth);

        PlayerController.Instance.Rewind.RegisterCommand(commandChangeHealth);
    }



}
