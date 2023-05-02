using UnityEngine;

public class DialogueBox : MonoBehaviour
{
    [SerializeField] private ListGroups listGroups;

    public static DialogueBox Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        listGroups.OnListItemSelected += ListGroups_OnListItemSelected;
        //listGroups.Add("Trumpy", new List<string>() { "Item 1", "Item 2", "Item 3" });
    }

    private void ListGroups_OnListItemSelected(object sender, System.EventArgs e)
    {
        Debug.Log("Hello");
    }


    public void Open()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }
}