using System;
using UnityEngine;

public class CollisionsListener : MonoBehaviour
{
    public event Action<Collision> onCollisionEnter;
    public event Action<Collision> onCollisionExit;


    private void OnCollisionEnter(Collision collision)
    {
        onCollisionEnter?.Invoke(collision);
    }

    private void OnCollisionExit(Collision collision)
    {
        onCollisionExit?.Invoke(collision);
    }

}
