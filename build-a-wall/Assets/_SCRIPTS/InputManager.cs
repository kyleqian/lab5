using UnityEngine;

public class InputManager : MonoBehaviour
{
	public static InputManager Instance;
	
	// From 0.0 to 1.0
	public float bellyBreath;
	public float mouthBreath;
	
	bool debugModeOn = true; // Use keyboard for input

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
		
	}
	
	void Update()
	{
		if (debugModeOn)
		{
			mouthBreath = Input.GetAxis("Vertical");
			bellyBreath = Input.GetAxis("Horizontal");
		}
		else
		{
			// Integrate sensors
		}
	}
}
