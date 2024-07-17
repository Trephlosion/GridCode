using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterface : MonoBehaviour
{
    public void QuitGame()
    {
        //will only work in a build
        Application.Quit();
    }
}
