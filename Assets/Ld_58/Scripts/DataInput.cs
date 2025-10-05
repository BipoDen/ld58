using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DataInput : MonoBehaviour
{
    [SerializeField] private TMP_Text keyField;
    [SerializeField] private TMP_InputField inputField;

    public string Name;
    private string inputData;

    public bool IsComplete;

    public Action OnComplete;

    private void Start()
    {
        inputField.onValueChanged.AddListener(OnInputChanged);
    }

    public void SetData(string key, string value)
    {
        Name = key;
        inputData = value;
        keyField.text = Name + ":";
    }

    public void OnInputChanged(string currentText)
    {
        if(currentText.ToUpper() == inputData.ToUpper())
        {
            IsComplete = true;
            inputField.interactable = false;
            inputField.text = inputData;
            OnComplete?.Invoke();
        }
    }
}
