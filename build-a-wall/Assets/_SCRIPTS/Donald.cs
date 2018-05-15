using UnityEngine;

public class Donald : MonoBehaviour
{
	public ParticleSystem ps;

	float health = 5000;
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
        mainCamera = GetFirstChildTagged(gameObject, "MainCamera");
    }

    void Update()
	{
		float mouthBreath = InputManager.Instance.mouthBreath;

		var emissionModule = ps.emission;
		emissionModule.enabled = mouthBreath > 0.1;

		var shapeModule = ps.shape;
		shapeModule.scale = new Vector3(mouthBreath, mouthBreath, shapeModule.scale.z);

        if (!InputManager.Instance.usingHmd)
        {
            // Update the camera look.
            Vector3 currentLook = mainCamera.transform.eulerAngles;

            mainCamera.transform.eulerAngles =
                new Vector3(currentLook.x + 2f * InputManager.Instance.lookVert,
                    currentLook.y + 2f * InputManager.Instance.lookHoriz,
                    currentLook.z);
        }
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
