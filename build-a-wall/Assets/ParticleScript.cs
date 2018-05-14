using UnityEngine;

public class ParticleScript : MonoBehaviour
{
	void Start()
	{
		
	}
	
	void Update()
	{
		
	}

	void OnParticleCollision(GameObject other)
	{
    	Debug.Log("PS COLLISION: " + other.name);
	}
}
