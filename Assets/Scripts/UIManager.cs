using UnityEngine;
using ArabicSupport;
using System;

public class UIManager : MonoBehaviour
{
	public static UIManager Instance;

	public Fader login, courses, levels, loading, register, addGrade, loadingUrl, newsPanel;

	public GameObject errorMsg, notiMsg;

	public UnityEngine.UI.Text errorText, errorTitleText, notiText, notiTitleText, gradeText, subjectText;

    void Awake()
    {
		Instance = this;    
    }

	public void HideAll()
	{
		login.FadeOut();
		courses.FadeOut();
		levels.FadeOut();
		loading.FadeOut();
		register.FadeOut();
		addGrade.FadeOut();
		newsPanel.FadeOut();
	}
	public void ShowLogin()
	{
		login.FadeIn();
	}
	public void ShowNews()
	{
		newsPanel.FadeIn();
	}
	public void HideNews()
	{
		newsPanel.FadeOut();
	}

	public void ShowLoadingUrl()
	{
		loadingUrl.FadeIn();
	}

	public void HideLoadingUrl()
	{
		loadingUrl.FadeOut();
	}
	public void HideLogin()
	{
		login.FadeOut();
	}

	public void ShowRegister()
	{
		register.FadeIn();
	}

	public void HideRegister()
	{
		register.FadeOut();
	}

	public void ShowLevels()
	{
		levels.FadeIn();
	}

	public void HideLevels()
	{
		levels.FadeOut();
	}

	public void ShowCourses()
	{
		courses.FadeIn();
	}

	public void HideCourses()
	{
		courses.FadeOut();
	}

	public void LoadGame()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene(1);
	}

	public void LogOut()
	{
		PlayerPrefs.DeleteAll();
		UnityEngine.SceneManagement.SceneManager.LoadScene(0);
	}

	public void ShowError(string title, string errorMsg)
	{
		this.notiMsg.SetActive(false);
		errorText.text = ArabicFixer.Fix(errorMsg);
		errorTitleText.text = ArabicFixer.Fix(title);
		this.errorMsg.SetActive(true);
	}

	public void ShowAddGrade()
	{
		addGrade.FadeIn();
	}

	public void ShowNotification(string title, string notiMsg)
	{
		this.errorMsg.SetActive(false);
		notiText.text = ArabicFixer.Fix(notiMsg);
		notiTitleText.text = ArabicFixer.Fix(title);
		this.notiMsg.SetActive(true);
	}

	public void ResetNotifications()
	{
		this.notiMsg.SetActive(false);
		this.errorMsg.SetActive(false);
	}
}
