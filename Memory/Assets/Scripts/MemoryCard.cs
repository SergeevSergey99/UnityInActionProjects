using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCard : MonoBehaviour
{
    [SerializeField]private GameObject cardBack;

    private SceneController controller;

    private int id;

    public int getId() => id;
    public void setId(int i)
    {
        id = i;
        
    }
    public void SetController(SceneController c)
    {
        controller = c;
    }
    private void OnMouseDown()
    {
        if (cardBack.activeSelf && !controller.IsAnim())
        {
            StartCoroutine(Rottating());
        }
    }

    public void UnRevel()
    {
        StartCoroutine(Rottating(false));
    }

    IEnumerator Rottating(bool open = true)
    {
        float t = 0.1f;
        while (transform.localScale.x > 0.001f)
        {
            transform.localScale = new Vector3(Mathf.Lerp(transform.localScale.x, 0, t),1,1);
            yield return new WaitForEndOfFrame();
        }
        
        cardBack.SetActive(!open);
        
        while (transform.localScale.x < 1 - 0.001f)
        {
            transform.localScale = new Vector3(Mathf.Lerp(transform.localScale.x, 1, t),1,1);
            yield return new WaitForEndOfFrame();
        }
        transform.localScale = Vector3.one;
        if(open)
            controller.CardRevel(this);
    }
}
