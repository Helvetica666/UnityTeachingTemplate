using UnityEngine;
using UnityEngine.Events;

// Droppable：拖拽物体被放到它上面时触发事件
[RequireComponent(typeof(Collider2D))]
public class Droppable : MonoBehaviour
{
    [Header("事件")]
    [Tooltip("拖拽物体放上来时触发的事件")]
    public UnityEvent onDrop;

    // 由 Draggable 放下时调用
    public void Drop(Draggable draggable)
    {
        onDrop?.Invoke();
    }
}
