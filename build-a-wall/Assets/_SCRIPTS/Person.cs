using UnityEngine;

public class Person : MonoBehaviour
{
	public enum PersonState { Walking, AttackingWall, AttackingDonald }
	public PersonState personState;
	
	public GameObject donald;
	public GameObject wall;

	float health = 100;
	Animator animator;
	float speed;
	float initPitch;

	void Start()
	{
		personState = PersonState.Walking;
		animator = GetComponent<Animator>();
		animator.Play("run");
		speed = Random.Range(1f, 4f);

		initPitch = transform.eulerAngles.x;
		transform.LookAt(donald.transform);
		transform.eulerAngles = new Vector3(initPitch, transform.eulerAngles.y, transform.eulerAngles.z);
	}
	
	void Update()
	{
		// Characters rotate when attacking?
		transform.LookAt(donald.transform);
		transform.eulerAngles = new Vector3(initPitch, transform.eulerAngles.y, transform.eulerAngles.z);

		switch (personState)
		{
			case PersonState.Walking:
				transform.Translate(Vector3.forward * Time.deltaTime * speed);
				break;
			case PersonState.AttackingWall:
				wall.GetComponent<Wall>().TakeDamage(10); // TODO
				break;
			case PersonState.AttackingDonald:
				donald.GetComponent<Donald>().TakeDamage(10); // TODO
				break;
		}
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.tag == "Wall")
		{
			personState = PersonState.AttackingWall;
			animator.Play("melee");
		}
		else if (collider.tag == "Donald")
		{
			personState = PersonState.AttackingDonald;
			animator.Play("melee");
		}
	}

	void OnTriggerExit(Collider collider)
	{
		if (collider.tag == "Wall")
		{
			personState = PersonState.Walking;
			animator.Play("run");
		}
	}

	void OnParticleCollision(GameObject other)
	{
		Debug.Log("PARTICLE COLLIDE");
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
		animator.Play("death");
		Destroy(this);
	}
}
