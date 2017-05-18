using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Jimmy He
 * 4/17/17
 * CSC 631
 * Team12
 */

public class UIManager : MonoBehaviour {

    public Canvas initialMenu;
    public GameController gameController;

    private Canvas currentMenu;

	// Use this for initialization
	void Start () {
        currentMenu = initialMenu;

        if (gameController == null)
            gameController = GameObject.FindObjectOfType<GameController>();
	}
    
    /// <summary>
    /// Opens the given Canvas object by setting its active state to true
    /// and setting the current Canvas state to false.
    /// </summary>
    /// <param name="newMenu">Canvas menu object</param>
    public void OpenMenu(Canvas newMenu)
    {
        if (currentMenu == newMenu)
        {
            currentMenu.gameObject.SetActive(true);
            return;
        }

        //CloseCurrentMenu()
        if (currentMenu != null)
            currentMenu.gameObject.SetActive(false);

        currentMenu = newMenu;
        currentMenu.gameObject.SetActive(true);
        
    }

    /// <summary>
    /// Close the current Canvas menu by setting its active state to false.
    /// The reference to the closed menu will be null.
    /// </summary>
    public void CloseCurrentMenu()
    {
        if (currentMenu != null)
        {
            currentMenu.gameObject.SetActive(false);
            currentMenu = null;
        }
    }

}
