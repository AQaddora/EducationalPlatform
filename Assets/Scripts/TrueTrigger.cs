using UnityEngine;

public class TrueTrigger : MonoBehaviour
{
	public GameObject trueEffect;
	public UIAlpha blackSceen;

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag.Equals("Player"))
		{
			trueEffect.SetActive(true);
			blackSceen.ShowAfterASecond();
			Invoke("ReloadScene", blackSceen.duration + 1.3f);
		}		
	}

	void ReloadScene()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
	}
}
