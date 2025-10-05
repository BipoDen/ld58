using TMPro;
using UnityEngine;

public class MessegePfb : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _dateText;
    [SerializeField] private TextMeshProUGUI _messegeText;

    public void Setup(Messege messege)
    {
        _dateText.text = messege.Date;
        _messegeText.text = messege.Text;

        if (messege.Type == MessegeType.Incoming)
        {
            _dateText.alignment = TextAlignmentOptions.Left;
            _messegeText.alignment = TextAlignmentOptions.Left;
        }
        else
        {
            _dateText.alignment = TextAlignmentOptions.Right;
            _messegeText.alignment = TextAlignmentOptions.Right;
        }
    }
}
