using UnityEngine;
using UnityEngine.InputSystem;

public class LightSwitch : MonoBehaviour
{
    public Light light;
    public InputActionReference action;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        light = GetComponent<Light>();
        
        Color red = new Color(1f, 0f, 0f, 1f);
        Color white = new Color(1f, 1f, 1f, 1f);

        light.color = Color.white;

        action.action.Enable();
        action.action.performed += (ctx) =>
        {
            SwitchColor();
        };
    }

    // Update is called once per frame
    private void SwitchColor()
    {
        if (light.color == Color.white)
        {
            light.color = Color.red;
        } else {
            light.color = Color.white;
        }
    }
}
