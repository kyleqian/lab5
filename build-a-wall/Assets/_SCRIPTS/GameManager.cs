using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	float initSpeed = 1;
	float initSpawnFreq = 1;

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
		
	}
	
	void Update()
	{
		// Instantiate citizens with increasing speed and frequency
	}

	public void GameOver()
	{

	}
}
