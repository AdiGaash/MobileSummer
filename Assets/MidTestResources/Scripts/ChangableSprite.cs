using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangableSprite : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    int currentSprirteIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void switchNextSprite()
    {
        currentSprirteIndex++;
        if (currentSprirteIndex >= sprites.Length)
        {
            currentSprirteIndex = 0;
        }

        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[currentSprirteIndex];
    }


    public void switchPreviousSprite()
    {
        currentSprirteIndex--;
        if (currentSprirteIndex < 0)
        {
            currentSprirteIndex = sprites.Length -1;
        }
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[currentSprirteIndex];
    }
}
