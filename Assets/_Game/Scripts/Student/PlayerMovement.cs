using UnityEngine;

// PlayerMovement：控制角色移动
// 这是给零基础学生看的第一个脚本，请保持简单易懂
public class PlayerMovement : MonoBehaviour
{
    // moveSpeed 是角色移动速度，可以在 Inspector 面板修改
    // 试试把 5 改成 10，运行后角色会明显变快
    public float moveSpeed = 5f;

    private Rigidbody2D m_rigidbody2D;
    private Vector2 m_moveDirection;

    private void Awake()
    {
        // 提前拿到身上的 Rigidbody2D 组件，存进变量方便后面使用
        m_rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Update 每帧检查输入：按方向键时 GetAxisRaw 返回 1 或 -1，不按返回 0
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // 把按键方向记下来，FixedUpdate 里用它移动
        m_moveDirection = new Vector2(horizontal, vertical);
    }

    private void FixedUpdate()
    {
        // FixedUpdate 用于 Rigidbody 移动（固定时间间隔，适合物理）
        // 新位置 = 当前位置 + 方向 × 速度 × 时间，MovePosition 会带角色走过去
        Vector2 newPosition = m_rigidbody2D.position + m_moveDirection * moveSpeed * Time.fixedDeltaTime;
        m_rigidbody2D.MovePosition(newPosition);
    }
}
