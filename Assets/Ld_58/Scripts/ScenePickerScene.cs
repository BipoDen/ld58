using UnityEngine;

public class ScenePickerScene : MonoBehaviour
{
    void Awake()
    {
        PlayerPrefs.SetInt("Level"+0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
