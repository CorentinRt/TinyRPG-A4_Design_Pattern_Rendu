using System;
using UnityEngine;

public class TriggersListener : MonoBehaviour
{
    private event Action<Collider> onTriggerEnter;
    private event Action<Collider> onTriggerExit;


    private void OnTriggerEnter(Collider other)
    {
        onTriggerEnter?.Invoke(other);
    }

    private void OnTriggerExit(Collider other)
    {
        onTriggerExit?.Invoke(other);
    }
}
