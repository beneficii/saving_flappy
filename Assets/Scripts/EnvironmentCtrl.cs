using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentCtrl : MonoBehaviour
{
    public PoolableObjectSystem obstacles;
    public PoolableObjectSystem backgrounds;
    public PoolableObjectSystem grounds;
    public PoolableObjectSystem coins;

    public void Init(float cameraEdgeX)
    {
        obstacles.Init(cameraEdgeX, 5);
        coins.Init(cameraEdgeX, 2);
        backgrounds.Init(cameraEdgeX);
        grounds.Init(cameraEdgeX);
    }

    public void UpdateCameraPos(float cameraEdgeX)
    {
        obstacles.OnCameraUpdated(cameraEdgeX);
        coins.OnCameraUpdated(cameraEdgeX);
        backgrounds.OnCameraUpdated(cameraEdgeX);
        grounds.OnCameraUpdated(cameraEdgeX);
    }
}

[System.Serializable]
public class PoolableObjectSystem
{
    public List<Transform> objects;
    float _distanceMin;
    float _distanceMax;

    float _nextRandomDistance;
    int _nextIdx = 0;

    int GetIdx(int deltaIdx)
    {
        int r = (_nextIdx + deltaIdx) % objects.Count;
        return r < 0 ? r + objects.Count : r;
    }

    Transform GetObj(int deltaIdx)
    {
        return objects[GetIdx(deltaIdx)];
    }

    public void Init(float cameraEdgeX, float distanceDelta = 0f)
    {
        _distanceMin = GetObj(+1).position.x - GetObj(0).position.x;
        _distanceMax = _distanceMin + distanceDelta;
        UpdateNextRandomDistance();
    }

    void UpdateNextRandomDistance()
    {
        _nextRandomDistance = Random.Range(_distanceMin, _distanceMax);
    }

    void SpawnNext()
    {
        float x = GetObj(-1).position.x + _nextRandomDistance;


        var obj = GetObj(0);
        var pos = obj.position;
        pos.x = x;
        obj.position = pos;

        if(!obj.gameObject.activeSelf)
        {
            obj.gameObject.SetActive(true);
        }
        
        foreach (var item in obj.GetComponents<PoolableBase>())
        {
            item.OnPooled();
        }

        _nextIdx = GetIdx(+1);
        UpdateNextRandomDistance();
    }

    public void OnCameraUpdated(float cameraEdgeX)
    {
        if(GetObj(0).position.x + (_nextRandomDistance / 2f) < cameraEdgeX)
        {
            SpawnNext();
        }
    }
}
