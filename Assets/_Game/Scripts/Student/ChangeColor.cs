using UnityEngine;

// ChangeColor：让一个 Sprite 改变颜色
// 教学概念：GameObject → Component → Property → Script 修改 Property
public class ChangeColor : MonoBehaviour
{
    // 要改颜色的 SpriteRenderer
    public SpriteRenderer targetSprite;

    // 新颜色，在 Inspector 里选择
    public Color newColor = Color.red;

    // 按钮点击后调用这个方法
    public void Change()
    {
        targetSprite.color = newColor;
    }
}
