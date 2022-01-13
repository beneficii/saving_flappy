using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolableWithCoin : PoolableBase
{
    public List<GameObject> coins;

    public override void OnPooled()
    {
        base.OnPooled();

        foreach (var item in coins)
        {
            item.SetActive(true);
        }
    }
}
