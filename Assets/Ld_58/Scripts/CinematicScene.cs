using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CinematicScene : MonoBehaviour
{
    private int currentLevel;
    void Start()
    {
        G.ui.StartGame();
        currentLevel = PlayerPrefs.GetInt("CurrentLevel");
        ChooseCor(currentLevel); 
    }

    public void ChooseCor(int levelId)
    {
        switch (levelId)
        {
            case 0:
                StartCoroutine(FirstDialog());
                break;
            case 1:
                StartCoroutine(SecondDialog());
                break;
            case 2: 
                StartCoroutine(ThirdDialog());
                break;
            case 3:
                StartCoroutine(FourthDialog());
                break;
        }
    }



    IEnumerator FirstDialog()
    {
        yield return G.main.StartSaying();
        yield return new WaitForSeconds(1f);
        yield return G.main.Say("Hey.");
        yield return G.main.SmartWait(3f);
        yield return G.main.Say("Keep it short and to the point. My name is none of your business. Who I am is none of your concern either.");
        yield return G.main.SmartWait(3f);
        yield return G.main.Say("I give you a task — you do it. I know you're an expert at this; I've heard about you. I won't forget this.");
        yield return G.main.SmartWait(3f);
        yield return G.main.Say("Here's the objective. Use your computer skills to gather as much information as possible.");
        yield return G.main.SmartWait(3f);
        yield return G.main.Say("As you can see, I know almost nothing... The person I'm looking for is a doctor who was rude to a patient in a hospital.");
        yield return G.main.SmartWait(3f);
        yield return G.main.Say("That's it. The rest is your job.");
        yield return G.main.SmartWait(3f);
        yield return G.main.StopSaying();

        G.ui.Win();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Level0");
    }

    IEnumerator SecondDialog()
    {
        yield return G.main.StartSaying();
        yield return new WaitForSeconds(1f);
        yield return G.main.Say("Excellent work. It turns out this is quite easy for you…");
        yield return G.main.SmartWait(3f);
        yield return G.main.Say("But don’t think it’s going to stay easy. I have no doubts about you, but take this seriously.");
        yield return G.main.SmartWait(3f);
        yield return G.main.Say("Your next target is a woman who likes to hang out at the 'Broken Compass' bar.");
        yield return G.main.SmartWait(3f);
        yield return G.main.StopSaying();

        G.ui.Win();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Level1");

    }

    IEnumerator ThirdDialog()
    {
        yield return G.main.StartSaying();
        yield return new WaitForSeconds(1f);
        yield return G.main.Say("Still, the rumors about you aren't false. You're doing a good job.");
        yield return G.main.SmartWait(3f);
        yield return G.main.Say("Let's not waste time, because the next task will take up all of your time.");
        yield return G.main.SmartWait(3f);
        yield return G.main.Say("Your next target is a journalist at a media magazine. Note that he writes under a pseudonym — we need his real details.");
        yield return G.main.SmartWait(3f);
        yield return G.main.StopSaying();

        G.ui.Win();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Level2");
    }

    IEnumerator FourthDialog()
    {
        yield return G.main.StartSaying();
        yield return new WaitForSeconds(1f);
        yield return G.main.Say("...");
        yield return G.main.SmartWait(3f);
        yield return G.main.Say("Excellent.");
        yield return G.main.SmartWait(3f);
        yield return G.main.Say("...");
        yield return G.main.SmartWait(3f);
        yield return G.main.Say("Your services have been a great help. You handle things quickly.");
        yield return G.main.SmartWait(3f);
        yield return G.main.Say("I wouldn’t want to lose such a talented colleague.");
        yield return G.main.SmartWait(3f);
        yield return G.main.Say("Unfortunately, I can’t brief you on the new matter.");
        yield return G.main.SmartWait(3f);
        yield return G.main.Say("...");
        yield return G.main.SmartWait(3f);
        yield return G.main.Say("You probably won’t be able to help us with it...");
        yield return G.main.SmartWait(3f);
        yield return G.main.Say("Because our next target...");
        yield return G.main.SmartWait(3f);
        yield return G.main.Say("...is you.");
        G.ui.FastWin();
        G.audioManager.Mute();
        yield return G.main.StopSaying();
        yield return new WaitForSeconds(4f);
        yield return G.main.Say("But...");
        yield return G.main.SmartWait(3f);
        G.ui.FinalImage();
        yield return G.main.Say("It wasn’t a bad ending.");
        yield return G.main.SmartWait(5f);

    }

}
