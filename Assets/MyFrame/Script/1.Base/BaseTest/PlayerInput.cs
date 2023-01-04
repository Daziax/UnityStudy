using System;
using UnityEngine;
public class PlayerInput
{
    public float Horizontal { get => Input.GetAxis("Horizontal"); }
    public float Vertical { get => Input.GetAxis("Vertical"); }
    public bool Jump { get => Input.GetButtonDown("Jump"); }
    public bool Attack { get => Input.GetButtonDown("Attack"); }
    public bool Run { get => Input.GetButton("Run"); }
    public bool Move()
    {
        if (Horizontal != 0)
            return true;
        else if (Vertical != 0)
            return true;
        return false;
    }

}