using UnityEngine;

public class TrackManager : MonoBehaviour
{
    // Array to hold all track parts
    GameObject[] trackParts;

    // Speed at which the track parts move toward the camera
    public float speed = 5f;

    // Distance between track parts (fixed value, should match the length of each track part)
    public float trackSpacing = 4.8f; // Example length of each track part

    // Initial Z position for the first track part
    public float initialZPosition = -2f;

    // Variable to store the last track part that was repositioned
    private GameObject lastRepositionedTrack;

    // Start is called before the first frame update
    void Start()
    {
        CollectTrackParts();

        // Initialize track positions based on spacing and initial Z position
        InitTrackPositions();

        // Set the last repositioned track to be the farthest one at the start
        lastRepositionedTrack = trackParts[trackParts.Length - 1];
    }

    // Update is called once per frame
    void Update()
    {
        // Move each track part
        for (int i = 0; i < trackParts.Length; i++)
        {
            MoveTrack(trackParts[i]);

            // If the track part's Z position is less than -2, reposition it
            if (trackParts[i].transform.position.z < -2f)
            {
                RepositionTrack(trackParts[i]);
            }
        }
    }

    // Collect all the child objects as track parts
    // Collect only the direct child objects as track parts
    void CollectTrackParts()
    {
        // Get the number of direct children
        int childCount = transform.childCount;

        // Create an array of GameObjects to hold the track parts
        trackParts = new GameObject[childCount];

        // Loop through each direct child and add them to the trackParts array
        for (int i = 0; i < childCount; i++)
        {
            trackParts[i] = transform.GetChild(i).gameObject;
        }
    }

    // Initialize track part positions
    void InitTrackPositions()
    {
        // Set the Z position of each track part, starting from initialZPosition
        for (int i = 0; i < trackParts.Length; i++)
        {
            float newZPosition = initialZPosition + (i * trackSpacing);
            trackParts[i].transform.position = new Vector3(trackParts[i].transform.position.x, trackParts[i].transform.position.y, newZPosition);

            // Debugging to check positions
            Debug.Log($"TrackPart {trackParts[i].name} initialized at Z: {newZPosition}");
        }
    }

    // Move the track part toward the camera
    void MoveTrack(GameObject trackPart)
    {
        trackPart.transform.Translate(Vector3.back * speed * Time.deltaTime);
    }

    // Reposition the track part to the position of the last repositioned track + spacing
    
    void RepositionTrack(GameObject trackPart)
    {
        // Calculate the new Z position
        float newZPosition = lastRepositionedTrack.transform.position.z + trackSpacing;

        // Reposition this track right after the last repositioned track
        trackPart.transform.position = new Vector3(
            trackPart.transform.position.x,
            trackPart.transform.position.y,
            newZPosition
        );
        
        // put obstacles on the track after reposition (using pools) 

        // Debugging to check repositioning
        Debug.Log($"Repositioned {trackPart.name} to Z: {newZPosition}");

        // Update the last repositioned track to this one
        lastRepositionedTrack = trackPart;
    }
}
