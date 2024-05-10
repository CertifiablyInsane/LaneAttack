using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RadioButtonGroup : MonoBehaviour
{
    [SerializeField] private bool onlyTriggerEventOnConfirm = false;
    [SerializeField] private bool selectFirstButtonOnStart = false;
    [SerializeField] private ButtonEventPair[] buttonEventPairs;
    [SerializeField] private Color selectedColor;

    private ButtonEventPair _selectedButton;

    private void Start()
    {
        for (int i = 0; i < buttonEventPairs.Length; i++)
        {
            buttonEventPairs[i].button.AddOnClick(ButtonPressed);
            buttonEventPairs[i].button.SetID(i);
        }
        if (selectFirstButtonOnStart)
        {   
            // Set selected as first
            _selectedButton = buttonEventPairs[0];
            if(!onlyTriggerEventOnConfirm)
            {
                // Immediately activate first if not waiting for a confirm
                _selectedButton.action?.Invoke();
            }
        }
    }
    public void ButtonPressed(AdvancedButton button)
    {
        if(_selectedButton.button == button)
            return;     // Do nothing if already selected.

        _selectedButton = buttonEventPairs[button.id];
        if(!onlyTriggerEventOnConfirm)
            buttonEventPairs[button.id].action?.Invoke();
    }
    public void ConfirmSelection()
    {
        _selectedButton.action?.Invoke();
    }
}

[Serializable]
public struct ButtonEventPair
{
    public AdvancedButton button;
    public UnityEvent action;
}
