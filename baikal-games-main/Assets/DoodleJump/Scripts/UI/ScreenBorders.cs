using UnityEngine;
using UnityEngine.UI;

public class ScreenBorders : MonoBehaviour
{
    [SerializeField] private RectTransform _leftBorderLine;
    [SerializeField] private RectTransform _rightBorderLine;
    [SerializeField] private CanvasScaler _canvaScaler;

    public void SetWidth(float widthNormalized)
    {
        var rectWidth = _canvaScaler.referenceResolution.x * widthNormalized / _canvaScaler.transform.localScale.x;
        _leftBorderLine.anchoredPosition = new Vector2(-rectWidth * 0.5f, _leftBorderLine.anchoredPosition.y);
        _rightBorderLine.anchoredPosition = new Vector2(rectWidth * 0.5f, _rightBorderLine.anchoredPosition.y);
    }
}
