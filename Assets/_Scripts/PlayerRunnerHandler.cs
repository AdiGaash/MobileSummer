using System.Collections.Generic;
using UnityEngine;
using System;

public enum PlayerAction
{
    Left,
    Right,
    Up,
    Down
}

public enum InputType
{
    KeyPress,
    ScreenButton,
    SwipeDirection
}

[Serializable]
public struct SwipeData
{
    public float minSwipeDistance;
    public float swipeAngleThreshold; // Angle tolerance for swipe detection
}

[Serializable]
public struct InputBinding
{
    public InputType inputType;
    public PlayerAction action;
    
    // Input specific data
    public KeyCode keyCode;        // For keyboard inputs
    public string buttonName;      // For UI buttons
    public Vector2 swipeDirection; // For swipe inputs (normalized)
    public SwipeData swipeSettings;
}



public class PlayerRunnerHandler : MonoBehaviour
{
    private InputHandler inputHandler;

    private void Start()
    {
        inputHandler = GetComponent<InputHandler>();
        inputHandler.onActionTriggered.AddListener(HandleAction);
    }

    private void HandleAction(PlayerAction action)
    {
        
        switch (action)
        {
            case PlayerAction.Up:
                // Handle jump action
                break;
            case PlayerAction.Down:
                // Handle left action
                break;
            case PlayerAction.Right:
                // Handle right action
                break;
            case PlayerAction.Left:
                // Handle right action
                break;
        }
    }
}
