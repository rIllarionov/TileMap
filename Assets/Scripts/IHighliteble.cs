using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IHighliteble : MonoBehaviour
{
    private Renderer[] _renderers;
    private void Awake()
    {
        _renderers = GetComponentsInChildren<Renderer>();
    }

    public void SetColor()
    {
        foreach (var obj in _renderers)
        {
            obj.material.color = Color.green;
        }
    }

    public void ResetColor()
    {
        foreach (var obj in _renderers)
        {
            obj.material.color = Color.white;
        }
    }
}
