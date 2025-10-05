using TMPro;
using UnityEngine;

public class PersonCaseNode : CaseNode
{
    [SerializeField] private TMP_Text discText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    public void SetDesc(string desc)
    {
        discText.text = desc;
    }
}
