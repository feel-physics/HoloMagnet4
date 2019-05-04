using System.Collections;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;
using HoloToolkit.Sharing;
using FeelPhysics.HoloMagnet36;

// グローバル定数
public static class GlobalVar
{
    public const Boolean shouldDebugLog = true;
    public const Boolean shouldDebugLogInScene = true;
}

public class MyHelper : MonoBehaviour
{
    // シーンオブジェクト
    public static void DebugLogInScene(String logObjectName, String message)
    {
        if (GlobalVar.shouldDebugLogInScene)
        {
            TextMesh tm = GameObject.Find(logObjectName).GetComponent<TextMesh>();
            tm.text = message;
        }
    }

    // シーンのリストをenumで作る
    public enum MyScene { Operation, Point, Scene2D, Scene3D }
    public static MyScene scene;

    // シーン名とenumのシーンとを対応させる
    static Dictionary<string, MyScene> sceneDic = new Dictionary<string, MyScene>() {
        {"Operation", MyScene.Operation },
        {"Point",     MyScene.Point },
        {"Scene2D",   MyScene.Scene2D },
        {"Scene3D",   MyScene.Scene3D }
    };

    // 現在のシーンを取得する
    public static MyScene MyGetScene()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        scene = sceneDic[sceneName];
        return scene;
    }

    // enumのシーンで指定したシーンをロードする
    public static void MyLoadScene(MyScene scene)
    {
        SceneManager.LoadScene(sceneDic.FirstOrDefault(x => x.Value == scene).Key);
    }

    public class MyMonoBehaviour : MonoBehaviour
    {
        public void CallStartCoroutine(IEnumerator iEnumerator)
        {
            StartCoroutine(iEnumerator); //ここで実際にMonoBehaviour.StartCoroutine()を呼ぶ
        }
    }

    // AsyncAwaitでできる
    private static IEnumerator DelayMethod(float waitTime, Action action)
    {
        yield return new WaitForSeconds(waitTime);
        action();
    }

    // AsyncAwaitでできる
    public static void MyDelayMethod(MonoBehaviour i_behaviour, float waitTime, Action action)
    {
        i_behaviour.StartCoroutine(DelayMethod(waitTime, action));
    }

    public static void DebugLogEvery10Seconds(string message, ref bool hasLogged)
    {
        if (DateTime.Now.Second % 10 == 0)
        {
            if (!hasLogged)
            {
                Debug.Log(message);
                hasLogged = true;
            }
        }
        else
        {
            hasLogged = false;
        }
    }

    public static void SetTransFormOfObject(GameObject passiveObject, Transform selfTransform, 
        bool ignoreOnSharing, bool hasLogged)
    {
     /*
        public class SetTransformOfSharingStage : MonoBehaviour {

        // 初期設定
        private string passiveObjectName = "SharingStage";
        private GameObject passiveObject = null;
        private bool ignoreOnSharing = false;
        private bool hasLogged = false;

        // Use this for initialization
        void Start()
        {
            passiveObject = GameObject.Find(passiveObjectName);
        }

        // Update is called once per frame
        void Update()
        {
            MyHelper.SetTransFormOfObject(passiveObject, transform, ignoreOnSharing, hasLogged);
        }
    }
    */
        if (passiveObject == null) return;

        // 自身が動かなければ、何もしない
        if (!selfTransform.hasChanged) return;

        // 「Sharing時に無視する かつ Sharing中」のとき、returnする
        if (ignoreOnSharing && SharingStage.Instance.IsConnected) return;

        // 変更を受けるオブジェクトのTransformを変更する
        passiveObject.transform.position = selfTransform.position;
        passiveObject.transform.rotation = selfTransform.rotation;
        MyHelper.DebugLogEvery10Seconds("Setting " + passiveObject.name + " transform.", ref hasLogged);
    }

    /// <summary>
    /// 2つの移動するオブジェクトのあいだに線を引く
    /// </summary>
    public static void DrawDynamicLine(GameObject fromObject, GameObject toObject, GameObject lineRendererContainer)
    {
        LineRenderer lineOld = lineRendererContainer.GetComponent<LineRenderer>();
        DestroyImmediate(lineOld);

        LineRenderer lineNew = lineRendererContainer.AddComponent<LineRenderer>();
        // TODO: 色を変える
        //lineNew.material = new Material(Shader.Find("MixedRealityToolkit/Standard"));

        lineNew.startColor = Color.green;
        lineNew.endColor = Color.green;
        //第一引数には始点、第二引数には終点の色を渡します。

        lineNew.startWidth = 0.001f;
        lineNew.endWidth = 0.001f;

        lineNew.SetPosition(0, fromObject.transform.position + new Vector3(0, -0.1f, 0));
        lineNew.SetPosition(1, toObject.transform.position);
        //line.SetPosition関数の第一引数は配列の要素数(配列は0スタートです！,第二引数は座標です)
    }

    /*
    public static void SetSyncModelIsMagneticForceLines(bool shouldDraw)
    {
        // Spawned Application ManagerのSharingしている変数を変更する
        if (SharingStage.Instance.IsConnected)
        {
            var sam = GameObject.FindWithTag("SpawnedApplicationManager");
            var ssam = (SyncSpawnedApplicationManager)sam.
                GetComponent<DefaultSyncModelAccessor>().SyncModel;
            ssam.isMagneticForceLines.Value = shouldDraw;
        }
    }

    public static void ToggleDrawOrDeleteMagneticForceLines()
    {
        Debug.Log("MyHelper.DrawOrDeleteMagneticForceLines() script is fired");

        // BarMagnetを探す
        string barMagnetTag = "BarMagnet01";
        var barmagnetObject = GameObject.FindWithTag(barMagnetTag);
        if (barmagnetObject == null)
        {
            Debug.Log(barMagnetTag + " is not found");
        }
        else
        {
            // 磁力線を描画しているか否かを取得し、ログ出力する
            var state = barmagnetObject.GetComponent<MagneticForceLinesManager>().state;
            Debug.Log(barMagnetTag + " state: " + state.ToString());

            // 磁力線を描画していれば消す
            if (state == MagneticForceLinesManager.State.Drawing3D)
            {
                Debug.Log(barMagnetTag + " delete magnetic force lines.");
                barmagnetObject.GetComponent<MagneticForceLinesManager>().state =
                    MagneticForceLinesManager.State.NotDrawing;
                Debug.Log(barMagnetTag + " is set as NotDrawing");
                barmagnetObject.GetComponent<MagneticForceLinesManager>().DeleteLines();

                MyHelper.SetSyncModelIsMagneticForceLines(false);
            }
            // 磁力線を描画していなければ描画する
            else
            {
                // Spawned Application ManagerのSharingしている変数を変更する
                Debug.Log(barMagnetTag + " doesn't draw magnetic force lines. Will draw them.");
                barmagnetObject.GetComponent<MagneticForceLinesManager>().state =
                    MagneticForceLinesManager.State.Drawing3D;
                Debug.Log(barMagnetTag + " is set as Drawing3D");
                barmagnetObject.GetComponent<MagneticForceLinesManager>().DrawLines3D();

                MyHelper.SetSyncModelIsMagneticForceLines(true);
            }
        }
    }
    */
}