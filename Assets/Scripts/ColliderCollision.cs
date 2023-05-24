using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderCollision : MonoBehaviour
{
    private Renderer[] _renderers;

    private void Awake()
    {
        _renderers = GetComponentsInChildren<Renderer>();
    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        foreach (var renderer in _renderers)
        {
            renderer.material.color = Color.red;
        }
    }

    private void OnTriggerExit(Collider otherCollider)
    {
        foreach (var renderer in _renderers)
        {
            renderer.material.color = Color.white;
        }
    }
}
