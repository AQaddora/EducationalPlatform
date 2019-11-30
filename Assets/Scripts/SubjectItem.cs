using UnityEngine;
using UnityEngine.UI;

public class SubjectItem : MonoBehaviour
{
	public Text text;
	public string subjectName;
	public int subject_id;

	public void OnClickButton()
	{
		GetSubjects.OnButtonClick(subject_id);
		UIManager.Instance.subjectText.text = ArabicSupport.ArabicFixer.Fix(subjectName);
		UIManager.Instance.HideAll();
		UIManager.Instance.ShowLevels();
	}
}
