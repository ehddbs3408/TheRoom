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

    private bool isPanelOn = true;

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
        GameObject go = Instantiate(sendMessageBoxPrefab, contentParent);
        go.GetComponent<MessageBox>().SetText(message);
        go.SetActive(true);
    }

    private void ScrollToBottom()
    {
        Canvas.ForceUpdateCanvases(); // 레이아웃 갱신
        scrollRect.verticalNormalizedPosition = 0f; // 가장 아래로 이동
    }

    public void ToggleMessagePanel()
    {

        if (rectTransform.anchoredPosition.x < 0)
        {
            OnMessagePanel();
        }
        else
        {
            OffMessagePanel();
        }
    }

    public void OnMessagePanel()
    {
        if (isPanelOn) return;
        ScrollToBottom();
        rectTransform.DOAnchorPosX(300, 0.6f).SetEase(Ease.OutCubic).onComplete = () =>
        {
            isPanelOn = true;
        };
    }

    public void OffMessagePanel()
    {
        if (isPanelOn == false) return;
        rectTransform.DOAnchorPosX(-300, 0.6f).SetEase(Ease.OutCubic).onComplete = () =>
        {
            isPanelOn = false;
        };
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            PostMessageBox("안녕 ? 혹시 누구 있나요?");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            SendMessageBox("네, 저는 여기 있어요!");
        }
    }
}
