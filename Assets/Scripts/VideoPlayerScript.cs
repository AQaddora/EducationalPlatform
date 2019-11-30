using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerScript : MonoBehaviour
{
	VideoPlayer video;
	UIAlpha videoPanel;
	public UIAlpha blackScreen;

	private void Awake()
	{
		video = GetComponent<VideoPlayer>();
		videoPanel = GetComponent<UIAlpha>();

		video.loopPointReached += EndReached;
	}

	void EndReached(VideoPlayer vp)
	{
		videoPanel.Hide();
	}
}
