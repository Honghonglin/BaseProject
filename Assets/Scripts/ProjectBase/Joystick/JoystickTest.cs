using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///
///</summary>
public class JoystickTest : MonoBehaviour 
{
    private Vector3 dir;
    private void Start()
    {
        EventCenter.GetInstance().AddEventListener<Vector2>("Joystick", CheckDirChange);
    }
    private void Update()
    {
        this.transform.Translate(dir, Space.World);
    }
    private void CheckDirChange(Vector2 dir)
    {
        this.dir.x = dir.x;
        this.dir.z = dir.y;
    }
}

