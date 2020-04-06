using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformCamera : MonoBehaviour
{
    public float standing;
    public float crouching;

    public void Crouch()
    {
        Vector3 originalPos = gameObject.transform.position;
        gameObject.transform.position = new Vector3(originalPos.x, crouching, originalPos.z);
    }

    public void Stand()
    {
        Vector3 originalPos = gameObject.transform.position;
        gameObject.transform.position = new Vector3(originalPos.x, standing, originalPos.z);
    }
}
