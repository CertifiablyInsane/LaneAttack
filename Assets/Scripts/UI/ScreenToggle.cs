using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenToggle : MonoBehaviour
{
    [SerializeField] private GameObject[] screens;
    [SerializeField] private bool enableFirstScreenOnStart = false;

    private GameObject _currentScreen;

    private void Start()
    {
        DisableAll();
        if (enableFirstScreenOnStart)
            SetScreen(0);
    }
    public void SetScreen(int screen)
    {
        if(_currentScreen != null)
            _currentScreen.SetActive(false);
        _currentScreen = screens[screen];
        _currentScreen.SetActive(true);
    }
    public void DisableAll()
    {
        foreach (GameObject screen in screens)
        {
            screen.SetActive(false);
        }
        _currentScreen = null;
    }
}
