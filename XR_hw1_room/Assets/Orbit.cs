using UnityEngine;

public class Orbit : MonoBehaviour
{
    // Update is called once per frame
    public float degreesPerSecond = 2.0f;
    void Update()
    {
        transform.Rotate(0, degreesPerSecond * Time.deltaTime, 0);
    }
}
