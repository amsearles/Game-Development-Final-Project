/**
 * Jimmy He & Anthony Searles & Elric Dang
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameController : MonoBehaviour
{
    
    [Tooltip("REQUIRED! Reinstantiate this PlayerUnit is destroyed and has lives remaining.")]
    public PlayerUnit playerUnit;

    public GUIText livesText;
	public GameObject scoreText;

    [SerializeField]
    private int _score = 0;
    [SerializeField]
    private int _lives = 0;
    
    private PlayerUnit currentPlayerUnit;
    private bool _isGameOver = false;

    /*** Properties ***/
    public int lives
    {
        get { return _lives; }
        set { _lives = value; }
    }

    public int score
    {
        get { return _score; }
        set { _score = (value < 0) ? 0 : value; }
    }

    public bool isGameOver 
    {
        get { return _isGameOver; }
        // Read only. GameController decides when its game over.
    }


    // *****************
    // 
    //  Unity Methods
    //
    // *****************
    

    private void Start()
    {
		if (_score < 0) _score = 0;
        if (_lives < 0) _lives = 0;

        GameObject gobj = GameObject.FindGameObjectWithTag(Tags.Player);
        
        if (gobj != null)
            currentPlayerUnit = gobj.transform.root.GetComponentInChildren<PlayerUnit>();

        if (currentPlayerUnit == null)
            currentPlayerUnit = Instantiate(playerUnit);
        
        StartCoroutine(ManagePlayerState());

	}

    private IEnumerator ManagePlayerState()
    {
        bool isManaging = true;
        while (isManaging)
        {
            //Player is null and therefore implicitly dead. Not sure if this a good approach.
            if (currentPlayerUnit == null)
            {
                lives--;
                if (lives < 0)
                    _isGameOver = true;
                else
                    yield return StartCoroutine(RespawnPlayer(3.0f));

                if (isGameOver) isManaging = false;
            }
            // Break out of process per loop.
            yield return null;
        }
        
    }

    /// <summary>
    /// Handle respawning the Player in 3 second interval. It also calls
    /// <see cref="UIRespawnController"/> to display text countdown.
    /// </summary>
    /// <param name="delay"></param>
    /// <returns></returns>
    private IEnumerator RespawnPlayer(float delay)
    {
        float waitTime = Time.time + delay;

        // Use this loop to perhaps display a countdown.
        while (Time.time < waitTime)
        {
            UIRespawnController.DISPLAYTIME = Mathf.CeilToInt(waitTime - Time.time);
            yield return new WaitForSeconds(1.0f);
        }

        // Set Respawn text back to empty string.
        // TODO: Find a cleaner alternative later.
        UIRespawnController.DISPLAYTIME = -1;

        // Respawn the Player at the given Transform position, or else, at default (0,0,0).
        currentPlayerUnit = Instantiate(playerUnit);

        // Give Player invincibility for 3 seconds.
        currentPlayerUnit.healthComponent.SetInvincible(true, 3.0f);

        // Find the UIHealthController and re-link the text.
        // TODO: This is a very quick work around by making playerunit static. Find alternative later.
        UIHealthController.playerunit = currentPlayerUnit;
    }


    private void Update()
    {
        if (isGameOver)
        {
            PauseGame();
        }
    }


    // *****************
    // 
    //  Public Methods
    //
    // *****************

    public void LoadScene(int sceneInt)
    {
        SceneManager.LoadScene(sceneInt);
    }
    

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1;
    }

    public void RestartCurrentScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
        #else
		    Application.Quit();
        #endif
    }

    public void AddScore(int value)
    {
        score += value;
    }
    

}






//public void LoadData(string filename)
//{
//    /*
//     * Approach:
//     * 1). Get current Scene name.
//     * 2). Append 1). with necessary data files.
//     * 3). Initialize retrieved data to appropriate objects.
//     */

//    string fullFileName = Application.persistentDataPath + "/" + filename;

//    if (File.Exists(fullFileName))
//    {
//        //BinaryFormatter binaryFormatter = new BinaryFormatter();
//        //FileStream fileStream = File.Open(fullFileName, FileMode.Open);

//        //TODO: 
//    }
//}

//public void SaveData(string outputFile)
//{

//}