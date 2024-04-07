using System.Collections;
using UnityEngine;

public class NightColor : MonoBehaviour
{
    [SerializeField] private float          m_ChangeColorInterval = 10;
    [SerializeField] private Color          m_NightColor = new Color32(47, 47, 47, 255);
    [SerializeField] private Sprite         m_nightSprite;
    [SerializeField] private Sprite         m_daySprite;
    [SerializeField] private bool           m_IsSky = false;
    [SerializeField] private GameManager    m_GameManager;

    private bool    m_IsNight = false;

    public void ChangeColorRecursively(Transform parent)
    {
        var spriteRenderer = parent.GetComponent<SpriteRenderer>();
        m_IsNight = m_GameManager.IsNight();
        if (spriteRenderer != null)
        {
            if (m_IsSky)
            {
                if (m_IsNight)
                    spriteRenderer.sprite = m_nightSprite;
                else
                    spriteRenderer.sprite = m_daySprite;
            }
            if (m_IsNight)
                spriteRenderer.color = m_NightColor;
            else
                spriteRenderer.color = Color.white;
        }

        foreach (Transform child in parent)
        {
            ChangeColorRecursively(child);
        }
    }
}