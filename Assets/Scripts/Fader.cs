using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class Fader : MonoBehaviour
{
	public float duration;
	public bool autoWork = false;
	private bool work = false;
	[HideInInspector] public CanvasGroup cg;
	private int direction = -1;

	void Awake()
	{
		cg = GetComponent<CanvasGroup>();
	}

	void Start()
	{
		if (autoWork) Invoke("Hide", 1.3f);
	}

	void Update()
	{
		if (work)
		{
			Debug.Log(this.gameObject.name + ": " + cg.alpha);
			cg.alpha += direction * Time.deltaTime / duration;
			cg.blocksRaycasts = cg.alpha >= 0.5f;
			work = !(cg.alpha == 1 || cg.alpha == 0);
		}
	}

	public void FadeIn()
	{
		direction = 1;
		work = true;
	}

	public void FadeOut()
	{
		direction = -1;
		work = true;
	}
}
