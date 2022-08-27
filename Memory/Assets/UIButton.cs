using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIButton : MonoBehaviour
{
    public Color highlightColor = Color.cyan;
    [SerializeField]private UnityEvent onClick;

    private SpriteRenderer sr;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnMouseEnter()
    {
        if (sr != null)
            sr.color = highlightColor;
    }
    private void OnMouseExit()
    {
        if (sr != null)
            sr.color = Color.white;
    }

    private void OnMouseDown()
    {
        transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
    }

    private void OnMouseUp()
    {
        transform.localScale = Vector3.one;
        onClick.Invoke();
    }
}
