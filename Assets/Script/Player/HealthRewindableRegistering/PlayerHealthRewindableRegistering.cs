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
        if (PlayerCommandRewind.Exist)
            return;


    }

    private void ReceiveOnHealthChanged(float currentHealth, float amountChanged)
    {
        if (PlayerCommandRewind.Exist)
            return;

        Command_ChangeHealth commandChangeHealth = 
            new Command_ChangeHealth(_health, currentHealth - amountChanged, currentHealth);
        
        PlayerCommandRewind.Instance.RegisterCommand(commandChangeHealth);
    }



}
