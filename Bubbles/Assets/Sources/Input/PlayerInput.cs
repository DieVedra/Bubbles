using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

public class PlayerInput
{
    private TouchControl _touchControl;
    private Camera _camera;
    private Ball ball;
    private RaycastHit2D hit;
    public Action<Ball> OnBallDetected;
    public PlayerInput()
    {
        _touchControl = new TouchControl();
        _camera = Camera.main;
    }
    public void TouchEnable()
    {
        _touchControl.Enable();
        if (Application.isEditor)
        {
            TouchSimulation.Enable();
        }
        _touchControl.Touch.TouchPress.started += context => UpdateValueTouchScreen(context);
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown += FingerDown;
    }
    private void UpdateValueTouchScreen(InputAction.CallbackContext context)
    {
        LaunchRay(_touchControl.Touch.TouchPosition.ReadValue<Vector2>());
    }
    private void FingerDown(Finger finger)
    {
        LaunchRay(finger.screenPosition);
    }
    private void LaunchRay(Vector2 pos)
    {
        Vector3 position = _camera.ScreenToWorldPoint(new Vector3(pos.x, pos.y, _camera.nearClipPlane));
        hit = Physics2D.Raycast(position, Vector2.down);

        if (hit.collider.TryGetComponent<Ball>(out ball))
        {
            OnBallDetected?.Invoke(ball);
        }

    }
    public void TouchDisable()
    {
        _touchControl.Disable();
        if (Application.isEditor)
        {
            TouchSimulation.Enable();
        }
        _touchControl.Touch.TouchPress.started -= context => UpdateValueTouchScreen(context);
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown -= FingerDown;
    }
}
