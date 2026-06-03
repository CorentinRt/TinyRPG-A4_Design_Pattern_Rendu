using NaughtyAttributes;
using UnityEngine;
using static Command_MoveRigidbody;

public class PlayerMovementsBehaviour : CharacterMovementsBehaviour
{
    #region Fields

    [Header("Init")]
    [SerializeField] private bool _autoInitOnStart;
    [SerializeField] private bool _movementsDisabledByDefault;

    [ShowIf("_autoInitOnStart")]
    [SerializeField] private SO_PlayerDatas _autoInitPlayerDatas;

    private SO_PlayerDatas _datas;

    private bool _isMovementsEnabled;

    #endregion


    private void Start()
    {
        if (_autoInitOnStart)
        {
            InitPlayerMovements(_autoInitPlayerDatas);
        }

    }

    public void InitPlayerMovements(SO_PlayerDatas datas)
    {
        _datas = datas;

        SetMovementsEnable(!_movementsDisabledByDefault);

    }

    public override void SetMoveDirection(Vector2 dir)
    {
        Camera mainCam = Camera.main;

        if (mainCam != null)
        {
            Vector3 cameraForward = Vector3.zero;
            cameraForward += mainCam.transform.forward;
            cameraForward.y = 0f;
            cameraForward.Normalize();

            Vector3 cameraRight = Vector3.zero;
            cameraRight += mainCam.transform.right;
            cameraRight.y = 0f;
            cameraRight.Normalize();

            cameraRight *= dir.x;
            cameraForward *= dir.y;

            Vector3 projectedMoveDir = cameraRight + cameraForward;

            dir = new Vector2(projectedMoveDir.x, projectedMoveDir.z);
        }

        base.SetMoveDirection(dir);
    }

    private void FixedUpdate()
    {
        if (_datas != null && _isMovementsEnabled)
        {
            MoveRigidbody_Params beforeMoveParams = new MoveRigidbody_Params();
            beforeMoveParams.Position = RigidBody.position;
            beforeMoveParams.Rotation = RigidBody.rotation;
            beforeMoveParams.LinearVelocity = RigidBody.linearVelocity;
            beforeMoveParams.AngularVelocity = RigidBody.angularVelocity;

            UpdateMovements(_isMovementsEnabled, _datas.Acceleration, _datas.MaxVelocity, _datas.TurningBoost, _datas.Deceleration);

            MoveRigidbody_Params afterMoveParams = new MoveRigidbody_Params();
            afterMoveParams.Position = RigidBody.position;
            afterMoveParams.Rotation = RigidBody.rotation;
            afterMoveParams.LinearVelocity = RigidBody.linearVelocity;
            afterMoveParams.AngularVelocity = RigidBody.angularVelocity;

            Command_MoveRigidbody commandMove = new Command_MoveRigidbody(RigidBody, beforeMoveParams, afterMoveParams);

            if (PlayerCommandRewind.Exist)
            {
                PlayerCommandRewind.Instance.RegisterCommand(commandMove);
            }
        }

    }

    public void SetMovementsEnable(bool enable)
    {
        _isMovementsEnabled = enable;
    }

}
