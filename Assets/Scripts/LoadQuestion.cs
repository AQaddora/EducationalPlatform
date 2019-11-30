using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadQuestion : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag.Equals("Player"))
		{
			//LoadAQuestion();
			Debug.Log("Load Question");
		}
	}
}
