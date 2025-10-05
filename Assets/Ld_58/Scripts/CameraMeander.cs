using DG.Tweening;
using UnityEngine;

public class CameraMeander : MonoBehaviour
{
    private void Awake()
    {
        G._cameraMeander = this;
    }

    void Update()
    {
        //var mousePositionX = Mathf.Clamp01(Input.mousePosition.x / Screen.width) - 0.5f;
        //var mousePositionY = Mathf.Clamp01(Input.mousePosition.y / Screen.height) - 0.5f;
        //transform.localPosition = new Vector3(mousePositionX, mousePositionY * 0.05f, -10);
    }

    public void Shake(float i, float t)
    {
        Camera.main.DOShakePosition(t, i, 10, 45f);
    }

    public void UIHit()
    {
        G._skillCheckCircle.GetComponent<RectTransform>().DOShakePosition(0.4f, 15f, 10, 45f);
        Shake(0.025f, 0.4f);
    }
}
