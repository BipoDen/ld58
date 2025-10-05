using DG.Tweening;
using System.Collections;
using UnityEngine;

public class CheckFragment : ManagedBehaviour
{
    public float currentAngle;
    public RectTransform rectTransform => GetComponent<RectTransform>();
    public RectTransform fragmentImage;
    [field: SerializeField] public float size { get; private set; } = 12f;

    public int health = 1;

    private void Start()
    {
        fragmentImage.localScale = Vector3.zero;
        fragmentImage.DOScale(Vector3.one, 0.25f).SetEase(Ease.OutBack);
    }

    public void HitFragment()
    {
        G._cameraMeander.UIHit();
        health--;
        if (health<=0)
        {
            StartCoroutine(DestroyCor());
        }        
    }

    IEnumerator DestroyCor()
    {
        fragmentImage.transform.DOScale(Vector3.zero, 0.25f);
        yield return new WaitForSeconds(0.3f);
        Destroy(this.gameObject);
    }
}
