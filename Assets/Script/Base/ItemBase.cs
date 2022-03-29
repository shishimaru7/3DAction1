using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// アイテムの基底クラス
/// </summary>
public class ItemBase : MonoBehaviour
{
    [Header("増加量")]
    [SerializeField]
    protected int amount;
    protected virtual void Start()
    {
        //時間消滅
        Destroy(gameObject, 20f);

    }
    protected virtual void Update()
    {
        //回転演出
        transform.Rotate(new Vector3(0, 100, 0) * Time.deltaTime);
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}