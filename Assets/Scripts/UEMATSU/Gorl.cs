using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gorl : MonoBehaviour
{
    //クリアパネルを追加する
    public GameObject ClearCanvas;

    //プレイヤーが当たり判定に入った時の処理
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            ClearCanvas.SetActive(true);
        }
    }
}
