//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class UnitPool : MonoBehaviour
{
    public ObjectPool<GameObject> pool {get; protected set;}
    [SerializeField]
    GameObject enemyPrefab;
    void OnEnable()
    {
        pool = new ObjectPool<GameObject>(CreatePooledEnemy, OnGetEnemyFromPool, OnReleasedToPool, OnDestroyFromPool);
    }

    GameObject CreatePooledEnemy()
    {
        GameObject obj = Instantiate(enemyPrefab);
        obj.SetActive(false);
        obj.transform.SetParent(transform);
        return obj;
    }

    void OnGetEnemyFromPool(GameObject b)
    {
        b.gameObject.SetActive(true);
    }

    void OnReleasedToPool(GameObject b)
    {
        try { b.gameObject.SetActive(false); }
        catch { Debug.Log ("Enemy already released!");}
    }

    void OnDestroyFromPool(GameObject b)
    {
        try { Destroy(b.gameObject);}
        catch { Debug.Log ("Enemy already destroyed!");}
    }
}