using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    private InputSystem_Actions action;
    void Awake()
    {
        action = new InputSystem_Actions();
    }
    private void OnEnable()
    {
        action.Player.Enable();
    }

    private void OnDisable()
    {
        action.Player.Disable();
    }
    void Update()
    {
        
    }
}
