using System;
using UnityEngine;

public class DestroyablePlayer : Destroyable
{
    public Action<float> OnDecreaseEnergy;

    public void DecreaseEnergy(float value)
    {
        OnDecreaseEnergy(value);
    }
}
