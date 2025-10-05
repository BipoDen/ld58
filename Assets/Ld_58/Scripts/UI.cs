using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private Image skillCheckPanel;
    [SerializeField] private GameObject skillCheckCircle;

    [SerializeField] private Image fadePanel;

    private void Awake()
    {
        G.ui = this;
    }
    public void ShowSkillCheck()
    {
        skillCheckPanel.gameObject.SetActive(true);
        skillCheckCircle.SetActive(true);
        skillCheckPanel.DOFade(1, .25f);
    }

    public void HideSkillCheck()
    {
        skillCheckCircle.SetActive(false);
        skillCheckPanel.DOFade(0, .25f).OnComplete(() =>
        {
            skillCheckPanel.gameObject.SetActive(false);

        });
    }

    public void StartGame()
    {
        fadePanel.gameObject.SetActive(true);
        fadePanel.DOFade(0, 1.5f);
    }

    public void Win()
    {
        fadePanel.DOFade(1, 1.5f);
    }

    public void FastWin()
    {
        fadePanel.DOFade(1, .5f);
    }

    public Image finalImage;
    public void FinalImage()
    {
        finalImage.DOFade(1, 20).SetEase(Ease.Linear);
    }
}
