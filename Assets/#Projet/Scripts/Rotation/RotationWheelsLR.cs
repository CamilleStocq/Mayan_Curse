using UnityEngine;
using System.Collections;

public class RotateWheel : MonoBehaviour
{
    [SerializeField] Vector3 rotation;
    void Update()
    {
        // transform.localEulerAngles = rotation;
        transform.localRotation = Quaternion.Euler(rotation);
    }
}

