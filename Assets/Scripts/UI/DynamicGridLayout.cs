using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
[ExecuteInEditMode]
public class DynamicGridLayout : MonoBehaviour
{
    [Range(0, 100)][SerializeField] private float cellHeightPercent = 50f;
    [Range(0, 100)][SerializeField] private float cellWidthPercent = 50f;
    [SerializeField] private UIPositionerType type;
    [SerializeField] private SquareResizeType cellResizeBehaviour;

    private GridLayoutGroup grid;
    private RectTransform rectTransform;

    private float _widthRelative;
    private float _heightRelative;

    private float _prevCellWidth;
    private float _prevCellHeight;
    private float _prevCellWidthPercent;
    private float _prevCellHeightPercent;
    private SquareResizeType _cellResizeBehaviour;
    void Update()
    {
        if (grid == null) grid = GetComponent<GridLayoutGroup>();
        if (rectTransform == null) rectTransform = GetComponent<RectTransform>();
        SwitchType(type);
        if (_widthRelative != _prevCellWidth || _heightRelative != _prevCellHeight || cellWidthPercent != _prevCellWidthPercent || cellHeightPercent != _prevCellHeightPercent || cellResizeBehaviour != _cellResizeBehaviour)
        {
            Vector2 newSize;
            switch (cellResizeBehaviour)
            {
                case SquareResizeType.UNCONSTRAINED:
                    newSize = new(_widthRelative * (cellWidthPercent / 100f), _heightRelative * (cellHeightPercent / 100f));
                    break;
                case SquareResizeType.HEIGHT_MATCHES_WIDTH:
                    float width = _widthRelative * (cellWidthPercent / 100f);
                    newSize = new(width, width);
                    break;
                case SquareResizeType.WIDTH_MATCHES_HEIGHT:
                    float height = _heightRelative * (cellHeightPercent / 100f);
                    newSize = new(height, height);
                    break;
                default:
                    throw new System.NotImplementedException();
            }
            grid.cellSize = newSize;
            _prevCellWidth = _widthRelative;
            _prevCellHeight = _heightRelative;
            _prevCellWidthPercent = cellWidthPercent;
            _prevCellHeightPercent = cellWidthPercent;
            _cellResizeBehaviour = cellResizeBehaviour;
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
            case UIPositionerType.RELATIVE_TO_SELF:
                _widthRelative = rectTransform.sizeDelta.x;
                _heightRelative = rectTransform.sizeDelta.y;
                break;
        }
    }
}
