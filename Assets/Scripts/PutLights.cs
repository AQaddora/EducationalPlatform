using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class PutLights : MonoBehaviour
{
	public GameObject lightPrefab;
	public Transform[] lamps;

	private void OnEnable()
	{
		foreach (Transform t in lamps)
		{
			GameObject obj = Instantiate(lightPrefab, t);
		}
	}
}
