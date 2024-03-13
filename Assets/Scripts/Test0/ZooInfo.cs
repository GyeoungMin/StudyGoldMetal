using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZooInfo
{
    public List<AnimalInfo> animalInfos;

    //public ZooInfo(List<AnimalInfo> animalInfos)
    //{
    //    this.animalInfos = animalInfos;
    //}

    public void Init()
    {
        animalInfos = new List<AnimalInfo>();
    }
}
