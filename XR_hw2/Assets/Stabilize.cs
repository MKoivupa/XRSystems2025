using UnityEngine;

public class Stabilize : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Vector3 eulerAngles = transform.eulerAngles;
        transform.eulerAngles = new Vector3(eulerAngles.x, eulerAngles.y, 0);
    }
}
