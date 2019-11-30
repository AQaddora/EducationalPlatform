using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class MainVideoPlayer : MonoBehaviour
{
	public static string url;
	public Fader playButton, Controllers, videoEndPanel, loadingVideo;
	private RawImage rawImage;
	private VideoPlayer videoPlayer;
	public Slider progressBar;
	public Image progressBarImg;

	private GameObject loadingUrl;

	private bool isEnded = false;

	private void Awake()
	{
		url = GetLevels.videoUrl;
	}
	void Start()
    {
		videoPlayer = GetComponent<VideoPlayer>();
		rawImage = GetComponent<RawImage>();
		videoPlayer.url = url;
		Destroy(GameObject.Find("LOADING"));
		StartCoroutine(PlayVideo());
    }

	IEnumerator PlayVideo()
	{
		isEnded = false;
		videoPlayer.Prepare();
		while (!videoPlayer.isPrepared)
		{
			Debug.Log(videoPlayer.frameCount);
			yield return null;
		}
		loadingVideo.FadeOut();
		videoPlayer.Play();
		Controllers.FadeIn();
		rawImage.texture = videoPlayer.texture;

		while (true)
		{
			progressBarImg.fillAmount = (float)videoPlayer.frame / (float)videoPlayer.frameCount;

			if (((ulong)videoPlayer.frame >= videoPlayer.frameCount - 10) && videoPlayer.frameCount > 0 && videoPlayer.frame > 0)
			{
				Debug.Log("ENDED");
				playButton.FadeOut();
				videoEndPanel.FadeIn();
				isEnded = true;
			}
			yield return new WaitForEndOfFrame();
		}
	}


	public void PlayPauseToggle()
	{
		if (isEnded)
		{
			isEnded = false;
			videoPlayer.Stop();
			videoPlayer.Play();
			videoEndPanel.FadeOut();
			return;
		}

		if (videoPlayer.isPlaying)
		{
			videoPlayer.Pause();
			playButton.FadeIn();

		}
		else
		{
			videoPlayer.Play();
			playButton.FadeOut();
		}
	}

	public void Seek(float seekPoint)
	{
		Debug.Log("Seeking: " + (long)(seekPoint * (float)videoPlayer.frameCount) + " out of " + videoPlayer.frameCount);
		videoPlayer.frame = (long)(seekPoint * (float)videoPlayer.frameCount);
		progressBar.value = seekPoint;
		progressBarImg.fillAmount = seekPoint;
		videoPlayer.Play();
		videoEndPanel.cg.alpha = 0;
		videoEndPanel.cg.blocksRaycasts = false;
		playButton.FadeOut();
	}

	public void HideVideo()
	{
		GetComponentInParent<Fader>().FadeOut();
		videoPlayer.Stop();
	}
}
