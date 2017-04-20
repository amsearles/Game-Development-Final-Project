using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
	public GameObject enemy;
	public Vector2 spawnCoordinates;
	public int numberOfEnemies;
	public float timeUntilNextWave;


	void Start ()
	{
		StartCoroutine (Spawn());
	}

	IEnumerator Spawn()
	{
		yield return new WaitForSeconds (1);
		do {
			for (int i = 0; i < numberOfEnemies; i++) {

				//I use a range for x coordinates so that it will spawn enemies at a random variable.
				Vector2 spawnPosition = new Vector3 (Random.Range (-spawnCoordinates.x, spawnCoordinates.x), spawnCoordinates.y);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (enemy, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (1);
			}
			yield return new WaitForSeconds (timeUntilNextWave);
		} while(true);
	}
}