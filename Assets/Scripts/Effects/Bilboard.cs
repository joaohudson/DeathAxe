using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bilboard : MonoBehaviour
{
    private void LateUpdate()
    {
        var camPos = CameraController.Instance.transform.position;
        var camDir = camPos - transform.position;
        camDir.Normalize();

        transform.forward = camDir;
    }
}
