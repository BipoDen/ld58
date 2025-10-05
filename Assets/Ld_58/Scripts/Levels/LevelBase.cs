using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelBase : MonoBehaviour
{
    public List<CalledElement> calledElements = new();
    public void CheckEvent(string Id)
    {
        foreach (var element in calledElements)
        {
            if (element.id == Id)
            {
                element.actionToDo.Invoke();
            }
        }
    }
}

[Serializable]
public class CalledElement
{
    public string id;
    public UnityEvent actionToDo;
}
