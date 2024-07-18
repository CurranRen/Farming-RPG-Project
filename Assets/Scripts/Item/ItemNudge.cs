using System.Collections;
using UnityEngine;

public class ItemNudge : MonoBehaviour
{
    // 用于暂停的等待时间
    private WaitForSeconds pause;
    // 保证物体与玩家角色触碰过程中只触发一次动画
    private bool isAnimating = false;

    // 初始化等待时间
    private void Awake()
    {
        pause = new WaitForSeconds(0.04f);
    }

    // 当有其他物体进入触发器区域时调用
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isAnimating == false)
        {
            // 如果当前物体位于碰撞物体的左侧，则顺时针旋转，否则逆时针旋转
            if (gameObject.transform.position.x < collision.gameObject.transform.position.x)
            {
                StartCoroutine(RotateAntiClock());
            }
            else
            {
                StartCoroutine(RotateClock());
            }
        }
    }

    // 当其他物体离开触发器区域时调用
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isAnimating == false)
        {
            // 如果当前物体位于碰撞物体的右侧，则顺时针旋转，否则逆时针旋转
            if (gameObject.transform.position.x > collision.gameObject.transform.position.x)
            {
                StartCoroutine(RotateAntiClock());
            }
            else
            {
                StartCoroutine(RotateClock());
            }
        }
    }

    // 协程实现逆时针旋转
    private IEnumerator RotateAntiClock()
    {
        // 标记动画开始
        isAnimating = true;

        // 逆时针旋转4次，每次旋转2度
        for (int i = 0; i < 4; i++)
        {
            gameObject.transform.GetChild(0).Rotate(0f, 0f, 2f);
            yield return pause;
        }

        // 顺时针旋转5次，每次旋转2度
        for (int i = 0; i < 5; i++)
        {
            gameObject.transform.GetChild(0).Rotate(0f, 0f, -2f);
            yield return pause;
        }

        // 逆时针旋转2度复原
        gameObject.transform.GetChild(0).Rotate(0f, 0f, 2f);
        yield return pause;

        // 标记动画结束
        isAnimating = false;
    }

    // 协程实现顺时针旋转
    private IEnumerator RotateClock()
    {
        // 标记动画开始
        isAnimating = true;

        // 顺时针旋转4次，每次旋转2度
        for (int i = 0; i < 4; i++)
        {
            gameObject.transform.GetChild(0).Rotate(0f, 0f, -2f);
            yield return pause;
        }

        // 逆时针旋转5次，每次旋转2度
        for (int i = 0; i < 5; i++)
        {
            gameObject.transform.GetChild(0).Rotate(0f, 0f, 2f);
            yield return pause;
        }

        // 顺时针旋转2度复原
        gameObject.transform.GetChild(0).Rotate(0f, 0f, -2f);
        yield return pause;

        // 标记动画结束
        isAnimating = false;
    }
}
