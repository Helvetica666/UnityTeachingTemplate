using UnityEngine;

// EndingDataSO：结局数据（名称、标题、描述、可选图片）
[CreateAssetMenu(fileName = "EndingData", menuName = "Game Data/Ending", order = 0)]
public class EndingDataSO : ScriptableObject
{
    [Header("Ending Info")]
    public string endingName = "Ending";
    public string endingTitle = "The End";
    [TextArea(2, 5)]
    public string endingDescription = "Description";
    public Sprite endingSprite; // 可选：结局图片，留空则不显示
}
