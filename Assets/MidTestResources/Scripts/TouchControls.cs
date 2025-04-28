using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchControls : MonoBehaviour
{
    private Vector2 startTouchPosition, endTouchPosition;
    [SerializeField] GameObject eyes;
    [SerializeField] GameObject mouth;

    private float minSwipeDistance = 50f; // Minimum distance for a swipe to be registered

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DetectSwipe();
    }

    private void DetectSwipe()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                startTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                endTouchPosition = touch.position;
                HandleSwipe();
            }
        }
    }
    private void HandleSwipe()
    {
        float swipeDistance = (endTouchPosition - startTouchPosition).magnitude;

        if (swipeDistance >= minSwipeDistance)
        {
            Vector2 swipeDirection = endTouchPosition - startTouchPosition;
            float x = swipeDirection.x;
            float y = swipeDirection.y;

            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                // Horizontal swipe
                if (y > 0)
                {
                    mouth.GetComponent<ChangableSprite>().switchNextSprite();
                }
                else
                {
                    mouth.GetComponent<ChangableSprite>().switchPreviousSprite();
                }
            }
            else
            {
                if (x > 0)
                {
                    eyes.GetComponent<ChangableSprite>().switchNextSprite();
                }
                else
                {
                    eyes.GetComponent<ChangableSprite>().switchPreviousSprite();
                }

            }
        }
    }
}
