using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class UIAlpha : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    public float duration = 0.3f;
    public float alpha = 1;
    // Start is called before the first frame update
    void Awake()
    {
		//duration = 0.3f;
		canvasGroup = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canvasGroup.alpha != alpha) {
            canvasGroup.alpha = Mathf.Clamp01(canvasGroup.alpha + ((alpha == 1) ? 1 : -1) * Time.deltaTime / duration);
        }
    }
    public void Show() {
        alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

	public void ShowAfterASecond()
	{
		Invoke("Show", 1f);
	}
    public void Hide() {
        alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

	public void FadeInOut()
	{
		Show();
		Invoke("Hide", duration + 0.2f);
	}
}
