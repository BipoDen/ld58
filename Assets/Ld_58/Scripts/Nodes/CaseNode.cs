using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CaseNode : MonoBehaviour
{
    [SerializeField] private GameObject _caseNodePanel;
    [SerializeField] private Button _closeButton;

    public void Awake()
    {
        _closeButton.onClick.AddListener(Hide);
        Hide();
    }

    public void Show(Transform messegeTransform)
    {
        _caseNodePanel.SetActive(true);

        _caseNodePanel.transform.localScale = Vector3.zero;
        _caseNodePanel.transform.DOScale(1, .3f).SetEase(Ease.OutBack);

        _caseNodePanel.GetComponent<MoveableBase>().targetPosition = messegeTransform.position;
        _caseNodePanel.transform.position = messegeTransform.position;
    }

    public void Hide()
    {
        _caseNodePanel.SetActive(false);
    }
}
