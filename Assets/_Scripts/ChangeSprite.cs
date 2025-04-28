using UnityEngine;
using DG.Tweening;
using System.Collections;

public class SpriteChanger : MonoBehaviour
{
    public SpriteRenderer spriteRenderer1; // The primary sprite renderer
    public float duration = 2.0f; // Duration of the cross-fade

    private SpriteRenderer spriteRenderer2; // The secondary sprite renderer for cross-fade
    private bool isFading = false; // Flag to prevent multiple simultaneous fades

    private void Awake()
    {
        // Create a secondary sprite renderer for cross-fade
        GameObject secondarySpriteObject = new GameObject("SecondarySpriteRenderer");
        secondarySpriteObject.transform.SetParent(transform);
        spriteRenderer2 = secondarySpriteObject.AddComponent<SpriteRenderer>();
        
        // Ensure the secondary sprite renderer is initially invisible
        spriteRenderer2.color = new Color(1, 1, 1, 0);
        spriteRenderer2.sortingOrder = spriteRenderer1.sortingOrder - 1; // Ensure it's behind the primary sprite
    }

    public void ChangeSprite(Sprite newSprite)
    {
        if (isFading) return; // Prevent multiple fades at the same time
        StartCoroutine(CrossFadeToNewSprite(newSprite));
    }

    private IEnumerator CrossFadeToNewSprite(Sprite newSprite)
    {
        isFading = true;

        // Set the secondary sprite renderer to the new sprite
        spriteRenderer2.sprite = newSprite;
        spriteRenderer2.color = new Color(1, 1, 1, 0);

        // Use DOTween to cross-fade the alpha values of the two sprites
        spriteRenderer1.DOFade(0, duration);
        spriteRenderer2.DOFade(1, duration);

        // Wait for the duration of the cross-fade
        yield return new WaitForSeconds(duration);

        // Swap the sprite renderers
        spriteRenderer1.sprite = newSprite;
        spriteRenderer1.color = new Color(1, 1, 1, 1);
        spriteRenderer2.color = new Color(1, 1, 1, 0);

        isFading = false;
    }
}