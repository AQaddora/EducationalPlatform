using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using ArabicSupport; 

public class Login : MonoBehaviour
{
	public static Login Instance;

	public static string accessToken = "";
	public static int id = 0;
	public string loginUrl;
	public UnityEngine.UI.InputField user_name_input;
	public UnityEngine.UI.InputField password_input;

	void Awake()
	{
		Instance = this;
	}

	public void Login_Button()
	{
		WWWForm form = new WWWForm();
		form.AddField("user_name", user_name_input.text);
		form.AddField("password", password_input.text);
		StartCoroutine(LoginWebRequest(loginUrl, form));
	}

	IEnumerator LoginWebRequest(string url, WWWForm form)
	{
		UIManager.Instance.ShowLoadingUrl();
		UnityWebRequest request = UnityWebRequest.Post(url, form);
		yield return request.SendWebRequest();

		LoginOutput output = JsonUtility.FromJson<LoginOutput>(request.downloadHandler.text);
		if (output.status)
		{
			accessToken = output.student.accessToken;
			id = output.student.id;
			Debug.Log(accessToken);
			PlayerPrefs.SetString("accessToken", accessToken);
			PlayerPrefs.SetInt("ID", id);
			UIManager.Instance.ShowNotification("تسجيل الدخول", "تم تسجيل دخولك بنجاح");
			UIManager.Instance.ShowAddGrade();
			UIManager.Instance.HideLogin();
			UIManager.Instance.HideRegister();
		}
		else
		{
			UIManager.Instance.ShowError("تسجيل الدخول", "خطأ في تسجيل الدخول");
		}
		UIManager.Instance.HideLoadingUrl();
	}
}

public class StudentLoginData
{
	public string user_name;
	public string password;

	public StudentLoginData(string user_name, string password)
	{
		this.user_name = user_name;
		this.password = password;
	}
}

public class LoginOutput
{
	public bool status;
	public string msg;
	public Student student;
}

[System.Serializable]
public class Student
{
	public int id;
	public string full_name;
	public string user_name;
	public int? grade;
	public string accessToken;
}