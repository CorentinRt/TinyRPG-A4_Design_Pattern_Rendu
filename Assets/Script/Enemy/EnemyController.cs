using UnityEngine;

public class EnemyController : MonoBehaviour
{
    #region Fields
    [SerializeField] private Enemy_StateMachine _stateMachine;
    #endregion
    void Start()
    {
        _stateMachine.InitStateMachine();
    }
    void Update()
    {
        _stateMachine.UpdateStateMachine();
    }
}
