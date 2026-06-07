using UnityEngine;

public class ZonePiece : MonoBehaviour
{
    #region Fields
    [SerializeField] private int coinCount = 20;
    [SerializeField] private BoxCollider _spawnArea;
    #endregion

    void Start()
    {
        if (_spawnArea != null)
        {
            SpawnCoinsInBox();
        }
        else
        {
            Debug.LogError("Missing BoxCollider");
        }
    }

    public void SpawnCoinsInBox()
    {
        Bounds bounds = _spawnArea.bounds;

        for (int i = 0; i < coinCount; i++)
        {
            float randomX = Random.Range(bounds.min.x, bounds.max.x);
            float randomY = Random.Range(bounds.min.y, bounds.max.y);
            float randomZ = Random.Range(bounds.min.z, bounds.max.z);

            Vector3 spawnPosition = new Vector3(randomX, randomY, randomZ);

            Piece piece = ManagerPieces.Instance.PiecesPool.Get();
            piece.SetPosition(spawnPosition, transform);
        }
    }
}
