using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManeger : MonoBehaviour
{
    //かかった時間を表示するテキスト
    public Text TimeText;
    private float time = 0;
    //メビウスシステムを使った回数を表示するテキスト
    public Text TryText = null;
    private int TryNum = 0;

    //時間のカウントを止めるための変数
    private bool FreezTime = false;

    // Start is called before the first frame update
    void Start()
    {
        //時間と挑戦回数のリセット
        time = 0;
        TryNum = 0;
        FreezTime = false;
    }

    // Update is called once per frame
    void Update()
    {
        //時間を進めないときはここで帰る
        if (FreezTime) { return; }

        //時間を進める
        time += Time.deltaTime;
    }

    public void StopTime(bool isStop)
    {
        //時間のカウントを止める
        FreezTime = isStop;
    }

    public void AddTryNum()
    {
        //挑戦回数を一回増やす
        TryNum += 1;
    }

    public void DisplayScore()
    {
        //時間と挑戦の回数をUIのテキストに表示する
        TimeText.text = time.ToString("N0")+"Count";
        TryText.text = TryNum.ToString("N0")+"Num";
    }
}
