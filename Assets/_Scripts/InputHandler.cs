using UnityEngine;
using UnityEngine.Events;

public class InputHandler : MonoBehaviour
{
    [SerializeField]
    private InputBinding[] inputBindings;
    [Header("Default Swipe Settings")]
    [SerializeField] private float defaultMinSwipeDistance = 50f;
    [SerializeField] private float defaultSwipeAngleThreshold = 30f;
    [SerializeField] private InputType CurrentInputType;
    private Vector2 touchStart;
    private bool isTouching = false;
    private bool isSwipeChecking = false;

    public UnityEvent<PlayerAction> onActionTriggered = new UnityEvent<PlayerAction>();

    private void Awake()
    {
        // Initialize default swipe directions if not set
        InitializeDefaultBindings();
    }

    private void InitializeDefaultBindings()
    {
        if (inputBindings == null || inputBindings.Length == 0)
        {
            inputBindings = new InputBinding[]
            {
                // Example keyboard binding
                new InputBinding 
                { 
                    inputType = InputType.KeyPress,
                    action = PlayerAction.Up,
                    keyCode = KeyCode.UpArrow
                },
                
                // Example button binding
                new InputBinding 
                { 
                    inputType = InputType.ScreenButton,
                    action = PlayerAction.Left,
                    buttonName = "Left"
                },
                
                // Example swipe bindings
                new InputBinding 
                { 
                    inputType = InputType.SwipeDirection,
                    action = PlayerAction.Left,
                    swipeDirection = Vector2.left,
                    swipeSettings = new SwipeData 
                    { 
                        minSwipeDistance = defaultMinSwipeDistance,
                        swipeAngleThreshold = defaultSwipeAngleThreshold
                    }
                },
                new InputBinding 
                { 
                    inputType = InputType.SwipeDirection,
                    action = PlayerAction.Right,
                    swipeDirection = Vector2.right,
                    swipeSettings = new SwipeData 
                    { 
                        minSwipeDistance = defaultMinSwipeDistance,
                        swipeAngleThreshold = defaultSwipeAngleThreshold
                    }
                },
                new InputBinding 
                { 
                    inputType = InputType.SwipeDirection,
                    action = PlayerAction.Up,
                    swipeDirection = Vector2.up,
                    swipeSettings = new SwipeData 
                    { 
                        minSwipeDistance = defaultMinSwipeDistance,
                        swipeAngleThreshold = defaultSwipeAngleThreshold
                    }
                },
                new InputBinding 
                { 
                    inputType = InputType.SwipeDirection,
                    action = PlayerAction.Down,
                    swipeDirection = Vector2.down,
                    swipeSettings = new SwipeData 
                    { 
                        minSwipeDistance = defaultMinSwipeDistance,
                        swipeAngleThreshold = defaultSwipeAngleThreshold
                    }
                }
            };
        }
    }
    
    private void Update()
    {
        switch (CurrentInputType)
        {
            case InputType.KeyPress:
                CheckKeyboardInput();
                break;
            case InputType.ScreenButton:
                // Handle screen button input
                break;
            case InputType.SwipeDirection:
                CheckTouchInput();
                break;
        }
    }

    private void CheckKeyboardInput()
    {
        foreach (var binding in inputBindings)
        {
            if (binding.inputType == InputType.KeyPress && Input.GetKeyDown(binding.keyCode))
            {
                TriggerAction(binding.action);
            }
        }
    }

    private void CheckTouchInput()
    {
        // Handle touch input for mobile
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchStart = touch.position;
                    isTouching = true;
                    isSwipeChecking = true;
                    break;

                case TouchPhase.Moved:
                    break;

                case TouchPhase.Ended:
                    if (isSwipeChecking)
                    {
                        CheckSwipe(touch.position);
                    }
                    isTouching = false;
                    isSwipeChecking = false;
                    break;
            }
        }
    }

    
    private void CheckSwipe(Vector2 currentPosition)
    {
        foreach (var binding in inputBindings)
        {
            if (binding.inputType != InputType.SwipeDirection)
                continue;
    
            float distance = Vector2.Distance(touchStart, currentPosition);
    
            if (distance >= binding.swipeSettings.minSwipeDistance)
            {
                Vector2 direction = (currentPosition - touchStart).normalized;
                float angle = Vector2.Angle(direction, binding.swipeDirection);
    
                if (angle <= binding.swipeSettings.swipeAngleThreshold)
                {
                    TriggerAction(binding.action);
                    isSwipeChecking = false;
                    break;
                }
            }
        }
    }
    // Call this method from UI buttons
    public void OnScreenButtonPress(string buttonName)
    {
        foreach (var binding in inputBindings)
        {
            if (binding.inputType == InputType.ScreenButton && 
                binding.buttonName == buttonName)
            {
                TriggerAction(binding.action);
                break;
            }
        }
    }

    private void TriggerAction(PlayerAction action)
    {
        onActionTriggered.Invoke(action);
        Debug.Log($"Action Triggered: {action}");
    }
}