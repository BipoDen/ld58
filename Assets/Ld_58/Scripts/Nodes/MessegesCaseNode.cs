using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessegesCaseNode : CaseNode
{
    public MessegesSO MessegesData;

    [SerializeField] private Transform _messegesContainer;

    [SerializeField] private TextMeshProUGUI _title;
 
    private void Awake()
    {
        base.Awake();
        _title.text = MessegesData.DialogName;
        foreach (var messege in MessegesData.Messeges)
        {
            var messegePfb = Instantiate(GameResources.Prefabs.MessegePref, _messegesContainer);
            messegePfb.Setup(messege);
        }
    }
}

[Serializable]
public class Messege
{
    public string Date;
    public string Text; 

    public MessegeType Type;
}

public enum MessegeType
{
    Incoming,
    Outgoing
}
