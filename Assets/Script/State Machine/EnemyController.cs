using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Enemy_StateMachine _stateMachine;

    void Start()
    {
        _stateMachine.InitStateMachine();
    }
    void Update()
    {
        _stateMachine.UpdateStateMachine();
    }
}
