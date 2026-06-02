using NaughtyAttributes;
using UnityEngine;

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
        if (_datas != null)
        {
            UpdateMovements(_isMovementsEnabled, _datas.Acceleration, _datas.MaxVelocity, _datas.TurningBoost, _datas.Deceleration);
        }

    }

    public void SetMovementsEnable(bool enable)
    {
        _isMovementsEnabled = enable;
    }

}
