using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkySprite : MonoBehaviour
{
    public Sprite sprite1;
    public Sprite sprite2;

    private void Start()
    {
        StartCoroutine(ChangeSpriteEveryInterval(10));
    }

    private IEnumerator ChangeSpriteEveryInterval(float seconds)
    {
        while (true)
        {
            yield return new WaitForSeconds(seconds);
            ChangeSpriteRecursively(transform);
        }
    }

    private void ChangeSpriteRecursively(Transform parent)
    {
        var spriteRenderer = parent.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = spriteRenderer.sprite == sprite1 ? sprite2 : sprite1;
        }

        foreach (Transform child in parent)
        {
            ChangeSpriteRecursively(child);
        }
    }
}