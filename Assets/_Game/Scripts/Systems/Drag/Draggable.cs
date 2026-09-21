using UnityEngine;

// Draggable：挂载后物体可以被鼠标拖拽
[RequireComponent(typeof(Collider2D))]
public class Draggable : MonoBehaviour
{
    private Camera m_mainCamera;
    private Vector3 m_offset;
    private bool m_isDragging;

    private void Awake()
    {
        m_mainCamera = Camera.main;
    }

    private void OnMouseDown()
    {
        // 记录鼠标与物体中心的偏移，避免拖拽时物体跳到鼠标位置
        m_offset = transform.position - GetMouseWorldPosition();
        m_isDragging = true;
    }

    private void OnMouseDrag()
    {
        if (!m_isDragging) return;

        transform.position = GetMouseWorldPosition() + m_offset;
    }

    private void OnMouseUp()
    {
        if (!m_isDragging) return;

        m_isDragging = false;
        TryDrop();
    }

    // 放下时查找脚下带 Droppable 的物体，并触发它的事件
    private void TryDrop()
    {
        foreach (Collider2D collider in Physics2D.OverlapPointAll(transform.position))
        {
            if (collider.gameObject == gameObject) continue;

            Droppable droppable = collider.GetComponent<Droppable>();
            if (droppable != null)
            {
                droppable.Drop(this);
                return;
            }
        }
    }

    // 鼠标的世界坐标（2D 场景中 z 固定为 0）
    private Vector3 GetMouseWorldPosition()
    {
        Vector3 position = m_mainCamera.ScreenToWorldPoint(Input.mousePosition);
        position.z = 0f;
        return position;
    }
}
