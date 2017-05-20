using UnityEngine;
using System.Collections;

public class HeroProjectile : MonoBehaviour {
	public float speed;
	private float deviation;
	public float deviationMovement = 0.3f;
	public float power;

	void Start(){
		deviation = Random.Range (-deviationMovement, deviationMovement);
	}

	void Update () {
		gameObject.transform.Translate (Vector2.up * speed * Time.deltaTime);
		gameObject.transform.Translate (-Vector2.right * deviation * speed * Time.deltaTime);
		Destroy (gameObject, 10);
	}
	void OnTriggerEnter2D (Collider2D other){
		if (other.gameObject.tag == ("Enemy")) {
				Destroy (gameObject, 0.1f);
		}
	}
}
