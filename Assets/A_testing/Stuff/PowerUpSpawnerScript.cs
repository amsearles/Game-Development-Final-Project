using UnityEngine;
using System.Collections;

public class PowerUpSpawnerScript : MonoBehaviour
{
	public GameObject enemy;
	public Vector3 spawnCoordinates;
	public int numberOfEnemies;
	public float timeUntilNextWave;
	public float stopDelay;
	public float startDelay;


	void Start ()
	{
		spawnCoordinates.y = 0.0f;

		StartCoroutine (Spawn());
	}

	IEnumerator Spawn()
	{
		//time before enemies start spawning
		yield return new WaitForSeconds (startDelay);
		do {
			//keep spawning until the chosen amount of enemies is reached
			for (int i = 0; i < numberOfEnemies; i++) {

				//I use a random range for x coordinates so that it will spawn enemies at a random variable along the x-axis
				Vector3 spawnPosition = new Vector3 (spawnCoordinates.x, spawnCoordinates.y, spawnCoordinates.z);

				//Quaternion spawnRotation = Quaternion.identity;

				//Instantiate (enemy, spawnPosition, spawnRotation);

				/*
                 * Just spawn the enemy ships exactly where this SpawnController object is placed in the world (outside of screen view).
                 * The ships will fly facing the direction of the SpawnController itself.
                 * //(i.e Instantiate(plane, transform.position, transform.rotation))
				*/
				Instantiate(enemy, spawnPosition, transform.rotation);

				yield return new WaitForSeconds (stopDelay);
			}
			yield return new WaitForSeconds (timeUntilNextWave);
		} while(true);
	}

}