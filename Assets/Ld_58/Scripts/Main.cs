using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public TMP_Text say_text;
    public TMP_Text say_text_shadow;

    public Image textBack;

    public LevelBase level;

    public int levelId;
    private void Start()
    {
        G.main = this;

        G.ui.StartGame();
    }

    public void CheckEvent(string Id)
    {
        level.CheckEvent(Id);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            skip = true;
        }
    }

    public IEnumerator StartSaying()
    {
        textBack.DOKill();
        yield return textBack.DOFade(1, 0.25f);
    }

    public IEnumerator StopSaying()
    {
        textBack.DOKill();
        yield return textBack.DOFade(0, 0.25f);
    }

    public IEnumerator Say(string text)
    {
        yield return Print(say_text, text);
    }

    private IEnumerator Print(TMP_Text text, string actionDefinition, string fx = "wave")
    {
        var visibleLength = TextUtils.GetVisibleLength(actionDefinition);
        if (visibleLength == 0)
            yield break;

        for (var i = 0; i < visibleLength; i++)
        {
            text.text = $"<link={fx}>{TextUtils.CutSmart(actionDefinition, 1 + i)}</link>";
            yield return new WaitForSeconds(0.01f);

            //G.audioManager.PlaySFX(G.audioManager.typing);
        }
    }

    bool skip;

    public IEnumerator SmartWait(float f)
    {
        skip = false;
        while (f > 0 && !skip)
        {
            f -= Time.deltaTime;
            yield return null;
        }
        if(skip && f>2.5f)
            yield return new WaitForSeconds(1f);
    }

    private IEnumerator Unprint(TMP_Text text, string actionDefinition)
    {
        var visibleLength = TextUtils.GetVisibleLength(actionDefinition);
        if (visibleLength == 0)
            yield break;

        var str = "";

        for (var i = visibleLength - 1; i >= 0; i--)
        {
            str = TextUtils.CutSmart(actionDefinition, i);
            text.text = $"<link=wave>{str}</link>";
            yield return null;
        }

        text.text = "";
    }

    public IEnumerator Unsay()
    {
        yield return Unprint(say_text, say_text.text);
    }

    public void RevealFile(FileObject file)
    {
        file.gameObject.SetActive(true);
        CreateLine(file.linkedFile, file.transform);
    }

    public void CreateLine(Transform from, Transform to)
    {
        var line = Instantiate(GameResources.Prefabs.LinePrefab).GetComponent<LineRenderer>();
        
        line.SetPosition(0, from.position);
        line.SetPosition(2, to.position);
        float avg = (from.position.y + to.position.y)/2;
        line.SetPosition(1, new Vector3(from.position.x, avg, from.position.z));
    }

    public void CompleteLevel()
    {
        G.ui.Win();
        StartCoroutine(LoadToMainScene());
    }

    IEnumerator LoadToMainScene()
    {
        PlayerPrefs.SetInt("Level"+(levelId+1), 1);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("ScenePicker");
    }
}
