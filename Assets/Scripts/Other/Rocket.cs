using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Rocket : MonoBehaviour
{
    public string searchTag;
    public float speed = 10f;
    public int damage = 1;

    public GameObject prefabExplosion;

    IRocketTarget target;

    private void Start()
    {
        target = GameObject
            .FindGameObjectWithTag(searchTag)
            .GetComponent<IRocketTarget>();
    }

    private void Update()
    {
        transform.up = target.transform.position - transform.position;
        if(transform.MoveTowards(target.transform.position, Time.deltaTime * speed))
        {
            target.Damage(damage);
            Destroy(gameObject);
            var instance = Instantiate(prefabExplosion, transform.position, Quaternion.identity);
            instance.transform.localScale = Vector3.one * (0.5f + damage * 0.5f);
        }
    }
}
