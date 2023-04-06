using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    private float maxValue;

    public void SetValue(float value)
    {
        fillImage.fillAmount = value / maxValue;
    }

    public void SetColor(Color color)
    {
        fillImage.color = color;
    }


    public void SetMaxValue(int value)
    {
        maxValue = value;
    }
}