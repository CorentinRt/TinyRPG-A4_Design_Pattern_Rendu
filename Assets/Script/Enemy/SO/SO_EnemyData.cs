using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_EnemyData", menuName = "Scriptable Objects/SO_EnemyData")]
public class SO_EnemyData : ScriptableObject
{
    #region Fields
    [Header("Movement")]
    [Range(0, 20), SerializeField] private float _maxRadiusExploration = 15;
    [Range(0, 20), SerializeField] private float _minRadiusExploration = 5;
    [Range(0, 30), SerializeField] private float _timeBeforeNextMove = 5;

    [Header("Attacks")]
    [Range(0, 3), SerializeField] private float _distAttackPlayer = 3;
    [Range(0, 10), SerializeField] private float _distanceSight = 5;
    [Range(0, 20), SerializeField] private float _distanceLooseSight = 7;
    [SerializeField] private List<AttackParams> _attackParams;
    #endregion

    #region Properties
    public List<AttackParams> AttackParams { get => _attackParams; }
    public float MaxRadiusExploration { get => _maxRadiusExploration; }
    public float MinRadiusExploration { get => _minRadiusExploration; }
    public float TimeBeforeNextMove { get => _timeBeforeNextMove; }
    public float DistAttackPlayer { get => _distAttackPlayer; }
    public float DistanceSight { get => _distanceSight; }
    public float DistanceLooseSight { get => _distanceLooseSight; }
    #endregion
}
