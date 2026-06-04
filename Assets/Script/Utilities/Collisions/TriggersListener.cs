using System;
using UnityEngine;

public class TriggersListener : MonoBehaviour
{
    public event Action<Collider> onTriggerEnter;
    public event Action<Collider> onTriggerExit;


    private void OnTriggerEnter(Collider other)
    {
        onTriggerEnter?.Invoke(other);
    }

    private void OnTriggerExit(Collider other)
    {
        onTriggerExit?.Invoke(other);
    }
}
