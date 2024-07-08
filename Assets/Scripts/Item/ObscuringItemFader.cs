using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ObscuringItemFader : MonoBehaviour
{
    // 用于保存SpriteRenderer组件的引用
    private SpriteRenderer spriteRenderer;

    // 在脚本初始化时获取SpriteRenderer组件
    private void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // 开始淡出效果
    public void FadeOut()
    {
        StartCoroutine(FadeOutRoutine());
    }

    // 开始淡入效果
    public void FadeIn()
    {
        StartCoroutine(FadeInRoutine());
    }

    // 实现淡入效果的协程
    private IEnumerator FadeInRoutine()
    {
        // 获取当前的透明度
        float currentAlpha = spriteRenderer.color.a;
        // 计算从当前透明度到完全不透明的距离
        float distance = 1f - currentAlpha;

        // 当透明度距离完全不透明还有一定范围时，不断增加透明度
        while (1f - currentAlpha > 0.01f)
        {
            currentAlpha = currentAlpha + distance / Settings.fadeInSeconds * Time.deltaTime;
            spriteRenderer.color = new Color(1f, 1f, 1f, currentAlpha);
            yield return null;
        }
        // 确保最终透明度为完全不透明
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
    }

    // 实现淡出效果的协程
    private IEnumerator FadeOutRoutine()
    {
        // 获取当前的透明度
        float currentAlpha = spriteRenderer.color.a;
        // 计算从当前透明度到目标透明度的距离
        float distance = currentAlpha - Settings.targetAlpha;

        // 当透明度距离目标透明度还有一定范围时，不断减少透明度
        while (currentAlpha - Settings.targetAlpha > 0.01f)
        {
            currentAlpha = currentAlpha - distance / Settings.fadeOutSeconds * Time.deltaTime;
            spriteRenderer.color = new Color(1f, 1f, 1f, currentAlpha);
            yield return null;
        }
        // 确保最终透明度为目标透明度
        spriteRenderer.color = new Color(1f, 1f, 1f, Settings.targetAlpha);
    }
}
