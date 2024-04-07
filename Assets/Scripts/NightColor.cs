using System.Collections;
using UnityEngine;

public class NightColor : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(ChangeColorEveryInterval(10));
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
            if (spriteRenderer.color == new Color32(255, 255, 255, 255))
            {
                spriteRenderer.color = new Color32(47, 47, 47, 255);
            }
            else if (spriteRenderer.color == new Color32(47, 47, 47, 255))
            {
                spriteRenderer.color = new Color32(255, 255, 255, 255);
            }
        }

        foreach (Transform child in parent)
        {
            ChangeColorRecursively(child);
        }
    }
}