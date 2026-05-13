using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputActions : MonoBehaviour
{
    private Controls m_actionMap;
    public event Action<float> UpOrDown;

    private void Awake()
    {
        m_actionMap = new Controls();
    }

    private void OnEnable()
    {
        m_actionMap.Enable();
        m_actionMap.Default.UpDown.performed += HandleUpDown;
        m_actionMap.Default.UpDown.canceled += HandleUpDown;
    }

    private void OnDisable()
    {
        m_actionMap.Disable();
    }

    private void HandleUpDown(InputAction.CallbackContext ctx)
    {
        UpOrDown?.Invoke(ctx.ReadValue<float>());
    }
    
}
