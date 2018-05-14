﻿using UnityEngine;

public class Wall : MonoBehaviour
{
	public static Wall Instance;

	public float health;
	public bool healthy;

	float wallTimeout = 10;
	float wallLastDestroyed = -Mathf.Infinity;

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
		health = 10000;
		healthy = true;
		InputManager.CrossedBellyBreathThreshold += ((bool ascending) =>
		{
			Turn(ascending);
		});
	}

	void Update()
	{
		if (!healthy)
		{
			if (Time.time - wallLastDestroyed >= wallTimeout)
			{
				healthy = true;
				health = 10000;
			}
		}
	}

	public void TakeDamage(float damage)
	{
		// TODO
		health -= damage;

		if (health <= 0)
		{
			Turn(false);
			wallLastDestroyed = Time.time;
			healthy = false;
		}
	}

	public void Turn(bool on)
	{
		if (!healthy)
		{
			return;
		}

		float to = 0.64f;
		float from = -1.5f;
		
		if (!on)
		{
			float temp = to;
			to = from;
			from = temp;
		}

		LeanTween.value(gameObject, (float v) => {
			transform.position = new Vector3(-5.99f, v, 2.13f);
		}, from, to, 1.2f).setEaseInCubic();
	}
}
