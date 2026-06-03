using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_PlayerDatas", menuName = "Datas/Player", order = 1)]
public class SO_PlayerDatas : ScriptableObject
{
    #region Fields

    [Header("Movements")]
    [SerializeField] private float _maxVelocity = 20f;
    [SerializeField] private float _acceleration = 5f;
    [SerializeField] private float _deceleration = 5f;
    [SerializeField] private float _turningBoost = 5f;

    [Header("Attacks")]
    [SerializeField] private List<AttackParams> _attacks;

    #endregion

    #region Properties

    // Movements
    public float MaxVelocity => _maxVelocity;
    public float Acceleration => _acceleration;
    public float Deceleration => _deceleration;
    public float TurningBoost => _turningBoost;


    // Attacks
    public List<AttackParams> Attacks => _attacks;


    #endregion
}
