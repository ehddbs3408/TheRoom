using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MessagePanel : MonoBehaviour
{
    [SerializeField]
    private GameObject postMessageBoxPrefab;
    [SerializeField]
    private GameObject sendMessageBoxPrefab;
    [SerializeField]
    private Transform contentParent;

    [SerializeField]
    private ScrollRect scrollRect;

    private RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }


    public void PostMessageBox(string message)
    {
        GameObject go = Instantiate(postMessageBoxPrefab, contentParent);
        go.GetComponent<MessageBox>().SetText(message);
        go.SetActive(true);
    }
    public void SendMessageBox(string message)
    {
        GameObject go = Instantiate(sendMessageBoxPrefab,contentParent);
        go.GetComponent<MessageBox>().SetText(message);
        go.SetActive(true);
    }

    private void ScrollToBottom()
    {
        Canvas.ForceUpdateCanvases(); // 레이아웃 갱신
        scrollRect.verticalNormalizedPosition = 0f; // 가장 아래로 이동
    }

    public void OnMessagePanel()
    {
        ScrollToBottom();
        rectTransform.DOAnchorPosX(300, 0.6f).SetEase(Ease.OutCubic);
    }

    public void OffMessagePanel()
    {
        rectTransform.DOAnchorPosX(-300, 0.6f).SetEase(Ease.OutCubic);
    }

    void Update()
    {
        
    }
}
