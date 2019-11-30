using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatherPath : MonoBehaviour
{
    public UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter thirdPerson;
    public float speed = 1;
    bool stop = false;
    public Transform lookAt;
    private bool doneTalking = false;
    void Update() {
        if (stop) {
            thirdPerson.Move(Vector3.zero, false, false);

            return;
        }
        if (transform.position.x < 162 && speed > 0) speed *= -1;
        if (transform.position.x > 168 && speed < 0) speed *= -1;
        thirdPerson.Move(Vector3.right * -speed, false, false);

    }
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player" && !doneTalking) {
            doneTalking = true;
            stop = true;
            transform.LookAt(other.transform);
            Invoke("backToPath", 4 * 3);
        }
    }
    void backToPath() {
        transform.LookAt(lookAt);
        stop = false;
    }
}
