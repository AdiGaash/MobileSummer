using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics; // Unity Analytics namespace
using System.Collections.Generic;
using Unity.Services.Analytics;
using Unity.Services.Core;
using Unity.Services.Core.Environments; // Required for Dictionary

public class ButtonAnalyticsTracker : MonoBehaviour
{
    public Button myButton; // Reference to the UI Button
    public string eventName = "button_click"; // Default event name


    
    async void Start() // async: to make the start event function work as async task
                       // and use the "await" task waiter (see below)
    {
        try // use try and catch (exception/error) method 
        {
            // Specify the environment options
            var options = new InitializationOptions();
            options.SetEnvironmentName("production");
            await UnityServices.InitializeAsync(options);
            Debug.Log("Analytics initialized successfully.");
            // AnalyticsService singleton
            AnalyticsService.Instance.StartDataCollection();
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to initialize analytics: {ex.Message}");
        }
    }

    void Awake()
    {
        // Ensure the button is assigned and add a click listener
        if (myButton != null)
        {
            // add custom UnityEvent that will happen onClick on the button
            // - this way you can have more flexibility with parameters of the event function
            myButton.onClick.AddListener(() => NewButtonForItem(name,transform.position));
        }
    }


    void NewButtonForItem(string nameOfGameObject, Vector3 position)
    {
        
    }
    
    
    void OnButtonClick()
    {
        if (UnityServices.State == ServicesInitializationState.Initialized)
        {
            try
            {
                // create the customEvent
                CustomEvent e = new CustomEvent("newcustomevent");
                // optional - add parameters to the event and the parameter value
                e.Add("nameoftheparameter", "value");
                AnalyticsService.Instance.RecordEvent(e);
                AnalyticsService.Instance.Flush();
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to send analytics event: {ex.Message}");
            }
        }
        else
        {
            Debug.Log("error initialized!");
        }
    }
}