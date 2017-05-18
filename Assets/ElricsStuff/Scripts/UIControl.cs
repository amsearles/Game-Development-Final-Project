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
    
}
