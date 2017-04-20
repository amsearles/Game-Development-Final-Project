using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    
    public void OpenMenu(Canvas newMenu)
    {
        if (currentMenu == newMenu)
        {
            currentMenu.gameObject.SetActive(true);
            return;
        }

        if (currentMenu != null)
        {
            currentMenu.gameObject.SetActive(false);

            currentMenu = newMenu;
            currentMenu.gameObject.SetActive(true);
        }
    }

    public void CloseCurrentMenu()
    {
        if (currentMenu != null)
        {
            currentMenu.gameObject.SetActive(false);
            currentMenu = null;
        }
    }

}
