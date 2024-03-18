using System.Buffers.Text;
using UnityEngine;
using UnityEngine.Pool;

public class Coin : ItemController, IItem
{
    public void Get()
    {
        FindObjectOfType<UIManager>().AddScore(100);
    }
}
