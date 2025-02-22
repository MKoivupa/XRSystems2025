using UnityEngine;
using UnityEngine.InputSystem;

public class BreakOut : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform roomLocation;
    public Transform extLocation;
    private bool isInRoom;
    public InputActionReference action;
    void Start()
    {
        isInRoom = true;
        action.action.Enable();
        action.action.performed += (ctx) =>
        {
            breakOut();
        };
    }

    private void breakOut(){
        if(isInRoom){
            transform.position = extLocation.position;
            isInRoom = false;
        } else {
            transform.position = roomLocation.position;
            isInRoom = true;
        }
    }

}
