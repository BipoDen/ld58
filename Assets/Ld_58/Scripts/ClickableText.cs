using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickableText : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        var text = GetComponent<TextMeshProUGUI>();
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            int linkIndex = TMP_TextUtilities.FindIntersectingLink(text, Input.mousePosition, eventData.pressEventCamera);
            if(linkIndex > -1)
            {
                var linkInfo = text.textInfo.linkInfo[linkIndex];
                var linkId = linkInfo.GetLinkID();

                G.audioManager.PlaySFX(G.audioManager.click);
                G.main.CheckEvent(linkId.ToString());
            }
        }

    }

}
