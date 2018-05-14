using UnityEngine;

public class Donald : MonoBehaviour
{
	float health = 100;
	float wallHealth = 100;
	float wallTimeout = 10;
	float wallLastDestroyed = -Mathf.Infinity;

	void Start()
	{
		
	}
	
	void Update()
	{
	}

	public void TakeDamage(float damage)
	{
		health -= damage;

		if (health <= 0)
		{
			GameManager.Instance.GameOver();
		}
	}
}
