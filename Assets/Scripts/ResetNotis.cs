using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetNotis : MonoBehaviour
{
	private Animator animator;

	private void OnEnable()
	{
		animator = GetComponent<Animator>();
		Invoke("FadeOut", 5f);
	}

	public void ResetNotifications()
	{
		UIManager.Instance.ResetNotifications();
	}

	void FadeOut()
	{
		Debug.Log("Fade OUT ANIMATOR");
		//Invoke("ResetNotifications", 0.5f);
		ResetNotifications();
		animator.SetTrigger("FadeOut");
	}
}
