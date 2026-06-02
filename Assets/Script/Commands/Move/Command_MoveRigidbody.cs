using UnityEngine;

public class Command_MoveRigidbody : Command
{
    public struct MoveRigidbody_Params
    {
        public Vector3 position;

        public Quaternion rotation;

        public Vector3 linearVelocity;

        public Vector3 angularVelocity;
    }

    #region Fields

    private Rigidbody _rb;

    private MoveRigidbody_Params _beforeMoveParams;
    private MoveRigidbody_Params _afterMoveParams;

    #endregion

    public override void Do()
    {
        Move(_afterMoveParams);
    }

    public override void Undo()
    {
        Move(_beforeMoveParams);
    }

    private void Move(MoveRigidbody_Params moveParams)
    {
        if (_rb == null)
        {
            Debug.LogError("Error : No rigidBody linked to Command Move Rigidbody ! Command_MoveRigidbody won't work !");
            return;
        }

        _rb.position = moveParams.position;
        _rb.rotation = moveParams.rotation;
        _rb.linearVelocity = moveParams.linearVelocity;
        _rb.angularVelocity = moveParams.angularVelocity;
    }
}
