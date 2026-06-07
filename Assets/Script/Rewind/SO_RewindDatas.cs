using UnityEngine;

[CreateAssetMenu(fileName = "SO_RewindDatas", menuName = "Datas/Rewind", order = 1)]
public class SO_RewindDatas : ScriptableObject
{
    #region Fields
    [Header("Rewind")]
    [SerializeField, Range(0.1f, 10f)] private float _rewindSpeed = 2f;
    [SerializeField, Range(1f, 30f)] private float _maxFreeTimeBeforeRewind = 15f;
    [SerializeField, Range(1f, 10f)] private float _minAccumulatedTimeToRewind = 2f;

    #endregion

    #region Properties

    // Rewind
    public float RewindSpeed => _rewindSpeed;
    public float MaxFreeTimeBeforeRewind => _maxFreeTimeBeforeRewind;
    public float MinAccumulatedTimeToRewind => _minAccumulatedTimeToRewind;

    #endregion

}
