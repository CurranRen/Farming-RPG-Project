using UnityEngine;
using Cinemachine;

public class SwitchConfineBoundingShape : MonoBehaviour
{
    // Start方法在脚本首次运行时被调用
    void Start()
    {
        // 调用SwitchBoundingShape方法
        SwitchBoundingShape();
    }

    /// <summary>
    /// 切换Cinemachine用来定义屏幕边界的碰撞器
    /// </summary>
    private void SwitchBoundingShape()
    {
        // 获取带有'Tags.BoundsConfiner'标签的GameObject上的PolygonCollider2D组件，Cinemachine使用该碰撞器来防止摄像机超出屏幕边界
        PolygonCollider2D polygonCollider2D = GameObject.FindGameObjectWithTag(Tags.BoundsConfiner).GetComponent<PolygonCollider2D>();

        // 获取当前GameObject上的CinemachineConfiner组件
        CinemachineConfiner cinemachineConfiner = GetComponent<CinemachineConfiner>();

        // 将CinemachineConfiner的m_BoundingShape2D属性设置为获取到的PolygonCollider2D
        cinemachineConfiner.m_BoundingShape2D = polygonCollider2D;

        // 因为限制边界已更改，需要清除缓存
        cinemachineConfiner.InvalidatePathCache();
    }
}