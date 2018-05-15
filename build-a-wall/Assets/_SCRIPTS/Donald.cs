using UnityEngine;
using UnityEngine.UI;

public class Donald : MonoBehaviour
{
	public ParticleSystem ps;
    public Text healthText;

    float health;
    GameObject mainCamera;

    public static GameObject GetFirstChildTagged(GameObject obj, string tag)
    {
        foreach (Transform t in obj.GetComponentsInChildren<Transform>())
        {
            if (tag.Equals(t.tag))
            {
                return t.gameObject;
            }
        }
        return null;
    }

    void Start()
    {
        health = 500f;
        mainCamera = GetFirstChildTagged(gameObject, "MainCamera");
    }

    void Update()
	{
        healthText.text = "Robosuit integrity: " + health;

        float mouthBreath = InputManager.Instance.mouthBreath;

		var emissionModule = ps.emission;
		emissionModule.enabled = mouthBreath > 0.1;
        emissionModule.rateOverTime = 12f + mouthBreath * 1.5f;

		var shapeModule = ps.shape;
        //shapeModule.scale = new Vector3(mouthBreath, mouthBreath, shapeModule.scale.z);
        shapeModule.angle = 10 + mouthBreath * 35;

        if (!InputManager.Instance.usingHmd)
        {
            // Update the camera look.
            transform.Rotate(new Vector3(0, 0, 2f*InputManager.Instance.lookHoriz));
            mainCamera.transform.Rotate(
                new Vector3(2f * InputManager.Instance.lookVert,
                    0f,
                    0f));
        }
	}

	public void TakeDamage(float damage)
	{
		health -= damage;
        
		if (health <= 0)
		{
            health = 0;
			GameManager.Instance.GameOver();
		}
	}
}
