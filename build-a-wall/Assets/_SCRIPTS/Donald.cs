using UnityEngine;

public class Donald : MonoBehaviour
{
	public ParticleSystem ps;

	float health = 100;

	void Update()
	{
		float mouthBreath = InputManager.Instance.mouthBreath;

		var emissionModule = ps.emission;
		emissionModule.enabled = mouthBreath > 0.1;

		var shapeModule = ps.shape;
		shapeModule.scale = new Vector3(mouthBreath, mouthBreath, shapeModule.scale.z);
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
