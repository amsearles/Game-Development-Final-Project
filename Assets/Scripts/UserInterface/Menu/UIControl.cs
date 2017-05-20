/** 
 * Elric Dang
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIControl : MonoBehaviour
{

    protected UnityEngine.UI.Text uitext;
    protected Animator anim;
    protected GameController gc;
    protected GameObject go;

    protected virtual void Start()
    {
        gc = GameObject.FindObjectOfType<GameController>();
        uitext = gameObject.GetComponentInChildren<UnityEngine.UI.Text>();
        anim = gameObject.GetComponent<Animator>();

    }

    /// <summary>Retrieves GameController.</summary>
    /// <returns>GameController</returns>
    protected virtual GameController Get()
    {
        return gc;
    }

    protected void Play(string str, int layer, float time)
    {
        if (anim != null)
            anim.Play(str, layer, time);
    }

    public virtual GameObject GetGameObject()
    {
        return go;
    }

    public void SetGameObject(GameObject newGo)
    {
        go = newGo;
    }

}
