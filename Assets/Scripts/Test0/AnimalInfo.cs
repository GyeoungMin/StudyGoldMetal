using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalInfo : ZooInfo
{
    enum AnimalType { Herbivore, Carnivore }

    public string name;
    public int type;
    public int moveSpeed;
    //public int damage;
    //public int photosyntesis;

    //»ý¼ºÀÚ
    public AnimalInfo(string name, int type, int moveSpeed)
    {
        this.name = name;
        this.type = type;
        this.moveSpeed = moveSpeed;
    }

}
