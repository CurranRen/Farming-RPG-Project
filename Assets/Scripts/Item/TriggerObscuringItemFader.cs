using UnityEngine;

public class TriggerObscuringItemFader : MonoBehaviour
{
    // 当其他碰撞器进入触发区域时调用
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 获取与当前触发器碰撞的游戏对象及其子对象上的所有 ObscuringItemFader 组件，并触发淡出效果
        ObscuringItemFader[] obscuringItemFader = collision.gameObject.GetComponentsInChildren<ObscuringItemFader>();

        // 如果找到至少一个 ObscuringItemFader 组件
        if (obscuringItemFader.Length > 0)
        {
            // 遍历所有找到的 ObscuringItemFader 组件并调用其 FadeOut 方法
            for (int i = 0; i < obscuringItemFader.Length; i++)
            {
                obscuringItemFader[i].FadeOut();
            }
        }
    }

    // 当其他碰撞器退出触发区域时调用
    private void OnTriggerExit2D(Collider2D collision)
    {
        // 获取与当前触发器碰撞的游戏对象及其子对象上的所有 ObscuringItemFader 组件，并触发淡入效果
        ObscuringItemFader[] obscuringItemFader = collision.gameObject.GetComponentsInChildren<ObscuringItemFader>();

        // 如果找到至少一个 ObscuringItemFader 组件
        if (obscuringItemFader.Length > 0)
        {
            // 遍历所有找到的 ObscuringItemFader 组件并调用其 FadeIn 方法
            for (int i = 0; i < obscuringItemFader.Length; i++)
            {
                obscuringItemFader[i].FadeIn();
            }
        }
    }
}
