using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Game/Dialogue Data")]
public class DialogueData : ScriptableObject
{
    [Header("Speaker")]
    public string speakerName = "Speaker";

    [Header("Dialogue Lines")]
    [TextArea(3, 8)]
    public string[] lines = { "Hello!" };
}
