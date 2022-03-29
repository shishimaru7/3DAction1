using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// DeathCountTextにアタッチ
/// </summary>
public class Count : MonoBehaviour
{/// <summary>
/// 時間と残敵数のみ
/// クリアシーン遷移
/// </summary>
    [Header("時間経過")]
    private float time;

    [Header("敵残数テキスト")]
    [SerializeField]
    private Text deathCountText;

    [Header("時間経過テキスト")]
    [SerializeField]
    private Text timerText;


    void Start()
    {   //初期時間0
        time = 0;
    }

    void FixedUpdate()
    {
        //時間表示
        time += Time.deltaTime;
        // ToString("0")は小数点を切り捨て　※123になる
        // 小数点1位まで表示するにはToString("n1")　※0.0 0.1 0.2になる
        // 小数点2位まで表示するにはToString("n2")　※0.01 0.01 0.02になる
        //数値のテキスト表示は必ずToString付ける事
        timerText.text = "Time:" + time.ToString("0") + "秒";

        //敵残数表示される
        int count = GameObject.FindGameObjectsWithTag("Enemy").Length;
        deathCountText.text = "後" + count.ToString() + "体";

        //残数がoになったらクリアシーンへ
        if (count <= 0)
        {
            //（）の中数字でも同じ（3）で登録してあるから（3）でもGameClear
            SceneManager.LoadScene("GameClear");
        }
    }
}