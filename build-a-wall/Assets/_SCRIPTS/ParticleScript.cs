using UnityEngine;

public class ParticleScript : MonoBehaviour
{
	void OnParticleCollision(GameObject other)
	{
		
		if (other.tag == "Person")
		{
			Debug.Log("FIRE HIT: " + other.name);
			other.GetComponent<Person>().TakeDamage(10);
		}
	}
}
