using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractMenu : MonoBehaviour {

    public UIManager menuManager;


    private void Start()
    {
        menuManager = (UIManager)GameObject.FindObjectOfType(typeof(UIManager));
    }

    /// <summary>
    /// This is a substitute for this menu's Update() as MenuManager's Update()
    /// will call this.ProcessInput() anyway, essentially the same thing.
    /// </summary>
    public abstract void ProcessInput();

    /// <summary>Turn this panel on/off.</summary>
    public void ToggleActivate()
    {
        bool isActive = gameObject.activeSelf;
        gameObject.SetActive(!isActive);
    }

    /// <summary>Check if the GameObject is active.</summary>
    /// <returns>boolean</returns>
    public bool IsActive()
    {
        return gameObject.activeSelf;
    }

}
