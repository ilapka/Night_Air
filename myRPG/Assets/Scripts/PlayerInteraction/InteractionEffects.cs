using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionEffects : MonoBehaviour
{
    [SerializeField] Light lightEffect;
    Color blue;
    Color red;

    void Start()
    {
        lightEffect.enabled = false;
        blue = new Color(35, 244, 255);
        red = new Color(255, 0, 0);
    }

    public void TurnOnBacklight()
    {
        lightEffect.enabled = true;
        lightEffect.color = blue;
    }

    public void TurnLightToDoubleSpeed()
    {
        lightEffect.color = red;
    }

    public void TurnOffBacklight()
    {
        lightEffect.enabled = false;
    }
}
