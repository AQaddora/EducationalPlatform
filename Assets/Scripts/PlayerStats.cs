using UnityEngine;

public class PlayerStats : MonoBehaviour
{
	public GameObject exitGuide;
	public UnityEngine.UI.Text scoreText;
	public UnityEngine.UI.Image scoreBG;

	public static PlayerStats Instance;

	private void Awake()
	{
		Instance = this;
	}
	private static int _correctAnswers = 0;
	public static int CorrectAnswers
	{
		get { return _correctAnswers; }
		set
		{
			_correctAnswers = value;
			Instance.scoreText.text = _correctAnswers + "";
			if(_correctAnswers >= 5)
			{
				Instance.exitGuide.SetActive(true);
				QuestionEnterance exit = FindObjectsOfType<QuestionEnterance>()[Random.Range(0, FindObjectsOfType<QuestionEnterance>().Length)];
				foreach (ParticleSystem ps in exit.GetComponentsInChildren<ParticleSystem>())
				{
					ps.startColor = Color.green;
				}
				exit.gameObject.tag = "Leave";
				Instance.scoreBG.color = new Color(46/255f, 204/255f, 113/255f, 1);
			}
		}
	}

	void Update()
    {
        
    }
}
