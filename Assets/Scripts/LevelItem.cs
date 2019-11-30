using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelItem : MonoBehaviour
{
	public Text text;
	public int levelId;
	public string levelTitle;
	public string videoUrl;
	public bool isAvailable;

	public void SetItemStuff()
	{
		GetComponent<Button>().interactable = isAvailable;
		text.text = ArabicSupport.ArabicFixer.Fix(levelTitle);
	}

	public void OnClickButton()
	{
		StartCoroutine(GetLevels.Instance.GetQuestions(levelId));
		GetLevels.OnClickButton_Level(videoUrl, levelTitle);
	}
}
