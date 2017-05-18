using UnityEngine;
using System.Collections;


public class BackgroundController : MonoBehaviour {

	//Backgrounds
	[Header("Backgrounds")]
	public GameObject firstBack;
	public GameObject secondBack;
	public GameObject thirdBack;

	//Spawned Backgrounds
	private GameObject newFirstBack;
	private GameObject newSecondBack;
	private GameObject newThirdBack;

	//Movement Effect
	[Header("Movement Effect")]
	public float multiplySpeed = 1f;
	public float firstBackSpeed = 1f;
	public float secondBackSpeed = 0.5f;
	public float thirdBackSpeed = 0.25f;

	void Start () {
		
		//Instantiate three backgrounds
		newFirstBack = Instantiate (firstBack) as GameObject;
		newSecondBack = Instantiate (secondBack) as GameObject;
		newThirdBack = Instantiate (thirdBack) as GameObject;
	}
		
	void Update (){
		BackMovement ();
	}

	void BackMovement (){

		//Move First Back
		MeshRenderer firstRenderer = newFirstBack.GetComponent<MeshRenderer>();
		newFirstBack.transform.position = new Vector3 (0,-10,1);
		Material firstBackMat = firstRenderer.material;
		Vector2 firstBackOffset = firstBackMat.mainTextureOffset;
		firstBackOffset.y += firstBackSpeed * multiplySpeed * Time.deltaTime;
		firstBackMat.mainTextureOffset = firstBackOffset;

		//Move Second Back
		MeshRenderer secondRenderer = newSecondBack.GetComponent<MeshRenderer>();
		newSecondBack.transform.position = new Vector3 (0,-10,2);
		Material secondBackMat = secondRenderer.material;
		Vector2 secondBackOffset = secondBackMat.mainTextureOffset;
		secondBackOffset.y += secondBackSpeed * multiplySpeed * Time.deltaTime;
		secondBackMat.mainTextureOffset = secondBackOffset;

		//Move Third Back
		MeshRenderer thirdRenderer = newThirdBack.GetComponent<MeshRenderer>();
		newThirdBack.transform.position = new Vector3 (0,-10,3);
		Material thirdBackMat = thirdRenderer.material;
		Vector2 thirdBackOffset = thirdBackMat.mainTextureOffset;
		thirdBackOffset.y += thirdBackSpeed * multiplySpeed * Time.deltaTime;
		thirdBackMat.mainTextureOffset = thirdBackOffset;
	}

	public void BoostSpeed(float speed){
		multiplySpeed = multiplySpeed * speed;
	}
}
