using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundColor : MonoBehaviour
{
	public Gradient colorGrad;
	public float strobeDuration = 2f;

	public void Update()
	{
		float t = Mathf.PingPong(Time.time / strobeDuration, 1f);
		GetComponent<UnityEngine.UI.RawImage>().color = colorGrad.Evaluate(t);
	}
}
