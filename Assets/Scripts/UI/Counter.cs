using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class Counter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private Button decrementButton;
    [SerializeField] private Button incrementButton;

    private readonly int minCount = 1;

    public int MaxCount { get; set; } = 1;

    // Count
    private int count = 1;
    public int Count
    {
        get { return count; }
        set
        {
            count = value;
            countText.text = count.ToString();
        }
    }


    // Is Enabled
    private bool isEnabled;
    public bool IsEnabled
    {
        get { return isEnabled; }
        set
        {
            isEnabled = value;
            decrementButton.interactable = isEnabled;
            incrementButton.interactable = isEnabled;
        }
    }




    // --------------------------------------------------------------------------- On Button Click --------------------------------------------------------------------------
    public void OnButtonClick(int direction)
    {
        Count = Mathf.Min(Mathf.Max(Count + direction, minCount), MaxCount);
    }
}