using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class SkillCheckCircle : MonoBehaviour
{
    public RectTransform pointer;       // стрелка ( pivot должен быть центр круга )
    public float speed = 180f;          // скорость вращения в градусах в секунду
    private float currentAngle = 0f;

    public bool running = false;
    private bool isHitting = false;

    [SerializeField] private Image skillCheckArrow;
    [SerializeField] private RectTransform fragmentParent;

    public List<CheckFragment> fragments = new();

    public event Action<bool> OnResult;

    private void Awake()
    {
        G._skillCheckCircle = this;
    }

    void Start()
    {
        
    }

    private void SpawnFragments(List<SkillCheckFragment> fragmentsToSpawn)
    {
        List<float> usedAngles = new();

        for (int i = 0; i < fragmentsToSpawn.Count; i++)
        {
            float angle;
            int attempts = 0;

            // ищем угол, который не близко к другим
            do
            {
                angle = UnityEngine.Random.Range(0f, 360f);
                attempts++;
            }
            while (usedAngles.Exists(a => Mathf.Abs(Mathf.DeltaAngle(a, angle)) < 25) && attempts < 100);

            usedAngles.Add(angle);


            CheckFragment fragment;

            switch(fragmentsToSpawn[i].type)
            {
                case SkillCheckFragment.SkillCheckFragmentType.Static:
                    fragment = Instantiate(GameResources.Prefabs.Fragment, fragmentParent);
                    break;
                case SkillCheckFragment.SkillCheckFragmentType.Moving:
                    fragment = Instantiate(GameResources.Prefabs.MovementFragment, fragmentParent);
                    break;
                default:
                    fragment = Instantiate(GameResources.Prefabs.Fragment, fragmentParent);
                    break;
            }

            fragment.currentAngle = angle;
            fragment.rectTransform.localEulerAngles = new Vector3(0, 0, -angle);

            fragments.Add(fragment);
        }
    }

    public void StartSkillCheck(List<SkillCheckFragment> fragments, ISkillCheckTarget target)
    {
        G.ui.ShowSkillCheck();

        currentAngle = 0f;

        if(PlayerPrefs.GetInt("SkillCheckTut", 0) == 0)
        {
            StartCoroutine(Tutorial());
        }
        else
            running = true;

        SpawnFragments(fragments);

        OnResult = result => target.OnSkillCheckResult(result);
    }

    IEnumerator Tutorial()
    {
        yield return G.main.StartSaying();
        yield return G.main.Say("Before you can access the data, you need to breach the security.");
        yield return G.main.SmartWait(3f);
        yield return G.main.Say("To do that, you must hit the marked areas when the arrow points at them. Press 'Space' to attempt.");
        yield return G.main.SmartWait(3f);
        yield return G.main.StopSaying();
        G.main.say_text.text = "";
        PlayerPrefs.SetInt("SkillCheckTut", 1);
        running = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (running)
        {
            currentAngle += speed * Time.deltaTime;
            currentAngle = (currentAngle % 360f + 360f) % 360f;

            

            pointer.localEulerAngles = new Vector3(0, 0, -currentAngle);

            if (Input.GetKeyDown(KeyCode.Space) && !isHitting)
            {
                StartCoroutine(CheckHit());
            }
        }

        if (isHitting)
        {
            skillCheckArrow.color = new Color(.4f, .4f, .4f, 1);
        }
        else
        {
            skillCheckArrow.color = new Color(1, 1, 1, 1);
        }

        if(fragments.Count == 0 && running && !isHitting)
        {
            running = false;
            Debug.Log("Skillcheck complete");

            Complete();
        }
    }

    private void Complete()
    {
        OnResult?.Invoke(true);
        G.ui.HideSkillCheck();
    }

    IEnumerator CheckHit()
    {
        var (isHit, fragment) = IsHit();
        isHitting = true;
        skillCheckArrow.transform.DOPunchPosition(Vector2.up * 20, 0.2f);
        if (isHit)
        {
            StartCoroutine(PauseHit());
            fragment.HitFragment();
            if(fragment.health <= 0)
            {
                fragments.Remove(fragment);
            }
            Debug.Log("Hit");
        }
        else
        {
            Debug.Log("Miss");
        }
        yield return new WaitForSeconds(0.3f);
        isHitting = false;
    }

    private IEnumerator PauseHit()
    {
        running = false;
        G.IsPaused = true;
        G.audioManager.PlaySFX(G.audioManager.Hit);
        yield return new WaitForSeconds(.2f);
        G.IsPaused = false;
        running = true;
    }

    private (bool, CheckFragment) IsHit()
    {
        foreach(var fragment in fragments)
        {
            if(Mathf.Abs(Mathf.DeltaAngle(currentAngle, fragment.currentAngle)) <= 15f)
            {
                return (true, fragment);
            }
        }
        return (false, null);
    }
}

[Serializable]
public class SkillCheckFragment
{
    public SkillCheckFragmentType type;

    public enum SkillCheckFragmentType
    {
        Static,
        Moving
    }
}

public interface ISkillCheckTarget
{
    void OnSkillCheckResult(bool success);
}
