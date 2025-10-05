using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "LD58/SO/MessegesSO")]
public class MessegesSO : ScriptableObject
{
    [Multiline]
    public string DialogName;

    public List<Messege> Messeges = new();
}
