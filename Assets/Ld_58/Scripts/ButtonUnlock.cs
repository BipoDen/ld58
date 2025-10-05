using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonUnlock : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, ISkillCheckTarget
{
    public CaseNode nodePrefab;

    public bool isUnlocked = false;

    [SerializeField] private Transform scaleRoot;

    [SerializeField] private List<SkillCheckFragment> skillCheckFragments;

    public Transform messegeTransform;

    public UnityEvent OnOpen;

    public GameObject notifIcon;
    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!G.IsCanInteract) return;

        if (isUnlocked)
            Show();
        else
        {
            G._skillCheckCircle.StartSkillCheck(skillCheckFragments, this);
        }
        G.audioManager.PlaySFX(G.audioManager.click);
    }

    public void NewMessage()
    {
        notifIcon.SetActive(true);
    }

    private void Show()
    {
        nodePrefab.gameObject.SetActive(true);
        nodePrefab.Show(messegeTransform);

        OnOpen?.Invoke();

        notifIcon.SetActive(false);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }

    public void OnSkillCheckResult(bool success)
    {
        if (success)
        {
            isUnlocked = true;
            Show();
        }
    }
}
