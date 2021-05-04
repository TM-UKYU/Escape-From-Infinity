using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearPanel : MonoBehaviour
{
    //パネルを登録する
    public GameObject Panel;


    // Start is called before the first frame update
    void Start()
    {
        //パネルを消す
        this.gameObject.SetActive(false);
    }

    void Update()
    {
        //ゴールのパネルが出現している最中にEnterを押すとタイトルに戻る処理
        if(Input.GetKey(KeyCode.Return))
        {
            SceneManager.LoadScene("NewTitle");
        }
    }

    void OnEnter()
    {
        //パネルを表示
        this.gameObject.SetActive(true);
    }
}
