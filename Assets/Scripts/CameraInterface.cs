using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICamera
{
    void CameraMove(GameObject target);
    void CameraPause();
}
