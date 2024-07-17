using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.OpenXR.Input;
using UnityEngine.InputSystem;

public class UserInterface : MonoBehaviour
{
    private UnityEngine.XR.InputDevice leftHandDevice;
    private bool menuButtonPressed;
    public GameObject PauseMenu;

    void Start()
    {
        leftHandDevice = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
    }
    void Update()
    {
        // Check if the right hand device is valid
        if (leftHandDevice.isValid)
        {
            bool pressed = false;

            // Check if the menu button is pressed
            if (leftHandDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.menuButton, out pressed) && pressed)
            {
                if (!menuButtonPressed)
                {
                    menuButtonPressed = true;
                    OnMenuButtonPressed();
                }
            }
            else
            {
                if (menuButtonPressed)
                {
                    menuButtonPressed = false;
                }
            }
        }
    }

    void OnMenuButtonPressed()
    {
        PauseMenu.SetActive(true);
    }

    public void QuitGame()
    {
        //will only work in a build
        Application.Quit();
    }
}