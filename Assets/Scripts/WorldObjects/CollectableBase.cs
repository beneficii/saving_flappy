using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBase : MonoBehaviour
{
    public List<CoinData> datas;

    CoinData _data;
    SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        _data = datas[Random.Range(0, datas.Count)];
        _spriteRenderer.sprite = _data.sprite;
    }

    public virtual void Collect()
    {
        Game.current.AddScore(_data.score);
        Game.current.PlayAudio(_data.collectSound);

        gameObject.SetActive(false);
        if(_data.objectToSpawn)
        {
            Instantiate(_data.objectToSpawn, transform.position, Quaternion.identity);
        }
    }
}


[System.Serializable]
public class CoinData
{
    public Sprite sprite;
    public int score;
    public AudioClip collectSound;
    public GameObject objectToSpawn;
}