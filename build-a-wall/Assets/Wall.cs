using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

	// Use this for initialization
	public void Turn (bool on){
		float to=0.64f,
			from = -0.98f;
		if (!on){
			float temp = to;
			to=from;
			from=temp;
			
		}
		LeanTween.value(gameObject, (float v)=>{
			transform.position=new Vector3(-5.99f,v, 2.13f);
		}, from, to, 1.2f).setEaseInCubic();
	}

}
