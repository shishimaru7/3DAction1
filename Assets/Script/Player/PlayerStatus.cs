using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStatus : CharacterBase
{
    [Header("速度")]
    [SerializeField]
    protected float moveSpeed = 20;
    public float MoveSpeed { set { moveSpeed = value; moveSpeed = Mathf.Clamp(moveSpeed, 0, 20); } get { return moveSpeed; } }

    [Header("最大速度")]
    [SerializeField]
    protected float moveMaxSpeed;
    //キャラがその場で回転する
    [Header("回転速度")]
    [SerializeField]
    private float rotateSpeed;

    [Header("ジャンプ")]
    private bool jump = false;

    //回数ジャンプ
    [Header("ジャンプ回数")]
    private int jumpcount;

    //回数ジャンプ
    [Header("ジャンプできる最大回数")]
    private int jumpcountmax = 2;
    /// /////////////////////////////ラベル表示させたい場合public入れる事（そこにTextを入れる）
    [Header("HPラベル")]
    [SerializeField]
    private Text hpText;
    [Header("攻撃力表示")]
    [SerializeField]
    private Text attackText;
    [Header("防御力表示")]
    [SerializeField]
    private Text defenseText;
    [Header("速度表示")]
    [SerializeField]
    private Text moveText;

    [Header("オーディオクラス取得")]
    private AudioSource audioSource;

    [Header("守備上がる音")]
    public AudioClip auddefense;

    [Header("回復音")]
    public AudioClip audcare;

    [Header("ワープ音")]
    public AudioClip warp;
    [Header("速度低下音")]
    public AudioClip audmovedown;

    [Header("player攻撃音")]
    public AudioClip audattack;

    [Header("リジッド")]
    private Rigidbody rb;
    [Header("敵クラス")]
    private EnemyStatus enemystatus;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        enemystatus = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyStatus>();

        hpText.text = "HP" + Hp + "/" + maxHp;
        attackText.text = "攻撃" + AttackPower + "/" + attackMaxPower;
        defenseText.text = "防御" + DefensepPower + "/" + defenseMaxPower;
        moveText.text = "速度" + MoveSpeed.ToString("0") + "/" + moveMaxSpeed;
    }
    private void Update()
    {
        if (transform.position.y < -10)
        {
            transform.position = new Vector3(-35,2,-35);
        }
        //徐々にスピードが回復していく
        MoveSpeed = MoveSpeed + (Time.deltaTime + 0.001f);
        moveText.text = "速度" + MoveSpeed.ToString("0") + "/" + moveMaxSpeed;
    }

    private void FixedUpdate()
    {
        Move();
        Stop();
        Jump();
    }

    private void Move()
    {
        //移動の設定
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //入力値の正規化
        Vector3 dir = new Vector3(x, 0, z).normalized;
        rb.AddForce(x * moveSpeed, 0, z * moveSpeed);
        LookDirection(dir);
    }

    private void LookDirection(Vector3 dir)
    {
        // ベクトル(向きと大きさ)の2乗の長さをfloatで戻す = Playerが移動しているかどうかの確認
        if (dir.sqrMagnitude <= 0f)
        {
            return;
        }
        // 補間関数 Slerp（始まりの位置, 終わりの位置, 時間）　なめらかに回転する
        Vector3 forward = Vector3.Slerp(transform.forward, dir, rotateSpeed * Time.deltaTime);

        // 引数はVector3　Playerの向きを、自分を中心に変える
        transform.LookAt(transform.position + forward);
    }
    //ボタン離したらすぐその場に止まる
    private void Stop()
    {
        if (Input.GetButtonUp("Horizontal"))
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        if (Input.GetButtonUp("Vertical"))
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    private void Jump()
    {
        //はじめに最大カウントもつけて制限させる
        if (Input.GetKeyDown(KeyCode.Space) && jumpcount < jumpcountmax)
        {
            jump = true;
        }
        if (jump)
        {
            // 速度をクリアして2回目のジャンプも1回目と同じ挙動にする
            rb.velocity = Vector3.zero;

            //ジャンプする
            rb.AddForce(Vector3.up * moveSpeed, ForceMode.Impulse);

            //ジャンプ数カウント
            jumpcount++;

            //ジャンプ許可
            jump = false;
        }
    }


    //接触してダメージ受ける処理
    private void OnCollisionEnter(Collision collision)
    {
          Playerdeath();

        if (collision.gameObject.tag == "Enemy")
        {
            //プレイヤのダメージ数を出すために変数宣言
            int getHitDamage;　　//enemyのAttackの結果はHPにする

            //enemystatusの攻撃メソッドの結果を受ける
            getHitDamage = enemystatus.EnemyAttack(enemystatus.AttackPower, DefensepPower);
            //現在のHPから戻り値の結果を書くこと
            Hp -= getHitDamage;

            //ラベルに現在/最大表示
            hpText.text = "HP" + hp + "/" + maxHp;

            transform.Translate(1, 0, 0);
        }
        if (collision.gameObject.name == "Floor")
        {
            jumpcount = 0;
        }
    }


    private void Playerdeath()
    {
        if (Hp <= 0)
        {
            Destroy(this.gameObject);
            SceneManager.LoadScene("GameOver");
        }
    }

    // 攻撃防御同数ならダメージ０　攻撃2防御1なら１ダメージ　攻撃１防御２なら０ダメージ
    //enemyのstatusに変化  プレイヤ→敵への攻撃処理
    public int Attackmethod(int playerAttackPower, int enemyDefensePower)
    {
        //攻撃音
        audioSource.PlayOneShot(audattack);

        int damage = Mathf.Clamp( playerAttackPower - enemyDefensePower,1,playerAttackPower);

        return damage;
    }
    //アイテム取得の増減元はセッターで制御する
    // 回復メソッド　　playerプラス処理
    //受け取る方にメソッド書く
    public void GetItemHeal(int amount)
    {
        //取ったら音がなる
        audioSource.PlayOneShot(audcare);
        Hp += amount;
        hpText.text = "HP" + Hp + "/" + maxHp;
    }

    //攻撃力増加アイテム習得用のメソッド
    public void GetItemAttackUp(int amount)
    {
        AttackPower += amount;
        attackText.text = "攻撃" + AttackPower + "/" + attackMaxPower;
    }
    //防御力増加アイテム習得用のメソッド
    public void GetItemDefenseUp(int amount)
    {
        //取ったら音なる
        audioSource.PlayOneShot(auddefense);
        DefensepPower += amount;
        //ラベル表示
        defenseText.text = "防御" + DefensepPower + "/" + defenseMaxPower;
    }
    public void GetItemMovedown(float amount)
    {
        //速度低下音
        audioSource.PlayOneShot(audmovedown);
        //速度低下
        MoveSpeed -= amount;
    }

    //ワープアイテムで飛ばされる座標
    private void OnTriggerEnter(Collider other)
    {
        //ワープ音
        audioSource.PlayOneShot(warp);

        if (other.gameObject.CompareTag("Warpzone"))
        {
            transform.position = new Vector3(Random.Range(-40, 40), Random.Range(0, 3), Random.Range(-40, 40));
        }
    }
}