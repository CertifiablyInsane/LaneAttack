using UnityEngine;
using UnityEngine.InputSystem;

public class SwipeDetection : Singleton<SwipeDetection>
{
    public delegate void Swipe(Vector2 direction);
    public event Swipe OnSwipe;

    [SerializeField] private float minSwipeDistance = 100f;
    [SerializeField] private float holdTimeout = 1.0f;

    private Vector2 _swipe1StartPos;
    private Vector2 _swipe2StartPos;
    private float _swipe1StartTime;
    private float _swipe2StartTime;

    private void OnEnable()
    {
        InputManager.OnStartTouch1 += Swipe1Start;
        InputManager.OnEndTouch1 += Swipe1End;
        InputManager.OnStartTouch2 += Swipe2Start;
        InputManager.OnEndTouch2 += Swipe2End;
    }
    private void OnDisable()
    {
        InputManager.OnStartTouch1 -= Swipe1Start;
        InputManager.OnEndTouch1 -= Swipe1End;
        InputManager.OnStartTouch2 -= Swipe2Start;
        InputManager.OnEndTouch2 -= Swipe2End;
    }
    private void Swipe1Start(Vector2 pos)
    {   
        _swipe1StartPos = pos;
        _swipe1StartTime = Time.time;
    }
    private void Swipe1End(Vector2 pos)
    {
        DetectSwipe(pos - _swipe1StartPos, Time.time - _swipe1StartTime);
    }
    private void Swipe2Start(Vector2 pos)
    {
        _swipe2StartPos = pos;
        _swipe2StartTime = Time.time;
    }
    private void Swipe2End(Vector2 pos)
    {
        DetectSwipe(pos - _swipe2StartPos, Time.time - _swipe2StartTime);
    }
    private void DetectSwipe(Vector2 deltaPos, float deltaTime)
    {
        if (deltaTime > holdTimeout) return;

        Vector2 direction = Vector2.zero;
        if(Mathf.Abs(deltaPos.x) > minSwipeDistance)
        {
            direction.x = Mathf.Clamp(deltaPos.x, -1, 1);
        }
        if (Mathf.Abs(deltaPos.y) > minSwipeDistance)
        {
            direction.y = Mathf.Clamp(deltaPos.y, -1, 1);
        }
        if(direction != Vector2.zero) 
        {
            OnSwipe?.Invoke(direction);
        }
    }
}
