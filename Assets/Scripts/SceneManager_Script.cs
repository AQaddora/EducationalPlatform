using System.Collections.Generic;
using UnityEngine;

public class SceneManager_Script : MonoBehaviour
{
	public static SceneManager_Script Instance;
	public UnityStandardAssets.Cameras.AutoCam autoCam;
	public Transform[] playerPositions;
	public Transform[] questionPositions;
	[Space]public GameObject questionEntrancePrefab;
	public Transform questionEntParent;

	[Space] public Light _light;
	public AudioClip[] musicClips;
	public Transform Player;
	public Color[] lightColors;

	public GameObject mcq_Game;

	private void Awake()
	{
		Instance = this;
	}
	private void Start()
	{
		UIManager.Instance.loadingUrl.FadeOut();
		Player.position = playerPositions[UnityEngine.Random.Range(0, playerPositions.Length)].position;
		_light.color = lightColors[Random.Range(0, lightColors.Length)];
		//GenerateQuestions();
	}

	public void PlayNowButton()
	{
		autoCam.enabled = true;
		GetComponent<AudioSource>().clip = musicClips[UnityEngine.Random.Range(0, musicClips.Length)];
		GetComponent<AudioSource>().Play();
	}

	public void MovePlayerRandomly()
	{
		Player.position = playerPositions[UnityEngine.Random.Range(0, playerPositions.Length)].position;
	}
	private void GenerateQuestions()
	{
		//MCQ_Manager.question = GetLevels.mcqQuestions[0];
		//mcq_Game.SetActive(true);
		
		/*
		Debug.Log("QUESTIONS: " + questions.Length);
		List<int> randomPositions = new List<int>(questions.Length);
		int rand;
		for (int i = 0; i < questions.Length; i++)
		{
			do
			{
				rand = Random.Range(0, questionPositions.Length);
			} while (!randomPositions.Contains(rand));
			randomPositions.Add(rand);
		}

		for (int i = 0; i < questions.Length; i++)
		{
			GameObject genQuestion = Instantiate(questionEntrancePrefab, questionPositions[randomPositions[i]].position
				, questionPositions[randomPositions[i]].rotation);

			genQuestion.transform.SetParent(questionEntParent);
			//SET DATA FOR ENTRANCE;
		}*/
	}

	public GameObject backVideoPanel;
	public GameObject videoPanel;
	public void HomeButton()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene(0);
	}

	public void ShowVideoButton()
	{
		backVideoPanel.SetActive(true);
		videoPanel.GetComponentInChildren<MainVideoPlayer>().Seek(0f);
		videoPanel.GetComponent<Fader>().FadeIn();
		videoPanel.SetActive(true);
	}
}
