using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Mask))]
[RequireComponent(typeof(Image))]
public class AutoRollText : MonoBehaviour
{
    public enum RollType
    {
        Pingpong = 0,
        Left = 1
    }

    public RollType rollType = RollType.Pingpong;
    public Text m_text;
    [Header("延迟时间")] public float delay = 5f;
    [Header("移动速度")] public float speed = .1f;
    private float _textWidth;
    private RectTransform _rect;

    public string text
    {
        set
        {
            m_text.text = value;
            CalcTextWidth();
        }
        get { return m_text.text; }
    }

    private void Awake()
    {
        _rect = this.transform as RectTransform;
        if (m_text == null)
        {
            m_text = this.GetComponentInChildren<Text>(true);
        }

        m_text.rectTransform.pivot = new Vector2(0, 0.5f);
        // m_text.rectTransform.anchoredPosition = new Vector2(-_rect.sizeDelta.x / 2, 0);
        CalcTextWidth();
    }

    private void CalcTextWidth()
    {
        // 根据字数 调整 view 宽度
        TextGenerator generator = new TextGenerator();
        TextGenerationSettings settings = m_text.GetGenerationSettings(Vector2.zero);
        _textWidth = generator.GetPreferredWidth(text, settings);
        Vector2 size = m_text.rectTransform.sizeDelta;
        size.x = _textWidth;
        m_text.rectTransform.sizeDelta = size;

        Vector2 pos = new Vector2(-size.x / 2, 0);
        pos.x += (_textWidth - _rect.sizeDelta.x) / 2f;
        Debug.Log($"textWidth:{_textWidth}-----width:{_rect.sizeDelta.x}-----pos:{pos}");
        m_text.rectTransform.anchoredPosition = pos;
        StopCoroutine(nameof(RollCoroutine));
        if (_textWidth > _rect.sizeDelta.x) //文字超出大小
        {
            StartCoroutine(nameof(RollCoroutine));
        }
    }

    private IEnumerator RollCoroutine()
    {
        Vector2 pos = m_text.rectTransform.anchoredPosition;
        Vector2 current = pos;
        float offset = _textWidth - _rect.sizeDelta.x;
        if (delay > 0)
        {
            yield return new WaitForSeconds(delay);
        }

        switch (rollType)
        {
            case RollType.Pingpong:
            {
                bool leftDir = true;
                while (true)
                {
                    if (leftDir)
                    {
                        current.x -= speed;
                    }
                    else
                    {
                        current.x += speed;
                    }

                    m_text.rectTransform.anchoredPosition = current;
                    if (current.x < (pos.x - offset))
                    {
                        leftDir = false;
                    }
                    else if (current.x > (pos.x + offset))
                    {
                        leftDir = true;
                    }

                    yield return null;
                }
            }
                break;
            case RollType.Left:
            {
                while (true)
                {
                    current.x -= speed;
                    m_text.rectTransform.anchoredPosition = current;
                    yield return null;
                    if (current.x < (pos.x - offset))
                    {
                        yield return new WaitForSeconds(delay);
                        current.x = pos.x;
                        m_text.rectTransform.anchoredPosition = current;
                        if (delay > 0)
                        {
                            yield return new WaitForSeconds(delay);
                        }
                    }
                }
            }
                break;
        }
    }
}