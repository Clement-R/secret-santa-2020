using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public abstract class Trap : MonoBehaviour
{
    protected virtual void Start()
    {
        LevelManager.Instance.RegisterTrap(this);
        TrapReset();
    }

    public abstract void TrapReset();
}