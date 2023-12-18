using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableObject : MonoBehaviour
{
    public AudioClip audioClip;
    public Vector3 initialPosition;
    [HideInInspector]
    public AudioSource audioSource;
}
