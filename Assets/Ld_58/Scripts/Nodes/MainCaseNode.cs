using System;
using System.Collections.Generic;
using UnityEngine;

public class MainCaseNode : MonoBehaviour
{
    public List<MainCaseElement> elements;
    [SerializeField] private RectTransform inputContent;

    public int fieldsCompleted = 0;
    [SerializeField] private int fieldsNeedToComplete;

    public List<string> Features = new();
    [SerializeField] private RectTransform featuresContent;

    private List<DataInput> dataInputs = new();

    private void Start()
    {
        fieldsNeedToComplete = elements.Count;
        foreach (var element in elements)
        {
            var inputField = Instantiate(GameResources.Prefabs.DataInput, inputContent);
            inputField.SetData(element.Key, element.value);
            inputField.OnComplete += AddInputPoint;
            dataInputs.Add(inputField);
        }

        foreach(var feature in Features)
        {
            var featureText = Instantiate(GameResources.Prefabs.FeatureText, featuresContent);
            featureText.text = feature;
        }
        
    }

    public void AddInputPoint()
    {
        fieldsCompleted++;
        var isRight = true;
        foreach (var item in dataInputs)
        {
            if (!item.IsComplete) 
                isRight = false;
        }
        if(isRight)
            OnComplete();
    }

    public void OnComplete()
    {
        G.main.CompleteLevel();
    }
}

[Serializable]
public class MainCaseElement
{
    public string Key;
    public string value;
}

