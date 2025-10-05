using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableSmoothDamp : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public MoveableBase moveable;
    public bool isDragging = false;

    private Camera mainCamera;
    private Vector3 offset;

    private FileObject _fileObject;

    private void Start()
    {
        isDragging = false;
        mainCamera = Camera.main;
        moveable.targetPosition = transform.position;

        _fileObject = GetComponent<FileObject>();
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;

        Vector3 screenPosition = mainCamera.WorldToScreenPoint(transform.position);
        offset = transform.position - mainCamera.ScreenToWorldPoint(
            new Vector3(eventData.position.x, eventData.position.y, screenPosition.z));

        G._dragObject = _fileObject;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 cursorPoint = new Vector3(eventData.position.x, eventData.position.y, mainCamera.WorldToScreenPoint(transform.position).z);
        Vector3 cursorPosition = mainCamera.ScreenToWorldPoint(cursorPoint) + offset;
        cursorPosition.z = transform.position.z;

        moveable.targetPosition = cursorPosition;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        G._dragObject = null;
        isDragging = false;
    }
}
