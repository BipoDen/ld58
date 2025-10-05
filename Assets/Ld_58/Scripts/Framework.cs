using UnityEngine;

public static class G
{
    public static Main main;
    public static UI ui;
    public static AudioManager audioManager;

    public static FileObject _hoverObject;
    public static FileObject _dragObject;

    public static CameraMeander _cameraMeander;
    public static SkillCheckCircle _skillCheckCircle;

    public static bool IsPaused;
    public static bool IsCanInteract = true;
}

public class ManagedBehaviour : MonoBehaviour
{
    void Update()
    {
        if (!G.IsPaused)
            PausableUpdate();
    }

    protected virtual void PausableUpdate()
    {
    }

    void FixedUpdate()
    {
        if (!G.IsPaused)
            PausableFixedUpdate();
    }

    protected virtual void PausableFixedUpdate()
    {
    }
}