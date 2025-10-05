using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PhoneSearcher : MonoBehaviour
{
    public List<PhoneElement> phoneNumbers = new();
    [SerializeField] private Button researchButton, SubmitButton;
    [SerializeField] private TMP_InputField inputField;

    [SerializeField] private GameObject window;
    private void Awake()
    {
        researchButton.onClick.AddListener(OpenWindow);
        SubmitButton.onClick.AddListener(Submit);
    }

    public void OpenWindow()
    {
        window.SetActive(true);
    }
    public void Submit()
    {
        foreach(var element in phoneNumbers)
        {
            if(element.number == inputField.text)
            {
                ResearchNumber(element);
                return;
            }
        }
        StartCoroutine(FailCor());
    }

    public void ResearchNumber(PhoneElement element)
    {
        StartCoroutine(ResearchCor(element));
    }

    IEnumerator ResearchCor(PhoneElement element)
    {
        yield return G.main.StartSaying();
        yield return new WaitForSeconds(1f);
        foreach (var text in element.textToSpeach)
        {
            yield return G.main.Say(text);
            yield return G.main.SmartWait(3f);
        }
        element.OnComplete?.Invoke();
        yield return G.main.StopSaying();
        G.main.say_text.text = "";
    }

    IEnumerator FailCor()
    {
        yield return G.main.StartSaying();
        yield return new WaitForSeconds(1f);
        yield return G.main.Say("Got nothing better to do? That number’s not even in the database.");
        yield return G.main.SmartWait(3f);
        yield return G.main.StopSaying();
        G.main.say_text.text = "";
    }
}
[Serializable]
public class PhoneElement
{
    public string number;
    public List<string> textToSpeach;
    public UnityEvent OnComplete;
}
