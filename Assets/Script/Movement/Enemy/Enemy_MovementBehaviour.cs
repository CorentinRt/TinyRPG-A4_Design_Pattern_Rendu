using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Enemy_MovementBehaviour : MonoBehaviour
{
    #region Fields
    [Header("References")]
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private SO_EnemyData _enemyData;
    #endregion

    public event Action onMove;
    public void Move()
    {

        Vector3 pos = new();
        while (pos.magnitude < _enemyData.radiusMinExploration)
        {

            pos = (Vector3)(Random.insideUnitCircle * _enemyData.radiusMaxExploration);
        }
        _agent.destination = pos + transform.position;
    }

    public void Idle()
    {
        StartCoroutine(WaitBeforeNextMove());
    }

    IEnumerator WaitBeforeNextMove()
    {
        yield return new WaitForSeconds(_enemyData.timeBeforeNextMove);

        onMove?.Invoke();
    }
}
