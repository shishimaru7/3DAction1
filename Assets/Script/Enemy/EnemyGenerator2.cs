using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator2 : EnemyGenerator
{
    // 出現ごとに出現が遅くなる
    protected override void Update()
    {
        //*で遅くすることも出来る

        time += Time.deltaTime;

        if (time > span)
        {
            //スタートにtime = 0 すると一気に生成されるがif文内なら条件満たしたときからスタートするからゆっくり生成される
            //↓必ずつける無いと一気に生成
            time = 0;
            span += Time.deltaTime;
            //↓加算数出現ごとに加算
            span = span + (Time.deltaTime) + 0.1f;
            int random = Random.Range(0, 4);
            GameObject appearance = Instantiate(enemypefab[random]);

            //ランダム場所に出現
            appearance.transform.position = new Vector3(Random.Range(-40, 40), 0, Random.Range(-40, 40));
        }
    }
}