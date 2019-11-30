using UnityEngine;

public class Rotator : MonoBehaviour
{
    void Update()
    {
		transform.Rotate(0, 0, Time.deltaTime * Random.Range(100, 500));
    }
}
