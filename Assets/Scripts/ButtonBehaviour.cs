using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ButtonBehaviour : MonoBehaviour
{
    public Light2D light2d;
    public int hoverIntensity;
    public void HoverButton()
    {
        light2d.intensity = hoverIntensity;
    }
    public void UnhoverButton()
    {
        light2d.intensity = 1;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
