using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public interface IPoolable
{
    public IObjectPool<GameObject> pool { get; set; }
}