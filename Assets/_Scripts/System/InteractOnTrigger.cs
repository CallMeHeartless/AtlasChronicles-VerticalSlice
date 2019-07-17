using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractOnTrigger : MonoBehaviour
{
    public LayerMask layers;
    public UnityEvent OnEnter, OnStay, OnExit;

    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }

    void Reset()
    {
        layers = LayerMask.NameToLayer("Player");
    }

    void OnTriggerEnter(Collider other)
    {
        if (0 != (layers.value & 1 << other.gameObject.layer))
        {
            ExecuteOnEnter(other);
        }
    }

    protected virtual void ExecuteOnEnter(Collider other)
    {
        OnEnter.Invoke();
    }

    void OnTriggerStay(Collider other)
    {
        if (0 != (layers.value & 1 << other.gameObject.layer))
        {
            ExecuteOnStay(other);
        }
    }

    protected virtual void ExecuteOnStay(Collider other)
    {
        OnStay.Invoke();
    }

    void OnTriggerExit(Collider other)
    {
        if (0 != (layers.value & 1 << other.gameObject.layer))
        {
            ExecuteOnExit(other);
        }
    }

    protected virtual void ExecuteOnExit(Collider other)
    {
        OnExit.Invoke();
    }
}
