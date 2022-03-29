using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : CharacterBase
{
    [Header("追跡距離0なら接近")]
    [SerializeField]
    private float trackingrange;

    [Header("追跡速度")]
    [SerializeField]
    private float tracking;

    [Header("対象")]
    private GameObject player;
    //複数なら[]がいる 自由に設定出来る　　　ヒエラルキーから数の指定可能　１つだけならいらない]
    [Header("アイテムドロップ")]
    [SerializeField]
    private GameObject[] itemdrop;

    [Header("クラス使用")]
    //プレイヤーとのダメージのやり取り可能
   public PlayerStatus playerstatus;

    private void Start()
    {
        playerstatus = GameObject.Find("Player").GetComponent<PlayerStatus>();
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        //Playerに向く
        if (GameObject.Find("Player") == null)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.LookAt(player.transform);
            if (Vector3.Distance(transform.position, player.transform.position) > trackingrange)
            {
                transform.Translate(Vector3.forward * Time.deltaTime * tracking);
            }
        }

        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //戻り値ありのメソッド　ダメージ数を出すために変数宣言
            int getHitDamage;

            //playerlife.Attackmethodの攻撃メソッドの結果を受ける
            getHitDamage = playerstatus.Attackmethod(playerstatus.AttackPower, defensePower);
            //現在のHPから戻り値の結果を書くこと
            Hp -= getHitDamage;

            transform.Translate(3, 0, 0);

            //攻撃のたびにランダムドロップ
            //倒した場所にアイテム出現
            //ドロップ確率
            if (Random.Range(0, 100) < 100)
            {
                    //アイテムの種類
                    //指定可能0.0は0のアイテム　1.1は１のアイテム
                    int itenumber = (Random.Range(0, 4));

                    //ランダムな座標に配置
                    Vector3 pos = transform.position;
                    Instantiate(itemdrop[itenumber], new Vector3(Random.Range(-40,40), pos.y + 1,Random.Range(-40,40)), Quaternion.identity);
            }
        }
        Death();
    }


    private void Death()
    {
        if (Hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    //プレイヤのstatusに変化  敵→Player攻撃
    public int EnemyAttack(int enemyAttackPower, int playerDefensePower)
    {
        int damage = Mathf.Clamp( enemyAttackPower - playerDefensePower,1,enemyAttackPower);

        return damage;
    }
}