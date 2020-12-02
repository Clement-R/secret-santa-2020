using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ParticleSystemDespawn : MonoBehaviour
{
    public void OnParticleSystemStopped()
    {
        SimplePool.Despawn(gameObject);
    }
}