using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 出現ごとに出現が早くなる
public class EnemyGenerator : MonoBehaviour
{
    [Header("敵増殖")]
    [SerializeField]
    protected GameObject[] enemypefab = new GameObject[3];

    //少ないとどんどん出る（最大値と同じ）
    [Header("召喚時間")]
    [SerializeField]
    protected float span;

    //０カウントから始まる（現在値と同じ）
    [Header("チャージ時間")]
    [SerializeField]
    protected float time;

    protected virtual void Update()
    {
        //*で遅くすることも出来る

        time += Time.deltaTime;

        if(time > span)
        {
            //スタートにtime = 0 すると一気に生成されるがif文内なら条件満たしたときからスタートするからゆっくり生成される
            //↓必ずつける無いと一気に生成
        　　　time = 0;
            span  += Time.deltaTime;
    // spanの値マイナスだと一気に生成　設定値高くする ↓加算数出現ごとに変化　値小さくすればspan短くてもOK
            span = span + (Time.deltaTime)-0.1f;
            int random = Random.Range(0,4);
            GameObject appearance = Instantiate(enemypefab[random]);
            //出現場所変化付ける
            appearance.transform.position = new Vector3(Random.Range(-40,40), Random.Range(1,3), Random.Range(-40,40));
        }
        //マイナス暴発防止
        if (span <=0)
        {
            Debug.Log("Gate消滅");
            Destroy(gameObject);
        }
    }
}