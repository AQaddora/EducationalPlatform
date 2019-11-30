using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingManager : MonoBehaviour
{
	private static UnityEngine.UI.Slider loadingBar;
	private static Fader fader;
	public static LoadingManager Instance;

	private void Awake()
	{
		Instance = this;
		loadingBar = GetComponentInChildren<UnityEngine.UI.Slider>();
		fader = GetComponent<Fader>();
	}

	public IEnumerator HideLoadingScreen()
	{
		for (float i = 0; i >= 0; i += (Time.deltaTime))
		{
			loadingBar.value = i;
			if (i > 1)
				break;
			yield return null;
		}
		fader.FadeOut();
	}
}
