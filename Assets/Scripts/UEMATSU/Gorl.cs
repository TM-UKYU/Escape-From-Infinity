using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gorl : MonoBehaviour
{
    //クリアパネルを追加する
    public GameObject ClearCanvas;
    //スコアを管理するオブジェクトを追加
    public GameObject ScoreManager;

    private void Start()
    {
        ScoreManager = GameObject.Find("ScoreManeger");
    }

    //プレイヤーが当たり判定に入った時の処理
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            ClearCanvas.SetActive(true);
            ScoreManager.GetComponent<ScoreManeger>().StopTime(true);
            ScoreManager.GetComponent<ScoreManeger>().DisplayScore();
        }
    }
}
