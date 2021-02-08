using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationPointer : MonoBehaviour, IDoubleSpeed
{
    [SerializeField] ParticleSystem particleSystem;
    [SerializeField] Light pointLight;


    public void DoubleSpeeedInteract()
    {
        particleSystem.startColor = Color.red;
        pointLight.color = Color.red;
    }
}
