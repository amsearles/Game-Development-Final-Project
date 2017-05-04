using UnityEngine;
using System.Collections;

public class SpawnController : MonoBehaviour
{
	public GameObject enemy;
	public Vector3 spawnCoordinates;
	public int numberOfEnemies;
	public float timeUntilNextWave;


	void Start ()
	{
		StartCoroutine (Spawn());
	}

	IEnumerator Spawn()
	{
		//time before enemies start spawning
		yield return new WaitForSeconds (1);
		do {
			//keep spawning until the chosen amount of enemies is reached
			for (int i = 0; i < numberOfEnemies; i++) {

				//I use a random range for x coordinates so that it will spawn enemies at a random variable along the x-axis
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnCoordinates.x, spawnCoordinates.x), spawnCoordinates.y, spawnCoordinates.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (enemy, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (1);
			}
			yield return new WaitForSeconds (timeUntilNextWave);
		} while(true);
	}
}