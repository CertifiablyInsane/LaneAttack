using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    // Events
    public delegate void TouchEvent(Vector2 position);
    public static event TouchEvent OnStartTouch1;
    public static event TouchEvent OnEndTouch1;
    public static event TouchEvent OnStartTouch2;
    public static event TouchEvent OnEndTouch2;
    private Controls controls;
    private void Awake()
    {
        controls = new Controls();
    }
    private void OnEnable()
    {
        controls.Enable();
        controls.Touch.Enable();
        controls.Touch.Touch1.started += Touch1Started;
        controls.Touch.Touch1.canceled += Touch1Cancelled;
        controls.Touch.Touch2.started += Touch2Started;
        controls.Touch.Touch2.canceled += Touch2Cancelled;
    }
    private void OnDisable()
    {
        controls.Disable();
        controls.Touch.Disable();
        controls.Touch.Touch1.started -= Touch1Started;
        controls.Touch.Touch1.canceled -= Touch1Cancelled;
        controls.Touch.Touch2.started -= Touch2Started;
        controls.Touch.Touch2.canceled -= Touch2Cancelled;
    }
    // GAME INTERACTIONS
    private void Touch1Started(InputAction.CallbackContext ctx)
    {
        OnStartTouch1?.Invoke(controls.Touch.Position1.ReadValue<Vector2>());
    }
    private void Touch1Cancelled(InputAction.CallbackContext ctx)
    {
        OnEndTouch1?.Invoke(controls.Touch.Position1.ReadValue<Vector2>());
    }
    private void Touch2Started(InputAction.CallbackContext ctx)
    {
        OnStartTouch2?.Invoke(controls.Touch.Position2.ReadValue<Vector2>());
    }
    private void Touch2Cancelled(InputAction.CallbackContext ctx)
    {
        OnEndTouch2?.Invoke(controls.Touch.Position2.ReadValue<Vector2>());
    }

    // UI INTERACTIONS
    public bool holdingLeft { get; private set; }
    public bool holdingRight { get; private set; }

    public void PressLeft(bool b)
    {
        holdingLeft = b;
    }
    public void PressRight(bool b)
    {
        holdingRight = b;
    }
}
