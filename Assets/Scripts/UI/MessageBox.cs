using TMPro;
using UnityEngine;

public class MessageBox : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;

    public void SetText(string message)
    {
        text.SetText(message);
    }
}
