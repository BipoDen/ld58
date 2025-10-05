using UnityEngine;

public class MoveableFragment : CheckFragment
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private RectTransform pointer;

    private void Update()
    {
        if (!G.IsPaused)
            PausableUpdate();
    }

    protected override void PausableUpdate()
    {
        currentAngle -= _speed * Time.deltaTime;
        currentAngle = (currentAngle % 360f + 360f) % 360f;

        Debug.Log(currentAngle);

        pointer.localEulerAngles = new Vector3(0, 0, -currentAngle);
    }
}
