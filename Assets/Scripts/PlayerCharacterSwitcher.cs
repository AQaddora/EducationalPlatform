using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterSwitcher : MonoBehaviour
{
    public PlayerCharacter[] playerCharacters;
    public GameObject player;
    public int nowCharacter = 0;
    public GameObject switchCFX;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyUp (KeyCode.F)) {
            nowCharacter = (nowCharacter + 1) % playerCharacters.Length;
            Game.GameState.characterPersona = nowCharacter;
            Destroy(player);
            Instantiate(switchCFX, player.transform.position + Vector3.up * 2.1f + Vector3.forward - Vector3.right * 0.5f, player.transform.rotation);
            player = Instantiate(playerCharacters[nowCharacter].prefab, player.transform.position, player.transform.rotation);
        }
    }
}
[System.Serializable]
public class PlayerCharacter{
    public GameObject prefab;
    public string name;
}
