using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(RectTransform))]
[ExecuteInEditMode]
public class UIPositioner : MonoBehaviour
{
    [Range(0, 100)][SerializeField] private float widthPercent = 50f;
    [Range(0, 100)][SerializeField] private float heightPercent = 50f;
    [SerializeField] private UIPositionerType type;
    [SerializeField] private SquareResizeType resizeBehaviour;

    private RectTransform rectTransform;

    private float _widthRelative;
    private float _heightRelative;

    private float _prevHeight;
    private float _prevWidth;
    private float _prevWidthPercent;
    private float _prevHeightPercent;
    private SquareResizeType _resizeBehaviour;
    private void Update()
    {
        if(rectTransform == null) rectTransform = GetComponent<RectTransform>();

        SwitchType(type);
        // If anything has changed
        if (_widthRelative != _prevWidth || _heightRelative != _prevHeight || widthPercent != _prevWidthPercent || heightPercent != _prevHeightPercent || _resizeBehaviour != resizeBehaviour)
        {
            Vector2 newSize;
            switch (resizeBehaviour)
            {
                case SquareResizeType.UNCONSTRAINED:
                    newSize = new(_widthRelative * (widthPercent / 100f), _heightRelative * (heightPercent / 100f));
                    break;
                case SquareResizeType.HEIGHT_MATCHES_WIDTH:
                    float width = _widthRelative * (widthPercent / 100f);
                    newSize = new(width, width);
                    break;
                case SquareResizeType.WIDTH_MATCHES_HEIGHT:
                    float height = _heightRelative * (heightPercent / 100f);
                    newSize = new(height, height);
                    break;
                default:
                    throw new System.NotImplementedException();
            }
            Vector2 deltaSize = new(newSize.x - rectTransform.sizeDelta.x, newSize.y - rectTransform.sizeDelta.y);
            rectTransform.sizeDelta = newSize;
            switch (Enum.GetAnchorPresetFromVector(rectTransform.anchorMax))
            {
                case AnchorPresets.BOTTOM_LEFT:     ////////////////
                                                    // Do nothing
                    break;
                case AnchorPresets.BOTTOM_CENTER:   ////////////////
                    deltaSize.x = 0;                // Don't move X
                    break;                          
                case AnchorPresets.BOTTOM_RIGHT:    ////////////////
                    deltaSize.x = -deltaSize.x;     // Invert X 
                    break;
                case AnchorPresets.TOP_LEFT:        ////////////////
                    deltaSize.y = -deltaSize.y;     // Invert Y 
                    break;
                case AnchorPresets.TOP_CENTER:      ////////////////
                    deltaSize.x = 0;                // Don't move X
                    deltaSize.y = -deltaSize.y;     // Invert Y
                    break;
                case AnchorPresets.TOP_RIGHT:       ////////////////
                    deltaSize.x = -deltaSize.x;     // Invert X
                    deltaSize.y = -deltaSize.y;     // Invert Y
                    break;
                case AnchorPresets.CENTER_LEFT:     ////////////////
                    deltaSize.y = 0;                // Don't move Y
                    break;
                case AnchorPresets.CENTER:          ////////////////
                    deltaSize.x = 0;                // Don't move X
                    deltaSize.y = 0;                // Don't move Y
                    break;
                case AnchorPresets.CENTER_RIGHT:    ////////////////
                    deltaSize.x = -deltaSize.x;     // Invert X
                    deltaSize.y = 0;                // Don't move Y
                    break;
            }
            rectTransform.anchoredPosition = new(rectTransform.anchoredPosition.x + deltaSize.x / 2, rectTransform.anchoredPosition.y + deltaSize.y / 2);
            _prevWidth = _widthRelative;
            _prevHeight = _heightRelative;
            _prevWidthPercent = widthPercent;
            _prevHeightPercent = heightPercent;
        }
    }
    private void SwitchType(UIPositionerType type)
    {
        switch (type)
        {
            case UIPositionerType.RELATIVE_TO_SCREEN:
                _widthRelative = Screen.width;
                _heightRelative = Screen.height;
                break;
            case UIPositionerType.RELATIVE_TO_PARENT:
                _widthRelative = transform.parent.GetComponent<RectTransform>().sizeDelta.x;
                _heightRelative = transform.parent.GetComponent<RectTransform>().sizeDelta.y;
                break;
        }
    }
}
