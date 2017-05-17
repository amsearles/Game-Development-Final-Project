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

    protected virtual void Start()
    {

        uitext = gameObject.GetComponentInChildren<UnityEngine.UI.Text>();
        anim = gameObject.GetComponent<Animator>();

    }

    void Update()
    {

    }

}
