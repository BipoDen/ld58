using DG.Tweening;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public bool isLocked = true;

    public int LevelId;

    [SerializeField] private Transform scaleRoot;

    private void Start()
    {
        if(PlayerPrefs.GetInt("Level"+LevelId) == 1)
            isLocked = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isLocked) return;
        scaleRoot.DOKill();
        scaleRoot.DOPunchRotation(Vector3.forward * (5 / 2), .15f, 20, 1);
        scaleRoot.DOScale(1.15f, .15f).SetEase(Ease.OutBack);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isLocked) return;
        scaleRoot.DOScale(1f, .15f).SetEase(Ease.OutBack);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isLocked) return;
        StartCoroutine(LoadScene());
    }

    public IEnumerator LoadScene()
    {
        G.ui.Win();
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("CinematicScene"); 
        PlayerPrefs.SetInt("CurrentLevel", LevelId);
    }

}
