using UnityEngine;

[CreateAssetMenu(fileName = "SO_EnemyData", menuName = "Scriptable Objects/SO_EnemyData")]
public class SO_EnemyData : ScriptableObject
{

    [Header("Movement")]
    [Range(0, 100)] public float radiusExploration = 50;
    [Range(0, 100)] public float timeBeforeNextMove = 5;
    [Range(0, 10)] public float MinDistAttackPlayer = 5;
}
