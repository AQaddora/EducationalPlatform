using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;



public class BackgroundVideoPlayer : MonoBehaviour
{

	public VideoPlayer videoPlayer;
	public RawImage rawImage;

    // Start is called before the first frame update
    void Start()
    {
		StartCoroutine(PlayVideo());   
    }

	IEnumerator PlayVideo()
	{
		videoPlayer.Prepare();
		while(!videoPlayer.isPrepared)
			yield return null;

		videoPlayer.Play();
		rawImage.texture = videoPlayer.texture;
		StartCoroutine(LoadingManager.Instance.HideLoadingScreen());
	}
}
