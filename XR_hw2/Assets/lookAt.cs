using UnityEngine;

public class lookAt : MonoBehaviour
{
    public Transform target;
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target);
        transform.Rotate(0f,180f,0f);
    }
}
