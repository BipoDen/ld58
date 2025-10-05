using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class FileObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, ISkillCheckTarget
{
    public CaseNode nodePrefab;

    public Transform linkedFile;

    public Transform messegeTransform;
    [SerializeField] private Transform scaleRoot;

    public bool isUnlocked = false;
    [SerializeField] private List<SkillCheckFragment> skillCheckFragments;

    public UnityEvent OnOpen;

    private void OnEnable()
    {
        scaleRoot.localScale = Vector3.zero;
        scaleRoot.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        G._hoverObject = this;

        if (scaleRoot)
        {
            scaleRoot.DOKill();
            scaleRoot.DOScale(1.2f, .3f);
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!G.IsCanInteract) return;

        if (isUnlocked)
            Show();
        else
        {
            G._skillCheckCircle.StartSkillCheck(skillCheckFragments,this);
        }
        G.audioManager.PlaySFX(G.audioManager.click);
    }

    private void Show()
    {
        nodePrefab.gameObject.SetActive(true);
        nodePrefab.Show(messegeTransform);
        OnOpen?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        G._hoverObject = null;

        if (scaleRoot)
        {
            scaleRoot.DOKill();
            scaleRoot.DOScale(1f, .3f);
        }
    }

    public void OnSkillCheckResult(bool success)
    {
        if(success)
        {
            isUnlocked = true;
            //Show();
        }
    }
}
