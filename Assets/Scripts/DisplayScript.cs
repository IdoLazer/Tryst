using UnityEngine;
using System.Collections;
/*
This class is used to enable Multi Monitor displays.
Run this script only once (!) at the start of the scene.
*/
public class DisplayScript : MonoBehaviour
{
    Camera[] myCams = new Camera[2];
    public void StartDisplay()
    {
        // === NOTE: MultiMonitor display works only in build, not in Unity editor === 
        Debug.Log("displays connected: " + Display.displays.Length
        + " >>> *Note: MultiMonitor display works only in build.\n"
        + "In unity editor 'displays connected' is always 1, but you can change the display in game view to switch cameras.");

        // Display.displays[0] is the primary, default display and is always ON.
        // Check if additional displays are available and activate each.
        if (Display.displays.Length > 1)
        {
            Display.displays[1].Activate();
        }
        if (Display.displays.Length > 2)
        {
            Display.displays[2].Activate();
        }

        //Get Players Camera
        myCams[0] = GameObject.Find("Camera1").GetComponent<Camera>();
        myCams[1] = GameObject.Find("Camera2").GetComponent<Camera>();

        //Call function when new display is connected
        Display.onDisplaysUpdated += OnDisplaysUpdated;

        //Map each Camera to a Display
        mapCameraToDisplay();
    }
    void mapCameraToDisplay()
    {
        //Loop over Connected Displays
        for (int i = 0; i < Display.displays.Length; i++)
        {
            myCams[i].targetDisplay = i; //Set the Display in which to render the camera to
            Display.displays[i].Activate(); //Enable the display
        }
    }

    void OnDisplaysUpdated()
    {
        Debug.Log("New Display Connected. Show Display Option Menu....");
    }
    void Update()
    {
        // press Esc to quit
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

}
