using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerHealth : MonoBehaviour
{
    #region Fields
    [Header("Slider")]
    [SerializeField] private Slider _healthSlider;

    #endregion


    private void Awake()
    {
        PlayerController.Instance.Health.onMaxHealthChanged += ReceiveOnSetMaxHealth;

        PlayerController.Instance.Health.onHealthChanged += ReceiveOnHealthChanged;
    }

    private void OnDestroy()
    {
        PlayerController.Instance.Health.onMaxHealthChanged -= ReceiveOnSetMaxHealth;

        PlayerController.Instance.Health.onHealthChanged -= ReceiveOnHealthChanged;
    }


    private void ReceiveOnHealthChanged(float currentHealth, float amountChanged)
    {
        _healthSlider.value = currentHealth;
    }

    private void ReceiveOnSetMaxHealth(float maxHealth)
    {
        _healthSlider.maxValue = maxHealth;
    }


}
