﻿using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	public GameObject[] people;
	public Transform spawnCenter;

	float initSpeed = 1;
	float initSpawnRate = 1;

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(this);
		}
	}

	void Start()
	{
		for (int i = 0; i < people.Length; ++i)
		{
			SpawnPerson();
		}
		// InvokeRepeating("SpawnPerson", 0, initSpawnRate);
	}
	
	void SpawnPerson()
	{
		Vector3 randomSpawnPosition = new Vector3(spawnCenter.position.x + Random.Range(-5f, 5f), 0, spawnCenter.position.z + Random.Range(-5f, 5f));
		Instantiate(people[Random.Range(0, people.Length)], randomSpawnPosition, Quaternion.identity);
	}

	public void GameOver()
	{
		Debug.Log("GAME OVER!!");
	}
}
