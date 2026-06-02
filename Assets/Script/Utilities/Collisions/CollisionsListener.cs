using System;
using UnityEngine;

public class CollisionsListener : MonoBehaviour
{


    private event Action<Collision> onCollisionEnter;
    private event Action<Collision> onCollisionExit;


    private void OnCollisionEnter(Collision collision)
    {
        onCollisionEnter?.Invoke(collision);
    }

    private void OnCollisionExit(Collision collision)
    {
        onCollisionExit?.Invoke(collision);
    }

}
