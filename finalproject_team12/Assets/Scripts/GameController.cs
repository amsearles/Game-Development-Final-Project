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

	public GameObject scoreText;
	private int score;

    public void LoadScene(int sceneInt)
    {
        SceneManager.LoadScene(sceneInt);
    }

	void Start()
    {
		score = 0;
		UpdateScore ();

	}

    public void LoadData(string filename)
    {
        /*
         * Approach:
         * 1). Get current Scene name.
         * 2). Append 1). with necessary data files.
         * 3). Initialize retrieved data to appropriate objects.
         */

        string fullFileName = Application.persistentDataPath + "/" + filename;

        if (File.Exists(fullFileName))
        {
            //BinaryFormatter binaryFormatter = new BinaryFormatter();
            //FileStream fileStream = File.Open(fullFileName, FileMode.Open);

            //TODO: 
        }
    }

    public void SaveData(string outputFile)
    {
        
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
		UpdateScore ();
	}

	public void UpdateScore()
	{
        if (scoreText != null && scoreText.GetComponent<UnityEngine.UI.Text>() != null )
		    scoreText.GetComponent<UnityEngine.UI.Text> ().text = "Score : " + score;
	}

}
