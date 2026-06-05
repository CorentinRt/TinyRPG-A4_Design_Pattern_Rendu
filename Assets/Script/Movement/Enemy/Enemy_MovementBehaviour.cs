using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

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
        //_agent.destination = (Random.insideUnitCircle * _enemyData.radiusExploration);
    }

    public void Idle()
    {
        //StartCoroutine(WaitBeforeNextMove());
    }

    IEnumerator WaitBeforeNextMove()
    {
        yield return new WaitForSeconds(_enemyData.timeBeforeNextMove);

        onMove?.Invoke();
    }
}
