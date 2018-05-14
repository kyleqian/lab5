using UnityEngine;

public class Person : MonoBehaviour
{
	public enum PersonState { Walking, AttackingWall, AttackingDonald }
	public PersonState personState = PersonState.Walking;
	public GameObject donald;
	public GameObject wall;

	float health = 100;

	void Start()
	{
		transform.LookAt(donald.transform);
	}
	
	void Update()
	{
		switch (personState)
		{
			case PersonState.Walking:
				transform.Translate(Vector3.forward * Time.deltaTime);
				break;
			case PersonState.AttackingWall:
				// wall.GetComponent<Wall>().TakeDamage(10); // TODO
				break;
			case PersonState.AttackingDonald:
				donald.GetComponent<Donald>().TakeDamage(10); // TODO
				break;
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Wall")
		{
			personState = PersonState.AttackingWall;
		}
		else if (collision.gameObject.tag == "Donald")
		{
			personState = PersonState.AttackingDonald;
		}
	}

	void OnCollisionExit(Collision collision)
	{
		if (collision.gameObject.tag == "Wall")
		{
			personState = PersonState.Walking;
		}
	}

	void OnParticleCollision(GameObject other)
	{
		TakeDamage();
	}

	void TakeDamage()
	{
		// TODO
		health -= 10;

		if (health <= 0)
		{
			Die();
		}
	}

	void Die()
	{
		Destroy(this);
	}
}
