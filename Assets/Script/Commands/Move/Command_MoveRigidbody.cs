using UnityEngine;

public class Command_MoveRigidbody : Command
{
    public struct MoveRigidbody_Params
    {
        #region Fields
        private Vector3 _position;

        private Quaternion _rotation;

        private Vector3 _linearVelocity;

        private Vector3 _angularVelocity;


        #endregion

        #region Properties
        public Vector3 Position { get => _position; set => _position = value; }
        public Quaternion Rotation { get => _rotation; set => _rotation = value; }
        public Vector3 LinearVelocity { get => _linearVelocity; set => _linearVelocity = value; }
        public Vector3 AngularVelocity { get => _angularVelocity; set => _angularVelocity = value; }

        #endregion
    }

    #region Fields

    private Rigidbody _rb;

    private MoveRigidbody_Params _beforeMoveParams;
    private MoveRigidbody_Params _afterMoveParams;

    #endregion

    public Command_MoveRigidbody(Rigidbody rb, MoveRigidbody_Params beforeMoveParams, MoveRigidbody_Params afterMoveParams)
    {
        _rb = rb;
        _beforeMoveParams = beforeMoveParams;
        _afterMoveParams = afterMoveParams;
    }
    
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

        _rb.position = moveParams.Position;
        _rb.rotation = moveParams.Rotation;
        _rb.linearVelocity = moveParams.LinearVelocity;
        _rb.angularVelocity = moveParams.AngularVelocity;
    }
}
