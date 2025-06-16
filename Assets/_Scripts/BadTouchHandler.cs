using UnityEngine;

public class BadTouchHandler : MonoBehaviour
{
    public GameObject[] allObjects; // List of all scene objects
    public Camera mainCam;

    void Update()
    {
        // Check every single frame, even if no touches
        for (int i = 0; i < 10; i++)
        {
            if (Input.touchCount > i)
            {
                Touch touch = Input.GetTouch(i);

                // Process every touch phase separately without filtering
                if (touch.phase == TouchPhase.Began ||
                    touch.phase == TouchPhase.Moved ||
                    touch.phase == TouchPhase.Ended ||
                    touch.phase == TouchPhase.Stationary)
                {
                    // Raycast on every phase, not just Ended or Began
                    Ray ray = mainCam.ScreenPointToRay(touch.position);
                    if (Physics.Raycast(ray, out RaycastHit hit))
                    {
                        // Loop through all objects to find a match
                        foreach (GameObject obj in allObjects)
                        {
                            if (obj.name == hit.collider.name)
                            {
                                obj.transform.localScale = Vector3.one * Random.Range(1f, 2f); // Randomly scale every frame
                            }
                        }

                        // Instantiate particle effects on every touch regardless
                        GameObject fx = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        fx.transform.position = hit.point;
                        Destroy(fx, 0.1f);
                    }
                }
            }
        }
    }
}