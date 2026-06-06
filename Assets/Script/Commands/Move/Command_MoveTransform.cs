using UnityEngine;

public class Command_MoveTransform : Command
{
    public struct MoveTransform_Params
    {
        #region Fields
        private Vector3 _position;

        private Quaternion _rotation;

        #endregion

        #region Properties
        public Vector3 Position { get => _position; set => _position = value; }
        public Quaternion Rotation { get => _rotation; set => _rotation = value; }

        #endregion
    }

    #region Fields
    private Transform _transform;

    private MoveTransform_Params _beforeMoveParam;
    private MoveTransform_Params _afterMoveParam;

    #endregion

    public Command_MoveTransform(Transform transform, MoveTransform_Params beforeMoveParam, MoveTransform_Params afterMoveParam)
    {
        _transform = transform;
        _beforeMoveParam = beforeMoveParam;
        _afterMoveParam = afterMoveParam;
    }

    public override void Do()
    {
        _transform.position = _afterMoveParam.Position;

       _transform.rotation = _afterMoveParam.Rotation;
    }

    public override void Undo()
    {
        _transform.position = _beforeMoveParam.Position;

        _transform.rotation = _beforeMoveParam.Rotation;
    }


}
