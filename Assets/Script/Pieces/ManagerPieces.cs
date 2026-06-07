using UnityEngine;

public class ManagerPieces : GenericSingleton<ManagerPieces>
{
    #region Fields
    [SerializeField] private GameObject _prefabPiece;
    private ObjectPool<Piece> _piecesPool;

    private int _piecesCollected = 0;
    #endregion

    #region Properties
    public ObjectPool<Piece> PiecesPool { get => _piecesPool; }
    #endregion

    private void Awake()
    {
        InitPoolPiece();
    }


    public void InitPoolPiece()
    {
        _piecesPool = new ObjectPool<Piece>
        (
            createFunc: CreatePiece,
            actionOnGet: GetPiece,
            actionOnRelease: DisablePiece
        );
    }





    private void OnCollectPiece(Piece piece)
    {
        _piecesPool.Release(piece);
        _piecesCollected += 1;

    }

    private Piece CreatePiece()
    {
        GameObject go = Instantiate(_prefabPiece);
        Piece piece = go.GetComponent<Piece>();
        return piece;
    }

    private void GetPiece(Piece piece)
    {
        piece.onCollect += OnCollectPiece;
        piece.ActivePiece();
    }

    private void DisablePiece(Piece piece)
    {
        piece.onCollect -= OnCollectPiece;
        piece.DeactivePiece();
    }
}
