using System;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public Action<Piece> onCollect;


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            onCollect?.Invoke(this);
    }

    public void SetPosition(Vector3 newpos, Transform parent)
    {
        transform.position = newpos;
        transform.parent = parent;
    }


    public void DeactivePiece()
    {
        gameObject.SetActive(false);
        print("deactive");
    }

    public void ActivePiece()
    {
        gameObject.SetActive(true);
    }
}
