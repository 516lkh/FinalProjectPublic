using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExp
{
    public int[] RequiredEXP = new int[20];

    public void Initialize()
    {
        for(int i = 0; i < RequiredEXP.Length; i++)
        {
            RequiredEXP[i] = 300 * (i + 1);
        }
    }
}
