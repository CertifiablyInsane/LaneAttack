using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationButton : MonoBehaviour
{
    public void StartPressLeft()
    {
        InputManager.Instance.PressLeft(true);
    }
    public void EndPressLeft()
    {
        InputManager.Instance.PressLeft(false);
    }
    public void StartPressRight()
    {
        InputManager.Instance.PressRight(true);
    }
    public void EndPressRight()
    {
        InputManager.Instance.PressRight(false);
    }
}
