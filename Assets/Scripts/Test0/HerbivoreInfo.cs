using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HerbivoreInfo : AnimalInfo
{
    public int photosyntesis;
    //»ý¼ºÀÚ
    public HerbivoreInfo(string name, int type, int moveSpeed, int photosyntesis) : base(name, type, moveSpeed)
    {
        this.photosyntesis = photosyntesis;
    }
}
