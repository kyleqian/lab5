﻿using UnityEngine;

public class Person : MonoBehaviour
{
	public enum PersonState { Walking, AttackingWall, AttackingDonald, Dying }
	public PersonState personState;
	
	public GameObject donald;

	float health = 100;
	Animator animator;
	Rigidbody rb;
	float speed;
	float initPitch;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		animator = GetComponent<Animator>();
		personState = PersonState.Walking;
		animator.Play("run");
		speed = Random.Range(100f, 300f);

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
				rb.AddForce(transform.forward * speed);
				break;
			case PersonState.AttackingWall:
				Wall.Instance.TakeDamage(10); // TODO
				break;
			case PersonState.AttackingDonald:
				donald.GetComponent<Donald>().TakeDamage(10); // TODO
				break;
            case PersonState.Dying:
            default:
                break;
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.transform.tag == "Wall")
		{
			personState = PersonState.AttackingWall;
			animator.Play("melee");
		}
		else if (collision.transform.tag == "Donald")
		{
			personState = PersonState.AttackingDonald;
			animator.Play("melee");
		}
	}

	void OnCollisionExit(Collision collision)
	{
		if (collision.transform.tag == "Wall" || collision.transform.tag == "Donald")
		{
			personState = PersonState.Walking;
			animator.Play("run");
		}
	}

	public void TakeDamage(float damage)
	{
		// TODO
        if (personState != PersonState.Dying)
        {
            health -= damage;
            rb.AddForce(1000 * (transform.position - donald.transform.position + new Vector3(0, 2.0f, 0)));

            if (health <= 0)
            {
                Die();
            }
        }
	}

	void Die()
	{
        personState = PersonState.Dying;
		animator.Play("death");
		Destroy(gameObject, 2);
	}
}
