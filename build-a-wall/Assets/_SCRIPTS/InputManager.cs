using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class InputManager : MonoBehaviour
{
	[Serializable]
	public class NeulogData
	{
		public int[] GetSensorValue;

		public static int Parse(string jsonString)
		{
			return JsonUtility.FromJson<NeulogData>(jsonString).GetSensorValue[0];
		}
	}

	public static InputManager Instance;

	public delegate void OnCrossedBellyBreathThreshold(bool ascending);
    public static event OnCrossedBellyBreathThreshold CrossedBellyBreathThreshold;
	
	// From 0.0 to 1.0
	public float bellyBreath;
	public float mouthBreath;
    public float lookHoriz = 0.0f;
    public float lookVert = 0.0f;
	public int neulogRequestsPerSecond;
    public bool usingHmd;

	private const string NEULOG_URL = "http://localhost:22002/NeuLogAPI?GetSensorValue:[Respiration],[1]";
    private Queue<float> breathQueue = new Queue<float>();
    private int breathQueueMaxSize;
	private bool debugModeOn = true; // Use keyboard for input
    private bool useXbox = true; // if debug mode on, actually use xbox for input

	void Awake()
	{
        usingHmd = !debugModeOn;
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
		if (!debugModeOn)
		{
		    // Set off method to periodically ping Neulog API
            breathQueueMaxSize = neulogRequestsPerSecond / 2;
            InvokeRepeating("QueryNeulog", 0.0f, 1.0f / neulogRequestsPerSecond);
		}
	}
	
	void Update()
	{
		if (debugModeOn)
		{
			float newMouthBreath = useXbox ? Input.GetAxis("XboxRightTrigger") : Input.GetAxis("Vertical");
			// float newMouthBreath = 1;
			float newBellyBreath = useXbox ? Input.GetAxis("XboxLeftTrigger") : Input.GetAxis("Horizontal");

			if (newBellyBreath >= 0.8 && bellyBreath < 0.8)
			{
				CrossedBellyBreathThreshold(true);
			}
			else if (newBellyBreath < 0.8 && bellyBreath >= 0.8)
			{
				CrossedBellyBreathThreshold(false);
			}

			mouthBreath = newMouthBreath;
			bellyBreath = newBellyBreath;

            if (useXbox)
            {
                lookHoriz = Input.GetAxis("XboxRightStickHorizontal");
                lookVert = Input.GetAxis("XboxRightStickVertical");
            }
        }
	}

	void QueryNeulog()
    {
        UnityWebRequest req = UnityWebRequest.Get(NEULOG_URL);
        StartCoroutine(HandleNeulog(req));
    }

    IEnumerator HandleNeulog(UnityWebRequest req)
    {
        yield return req.SendWebRequest();

        if (req.isNetworkError || req.isHttpError)
        {
            Debug.LogWarning(req.error);
        }
        else
        {
            bellyBreath = NeulogData.Parse(req.downloadHandler.text);
            breathQueue.Enqueue(bellyBreath);
            if (breathQueue.Count > breathQueueMaxSize)
            {
                breathQueue.Dequeue();
            }

            // if (breathQueue.Count == breathQueueMaxSize)
            // {
            //     breathingIn = (breath / breathQueue.Peek() >= breatheInThreshold);
            // }
        }
    }
}
