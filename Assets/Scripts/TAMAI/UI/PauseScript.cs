using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{
    [HideInInspector]
    public static bool isPouse;          // 停止中か

    // 外部クラスで使用
    public static bool changePouse;      // 停止中かを切り替える 
    public static bool changeEndUI;      // 終了確認画面を表示するか切り替える ()
    public static bool changeTutorialUI; // チュートリアル画面(基本操作) を表示するか切り替える (外部クラスで使用)

    // 表示するUI
    [SerializeField]
    private GameObject pauseUIPrefab;       // 停止中に表示するUIオブジェクト
    [SerializeField]
    private GameObject endConfirmationUI;   // 本当に終了して良いか確認する為のUIオブジェクト
    
    public GameObject pauseUIInstance;             // PauseUIのインスタンス

    private GameObject endConfirmationUIInstance;   // EndUIのインスタンス

    // 内部処理で使用
    [SerializeField]
    private GameObject[] stopObjects;       // 停止するオブジェクト ※ timeScale で止められないもの

    [SerializeField]
    //private Button[] interactableButtons;   // 一時的に操作が出来ないようにするボタン

    public void SetActive_EndUIInstance( bool flg )
    {
        endConfirmationUIInstance.SetActive(flg);
    }

    void Start()
    {
        isPouse     = false;
        changePouse = false;
        changeEndUI = false;

        //pauseUIInstance = GameObject.Instantiate(pauseUIPrefab) as GameObject;
        endConfirmationUIInstance = GameObject.Instantiate(endConfirmationUI) as GameObject;

        pauseUIInstance.SetActive(false);
        endConfirmationUIInstance.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //new MonoBehaviour().Update();

        if (Input.GetKeyDown(KeyCode.Escape) || changePouse)
        {
            isPouse = !isPouse;

            // ポーズ画面を表示する？
            if (isPouse)
            {
                // ポーズ画面に移動
                PauseMenu();
            }
            else
            {
                // ゲーム画面に移動
                GameMenu();
            }
        }
        
        if (isPouse)
        {
            // 切り替わった時に1度だけ実行
            if (changeEndUI != endConfirmationUIInstance.activeSelf)
            {
                Debug.Log("EndUi 切替：" + changeEndUI);

                //change_InteractableButtons(!changeEndUI);

                //foreach (var ib in interactableButtons)
                //{
                //    ib.interactable = !changeEndUI;
                //}

                endConfirmationUIInstance.SetActive(changeEndUI);
            }

            //if (changeTutorialUI != tutorial.TutorialCanvas.activeSelf)
            //{
            //    Debug.Log("基本操作画面 切替：" + changeEndUI);
            //    change_InteractableButtons(!changeTutorialUI);
            //}
        }
    }

    //// ボタンが有効かを切り替える
    //void change_InteractableButtons(bool changeFlg)
    //{
    //    foreach (var ib in interactableButtons)
    //    {
    //        ib.interactable = changeFlg;
    //    }
    //}

    // ゲーム画面に移動
    protected void GameMenu()
    {
        if (!pauseUIInstance.activeSelf) { return; }

        // Pause画面を非表示
        pauseUIInstance.SetActive(false);

        // 内部時間を稼働        
        Time.timeScale = 1.0f;

        // アタッチされている情報を更新する
        AttachUpdate(true);

        // カーソルを画面中央に固定
        Cursor.lockState = CursorLockMode.Locked;

        /// マウスカーソルを非表示に
        Cursor.visible = false;

        changePouse = false;
    }

    // ポーズ画面に移動
    protected void PauseMenu()
    {
        if (pauseUIInstance.activeSelf) { return; }

        // Pause画面を表示
        pauseUIInstance.SetActive(true);

        // 内部時間を停止
        Time.timeScale = 0.0f;

        // アタッチされている情報を更新しない
        AttachUpdate(false);

        // マウスカーソルのを自由に動かせる
        Cursor.lockState = CursorLockMode.None;

        // マウスカーソルを表示
        Cursor.visible = true;
    }

    // stopObjects に 指定したオブジェクトにアタッチされている情報を更新する？
    // ※ timeScale にて止まらないもの用
    public void AttachUpdate(bool updateFlg)
    {
        foreach (var stopObj in stopObjects)
        {
            MonoBehaviour[] monoBehaviours = stopObj.GetComponents<MonoBehaviour>();
            foreach (var monoBehaviour in monoBehaviours)
            {
                // ポストプロセス以外を変更
                if (!monoBehaviour.GetType().Name.StartsWith("PostProcess"))
                {
                    monoBehaviour.enabled = updateFlg;
                }
            }
        }
    }
}