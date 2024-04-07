using System.Collections;
using UnityEngine;

public class NightColor : MonoBehaviour
{
    [SerializeField] private float  m_ChangeColorInterval = 10;
    [SerializeField] private Color  m_NightColor = new Color32(47, 47, 47, 255);
    [SerializeField] private Sprite m_nightSprite;
    [SerializeField] private bool   m_IsSky = false;

    private Sprite m_daySprite;

    private void Start()
    {
        if (m_IsSky)
            m_daySprite = GetComponent<SpriteRenderer>().sprite;
        StartCoroutine(ChangeColorEveryInterval(m_ChangeColorInterval));
    }

    private IEnumerator ChangeColorEveryInterval(float seconds)
    {
        while (true)
        {
            yield return new WaitForSeconds(seconds);
            ChangeColorRecursively(transform);
        }
    }

    private void ChangeColorRecursively(Transform parent)
    {
        var spriteRenderer = parent.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            if (m_IsSky)
            {
                if (spriteRenderer.sprite == m_daySprite)
                    spriteRenderer.sprite = m_nightSprite;
                else if (spriteRenderer.sprite == m_nightSprite)
                    spriteRenderer.sprite = m_daySprite;
            }
            if (spriteRenderer.color == Color.white)
                spriteRenderer.color = m_NightColor;
            else if (spriteRenderer.color == m_NightColor)
                spriteRenderer.color = Color.white;
        }

        foreach (Transform child in parent)
        {
            ChangeColorRecursively(child);
        }
    }
}