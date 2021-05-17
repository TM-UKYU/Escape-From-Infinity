using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MöbiusSystem : MonoBehaviour
{
    private List<Vector3> l_GameObj_Pos = new List<Vector3>();          // 記憶媒体(List型 Pos)
    private List<Quaternion> l_GameObj_Rot = new List<Quaternion>();    // 記憶媒体(List型 Rot)
    private List<Vector3> l_GameObj_Scale = new List<Vector3>();        // 記憶媒体(List型 Scale)

    private bool Repetition;                // 往復フラグ(false:逆進行 true:正規進行)

    private int CountFrame;                 // 収録フレーム総数カウンター

    private int Num_FramesRecorded;         // 収録したフレーム数

    private bool Recood;                    // 収録するフラグ

    private bool Section;                   // 動かせるフラグ

    private Quaternion q_DefRot;            // オブジェクトの回転量

    private Effekseer.EffekseerEmitter EE_effekSeerEmi;   // エフェクト変数(EffekSeer)

    [SerializeField]
    private const float RecordableTime = 10f;   // 収録可能な最大秒数

    public float seconds;                // 収録可能な最大秒数

    public bool Destory;                    // 記憶した動きを破壊するフラグ

    public bool Stop;                       // 動きの停止フラグ

    public GameObject MöbiusSprite;

    public GameObject CatchObject;          // 対象オブジェクト変数

    public ScoreManeger scoreManeger;       // スコアに必要な情報をやり取りする用 ( 使用回数等 )


    // Start is called before the first frame update
    void Start()
    {
        // コンポーネントを取得
        scoreManeger = GameObject.Find("ScoreManeger").GetComponent<ScoreManeger>();
        if (!scoreManeger) { Debug.Log("scoreManeger 取得失敗"); }

        // エフェクシア初期化
        EE_effekSeerEmi = GetComponent<Effekseer.EffekseerEmitter>();

        // メビウスシステム初期化
        möbiusSystem_Reset();
    }

    // メビウスシステムの情報を初期化
    void möbiusSystem_Reset()
    {
        Section    = false;
        Recood     = false;
        Repetition = false;
        Destory    = false;
        Stop       = false;
        seconds    = RecordableTime;
        q_DefRot   = MöbiusSprite.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        // 座標記録開始
        if (Input.GetKeyDown(KeyCode.O) && !Recood)
        {
            if (!CatchObject) { return; }
            
            Recood = true;
            Debug.Log("記録開始");

            // システム使用回数を加算
            if (scoreManeger)
            {
                scoreManeger.AddTryNum();
            }
        }

        // システム起動(記録した座標を移動)
        if (Input.GetKeyDown(KeyCode.P) && !Section)
        {
            if (!CatchObject) { return; }

            if (CatchObject == GameObject.Find("Cube"))
            {
                Debug.Log("IN");
            }

            // 記録中に押しても終了する
            RecodEnd();

            MöbiusSprite.transform.rotation = q_DefRot;

            Section = true;

            Debug.Log("システム起動");
        }

        // システムリセット
        if (Input.GetKeyDown(KeyCode.R) && !Destory && Num_FramesRecorded != 0)
        {
            if (!CatchObject) { return; }

            Destory = true;
            Debug.Log("メビウスシステムリセット");
            return;
        }

        Recoding();
        Moving();
        RecetList();
    }


    // 動き（Transform）を記憶する関数
    private void Recoding()
    {
        if (!CatchObject) { return; }
        if (!Recood) { return; }

        if (MöbiusSprite)
        {
            MöbiusSprite.transform.Rotate(new Vector3(0, 0.2f, 0));
        }

        Vector3 pos = CatchObject.transform.position;
        Quaternion Rot = CatchObject.transform.rotation;
        Vector3 Scale = CatchObject.transform.localScale;

        l_GameObj_Pos.Add(pos);
        l_GameObj_Rot.Add(Rot);
        l_GameObj_Scale.Add(Scale);

        seconds -= Time.deltaTime;

        //Debug.Log("入ってる");
        
        Debug.Log("記録可能時間" + seconds);

        // 記録時間終了
        if (seconds <= 0)
        {
            RecodEnd();
        }
    }

    // 記録終了
    private void RecodEnd()
    {
        Recood = false;
        CountFrame = l_GameObj_Pos.Count - 1;

        Num_FramesRecorded = CountFrame;

        Debug.Log("終わり");
        Debug.Log("CountFrame" + CountFrame);

        ObjectMove(0);
    }

    //記憶させた動きを往復させる関数
    private void Moving()
    {
        if (!CatchObject) { return; }
        if (!Section) { return; }
        if (Stop) { return; }

        // 重力をOFF
        catchObject_useGravity(false);

        if (!Repetition)
        {
            ObjectMove(CountFrame);

            CountFrame--;
            if (CountFrame <= 0)
            {
                CountFrame = 0;
                Repetition = true;
            }
        }
        else
        {
            ObjectMove(CountFrame);

            CountFrame++;

            if (CountFrame >= l_GameObj_Pos.Count)
            {
                CountFrame = l_GameObj_Pos.Count - 1;
                Repetition = false;
            }
        }
    }

    // オブジェクトを保持した座標へ移動
    private void ObjectMove(int Frame)
    {
        EE_effekSeerEmi.transform.position = CatchObject.transform.position;
        if (!EE_effekSeerEmi.exists)
        {
            EE_effekSeerEmi.Play(EE_effekSeerEmi.effectAsset);
        }

        Vector3 pos = CatchObject.transform.position;
        Quaternion rot = CatchObject.transform.rotation;
        Vector3 scale = CatchObject.transform.localScale;

        pos = l_GameObj_Pos[Frame];
        rot = l_GameObj_Rot[Frame];
        scale = l_GameObj_Scale[Frame];

        CatchObject.transform.position = pos;
        CatchObject.transform.rotation = rot;
        CatchObject.transform.localScale = scale;
    }

    //記憶したものを破壊する
    public void RecetList()
    {
        if (!Destory) { return; }
        //if (!CatchObject) { return; }

        Debug.Log("リセット");

        ObjectMove(CountFrame);

        l_GameObj_Pos.Clear();
        l_GameObj_Rot.Clear();
        l_GameObj_Scale.Clear();

        möbiusSystem_Reset();

        // 重力を戻す
        catchObject_useGravity(true);
    }

    // 掴んだオブジェクトに重力を適応するか
    // ※ 持っている間及びシステム使用中は重力を切る(下方向への重力加速度が累積され、離した時に初速度がついてしまうのを防止する為)
    public void catchObject_useGravity(bool useGravity)
    {
        if (!CatchObject) { return; }

        if (CatchObject.GetComponent<Rigidbody>().useGravity != useGravity)
        {
            CatchObject.GetComponent<Rigidbody>().useGravity = useGravity;
        }
    }
}
