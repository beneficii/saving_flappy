using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolableCheckOverlap : PoolableBase
{
    Collider2D _collider;
    ContactFilter2D _filter;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _filter = new ContactFilter2D();
        _filter.NoFilter();

    }

    public override void OnPooled()
    {
        base.OnPooled();
        var list = new List<Collider2D>();
        _collider.OverlapCollider(_filter, list);

        foreach (var item in list)
        {
            if(item.CompareTag("Coin"))
            {
                item.gameObject.SetActive(false);
            }
        }
    }
}
