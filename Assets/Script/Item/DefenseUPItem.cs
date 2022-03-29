using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseUPItem : ItemBase
{
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
    }
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.gameObject.TryGetComponent(out PlayerStatus playerStatus))
        {
            amount = Random.Range(1, 10);

            playerStatus.GetItemDefenseUp(amount);

        }
    }
}