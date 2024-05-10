using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AdvancedButton : MonoBehaviour
{
    [Header("Start State")]
    [SerializeField] protected new bool enabled = true;
    [Header("Pointers")]
    [SerializeField] protected Image background;
    [SerializeField] protected TextMeshProUGUI label;
    [SerializeField] protected Button button;
    [Header("Colours")]
    [SerializeField] protected Color COLOR_ENABLED;
    [SerializeField] protected Color COLOR_DISABLED;
    public int id { get; protected set; }

    protected void Start()
    {
        if (enabled)
            Enable();
        else Disable();
    }

    public void AddOnClick(UnityAction<AdvancedButton> action)
    {
        button.onClick.AddListener(() => action(this));
    }
    public void SetText(string text)
    {
        label.text = text;
    }
    public void SetID(int id)
    {
        this.id = id;
    }
    public void SetColor(Color color)
    {
        background.color = color;
    }
    public void Enable()
    {
        enabled = true;
        button.enabled = true;
        SetColor(COLOR_ENABLED);
    }
    public void Disable()
    {
        enabled = false;
        button.enabled = false;
        SetColor(COLOR_ENABLED);
    }
}
