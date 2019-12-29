using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(SpriteRenderer))]
public class Flash : MonoBehaviour
{
    public AnimationCurve m_Curve;
    public float m_duration = 1;

    private SpriteRenderer m_spriteRenderer;
    private Material m_Material;
    // Use this for initialization
    void Start()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_Material = m_spriteRenderer.material;
    }

    IEnumerator EvaluateCureve(UnityAction<float> update)
    {
        float time = 0;
        while (time <= m_duration)
        {
            float value = m_Curve.Evaluate(time / m_duration);
            time += Time.deltaTime;
            update.Invoke(value);
            yield return null;
        }
    }
    public void OnMouseDown()
    {
        Debug.Log("123");
        StartCoroutine(EvaluateCureve((value) => m_Material.SetFloat("_FlashAmount", value)));
    }
}
