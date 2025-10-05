using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.Events;
using Unity.VisualScripting;

public class Level0 : LevelBase
{
    public Transform mainNode, firstFile;

    private void Start()
    {
        StartDialog(0);
    }

    HashSet<int> _activeDialogs = new HashSet<int>();
    public void StartDialog(int dialogId)
    {
        if (_activeDialogs.Contains(dialogId))
        {
            Debug.Log($"Dialog {dialogId} already running — skipping.");
            return;
        }

        switch (dialogId)
        {
            case 0:
                StartCoroutine(WrapDialog(dialogId, Level0_0()));
                break;
            case 1:
                StartCoroutine(WrapDialog(dialogId, Level0_1()));
                break;
            case 2:
                StartCoroutine(WrapDialog(dialogId, Level0_2()));
                break;
            case 3:
                StartCoroutine(WrapDialog(dialogId, Level0_3()));
                break;
        }
    }

    private IEnumerator WrapDialog(int dialogId, IEnumerator coroutine)
    {
        _activeDialogs.Add(dialogId);
        yield return StartCoroutine(coroutine);
    }

    public IEnumerator Level0_0()
    {
        yield return G.main.StartSaying();
        yield return new WaitForSeconds(1f);
        yield return G.main.Say("As you can see, there isn’t much information to gather, but I believe in you, my friend - this task isn’t hard. Start with the known data.");
        yield return G.main.SmartWait(3f);
        firstFile.gameObject.SetActive(true);
        G.main.CreateLine(mainNode, firstFile);
        yield return G.main.StopSaying();
        G.main.say_text.text = "";
    }

    public IEnumerator Level0_1()
    {
        yield return G.main.StartSaying();
        yield return new WaitForSeconds(.5f);
        yield return G.main.Say("That’s where that rude doctor works. Well then — I think we should collect everything we can about this place.");
        yield return G.main.SmartWait(3f);
        yield return G.main.StopSaying();
        G.main.say_text.text = "";
    }

    public IEnumerator Level0_2()
    {
        yield return G.main.StartSaying();
        yield return new WaitForSeconds(.5f);
        yield return G.main.Say("That’s what we needed. Now let’s get to him.");
        yield return G.main.SmartWait(3f);
        yield return G.main.StopSaying();
        G.main.say_text.text = "";
    }

    public IEnumerator Level0_3()
    {
        yield return G.main.StartSaying();
        yield return new WaitForSeconds(.5f);
        yield return G.main.Say("How about trying to hack his messages? I think we might find something interesting there.");
        yield return G.main.SmartWait(5f);
        yield return G.main.StopSaying();
        G.main.say_text.text = "";
    }
}
