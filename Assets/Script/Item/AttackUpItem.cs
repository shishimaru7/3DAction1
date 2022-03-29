using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackUpItem : ItemBase
{
    [Header("アイテム拾得音")]
    public AudioClip audioClip;

    [Header("クリップ使用のためのクラス")]
    private AudioSource audioSource;

    protected override void Start()
    {
        base.Start();
        //AudioSourceクラス取得
        audioSource = GetComponent<AudioSource>();
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
            amount = Random.Range(1, 5);

            playerStatus.GetItemAttackUp(amount);
            //効果音付ける　※消えるオブジェクトに使える
            AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position);
        }
    }
}