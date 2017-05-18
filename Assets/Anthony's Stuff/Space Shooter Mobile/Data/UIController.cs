using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {

	public GameObject inGameCanvas;
	public GameObject gameOverCanvas;

	public Texture2D faderTexture;
	public float faderSpeed = 1f;

	private int faderDepth = -1000;
	private float faderAlpha = 2f;
	private int faderDirection = -1;

	void OnGUI(){
		faderAlpha += faderDirection * faderSpeed * Time.deltaTime;
		faderAlpha = Mathf.Clamp01(faderAlpha);
		GUI.color = new Color (0, 0, 0, faderAlpha);
		GUI.depth = faderDepth;
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), faderTexture);
	}

	public float Fade (int Direction){
		faderDirection = Direction;
		return (faderSpeed);
	}

	void OnLevelWasLoaded(){
		Fade (-1);
	}

	//Open Level
	IEnumerator StartGame(){
		Fade (1);
		yield return new WaitForSeconds (1);
		SceneManager.LoadScene ("Game");
	}

	public void StartGameButton (){
		StartCoroutine ("StartGame");
	}

	public void SwitchToGameOverPanel(){
		inGameCanvas.SetActive (false);
		gameOverCanvas.SetActive (true);
	}
}