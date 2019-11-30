using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneEntrance : MonoBehaviour
{
    public int sceneIndex;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            Game.GameState.controllable = false;
            GameObject.Find("BlackScreen").GetComponent<UIAlpha>().Show();
            SceneManager.LoadSceneAsync(sceneIndex);
        }
    }
}
