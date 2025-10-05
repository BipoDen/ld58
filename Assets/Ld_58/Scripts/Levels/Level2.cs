using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Level2 : LevelBase
{
    public ButtonUnlock NordMessage;
    public ButtonUnlock SnowMessage;
    public MessegesCaseNode newNode;

    public FileObject lastFile;

    public TMP_Text Ptext;
    public string newDesc;

    private void Start()
    {
        StartCoroutine(StartCor());
    }

    public void StartNotifWaiting(int corId)
    {
        switch (corId)
        {
            case 0:
                StartCoroutine(FirstNotif());
                Ptext.text = newDesc;
                break;
            case 1:
                StartCoroutine(SecondNotif());
                break;
            case 2:
                StartCoroutine(StartCor());
                break;
        }

    }
    IEnumerator FirstNotif()
    {
        ///audio
        ///

        yield return new WaitForSeconds(25f);
        G.audioManager.PlaySFX(G.audioManager.Notification);
        NordMessage.NewMessage();
        NordMessage.GetComponent<Button>().interactable = true;

        yield return G.main.StartSaying();
        yield return new WaitForSeconds(1f);
        yield return G.main.Say("Looks like someone got a notification.");
        yield return G.main.SmartWait(3f);


        yield return G.main.StopSaying();
        G.main.say_text.text = "";
    }

    IEnumerator SecondNotif()
    {
        yield return new WaitForSeconds(25f);
        G.audioManager.PlaySFX(G.audioManager.Notification);
        SnowMessage.NewMessage();
        SnowMessage.nodePrefab.Hide();
        SnowMessage.nodePrefab = newNode;
        SnowMessage.OnOpen.RemoveAllListeners();
        SnowMessage.OnOpen.AddListener(RevealLastFile);

        yield return G.main.StartSaying();
        yield return new WaitForSeconds(1f);
        yield return G.main.Say("Again…");
        yield return G.main.SmartWait(3f);
        G.main.say_text.text = "";
    }

    IEnumerator StartCor()
    {
        yield return G.main.StartSaying();
        yield return new WaitForSeconds(1f);
        yield return G.main.Say("For this job we give you an assistant."); 
        yield return G.main.SmartWait(3f);
        yield return G.main.Say("If you ever need to look up someone's number, you can confidently turn to them.");
        yield return G.main.SmartWait(3f);
        yield return G.main.Say("To look up a number, press the button in the top‑left corner of the screen and enter the number.");
        yield return G.main.SmartWait(3f);
        G.main.say_text.text = "";
    }

    public void RevealLastFile()
    {
        G.main.RevealFile(lastFile);
    }


}
