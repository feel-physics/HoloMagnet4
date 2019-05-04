using HoloToolkit.Sharing;
using System;
using UnityEngine;
using FeelPhysics.HoloMagnet36;

public class MagneticForceLinesDrawer : MonoBehaviour {

    //状態
    public enum State { NotDrawing, Drawing2D, Drawing3D };
    public State state;

    LineRenderer lineRenderer;
    public GameObject magneticForceLineForOnOff;

    private GameObject[] southPoles;
    private GameObject[] northPoles;

    private GameObject myMagnet;

    // Use this for initialization
    private void Start()
    {
        // 磁力線を消しておく
        //state = State.NotDrawing;
        state = State.Drawing2D;

        /*
        northPoles = GameObject.FindGameObjectsWithTag("North Pole");
        southPoles = GameObject.FindGameObjectsWithTag("South Pole");
        DrawLines2D();
        */
    }

    // Update is called once per frame
    void Update()
    {
        GameObject GlobalParams = GameObject.FindGameObjectWithTag("Global Params");
        if (GlobalParams != null)
        {
            SyncSpawnedGlobalParams syncSpawnedGlobalParams =
                (SyncSpawnedGlobalParams)GlobalParams.
                GetComponent<DefaultSyncModelAccessor>().SyncModel;
            bool shouldShowMagneticForceLines = syncSpawnedGlobalParams.ShouldShowMagneticForceLines.Value;
            if (!shouldShowMagneticForceLines)
            {
                DeleteLines();
            }
            else
            {
                DrawLines2D();
            }
        }
        else
        {
            DrawLines2D();
        }


        /*
        // 追加
        int ownerId = GetComponent<DefaultSyncModelAccessor>().SyncModel.OwnerId;
        int myUserId = SharingStage.Instance.Manager.GetLocalUser().GetID();
        if (ownerId == myUserId)
        {
            myMagnet = gameObject;
            DeleteLines();
            DrawLines2D();
        }
        */
    }

    public void DrawLines2D()
    {
        northPoles = GameObject.FindGameObjectsWithTag("North Pole");
        southPoles = GameObject.FindGameObjectsWithTag("South Pole");

        int ownerId = GetComponent<DefaultSyncModelAccessor>().SyncModel.OwnerId;
        int myUserId = SharingStage.Instance.Manager.GetLocalUser().GetID();
        if (ownerId == myUserId)
        {
            myMagnet = gameObject;
            DeleteLines();

            magneticForceLineForOnOff.SetActive(true);

            for (int j = -1; j <= 1; j += 2)  // j=-1, 1のときS極、N極側の磁力線を描く
            {
                for (int i = -5; i <= 5; i++)
                {
                    GameObject magneticForceLine =
                        Instantiate(this.magneticForceLineForOnOff, transform.position, Quaternion.identity);

                    // 作成したオブジェクトを子として登録
                    magneticForceLine.tag = "CloneLine";
                    magneticForceLine.transform.parent = transform;

                    Vector3 myBarMagnetWorthPoleWorldPosition = transform.Find("Body1/North Pole").position;
                    Vector3 myBarMagnetSouthPoleWorldPosition = transform.Find("Body2/South Pole").position;

                    Vector3 barMagnetDirection = transform.rotation.eulerAngles;
                    //Debug.Log("transform.rotation.eulerAngles = " + barMagnetDirection);

                    if (j == 1)  // N極から
                    {
                        bool lineIsFromNorthPole = true;
                        Vector3 shiftPositionFromMyNorthPole = new Vector3(
                            0.001f * i,  // x
                            0.01f - 0.002f * Math.Abs(i),  // y
                            0);  // z
                        shiftPositionFromMyNorthPole =
                            myMagnet.transform.rotation * shiftPositionFromMyNorthPole;
                        Vector3 startPosition = myBarMagnetWorthPoleWorldPosition + shiftPositionFromMyNorthPole;
                        DrawLine(magneticForceLine, lineIsFromNorthPole, startPosition, 0.003f);
                    }
                    else  // S極から
                    {
                        bool lineIsFromNorthPole = false;
                        Vector3 shiftPositionFromMySouthPole = new Vector3(
                            0.001f * i,  // x
                            -0.01f + 0.002f * Math.Abs(i),  // y
                            0);  // z
                        shiftPositionFromMySouthPole =
                            myMagnet.transform.rotation * shiftPositionFromMySouthPole;
                        Vector3 startPosition = myBarMagnetSouthPoleWorldPosition + shiftPositionFromMySouthPole;
                        DrawLine(magneticForceLine, lineIsFromNorthPole, startPosition, 0.003f);
                    }
                }
            }

            magneticForceLineForOnOff.SetActive(false);
        }
    }

    public void DrawLines3D()
    {
        transform.rotation = Quaternion.Euler(0, 0, -90);

        magneticForceLineForOnOff.SetActive(true);

        for (int j = -1; j <= 1; j += 2)  // j=-1, 1のときS極、N極側の磁力線を描く
        {
            bool lineIsFromNorthPole = true;
            if (j == -1)
            {
                lineIsFromNorthPole = false;
            }

            for (int i = -1; i <= 1; i++)
            {
                for (int k = -2; k <= 2; k+=2)
                {
                    //GameObject magneticForceLine = (GameObject)Resources.Load("MagneticForceLine");
                    GameObject magneticForceLine = 
                        Instantiate(magneticForceLineForOnOff, transform.position, Quaternion.identity);
                    
                    // 作成したオブジェクトを子として登録
                    magneticForceLine.tag = "CloneLine";
                    magneticForceLine.transform.parent = transform;
                    Vector3 startPosition = new Vector3(
                        (0.05f + 0.0001f + 0.005f - 0.0012f * Math.Abs(i)) * j,  // x 
                        -0.0125f + 0.0025f * i,  // y
                        -0.0125f + 0.0025f * k);  // z
                    DrawLine(magneticForceLine, lineIsFromNorthPole, startPosition, 0.002f);
                }
            }
        }

        magneticForceLineForOnOff.SetActive(false);
    }

    /// <summary>
    /// 引数の(x, y, z)を始点として磁力線を描く
    /// </summary>
    /// <param name="magnetForceLine"></param>
    /// <param name="lineIsFromNorthPole"></param>
    /// <param name="startX">始点</param>
    /// <param name="startY">始点</param>
    /// <param name="startZ">始点</param>
    void DrawLine(GameObject magnetForceLine, bool lineIsFromNorthPole, Vector3 startPosition, float width)
    {
        LineRenderer line = magnetForceLine.GetComponent<LineRenderer>();
        line.useWorldSpace = true;

        line.SetPosition(0, startPosition);  // 引数の(x, y, z)を始点として磁力線を描く

        line.startWidth = width;
        line.endWidth = width;
        //lineの太さ

        // --- S極 ---
        // S極からの頂点への変位ベクトル(ベクトルs)
        Vector3 displacementFromSouthPoleToCurrentPoint;

        // ベクトルsの長さの2乗（これで単位ベクトルを割る）
        double lengthSquareFromSouthPoleToCurrentPoint;

        // ベクトルsの単位ベクトル
        Vector3 normalizedDisplacementFromSouthPoleToCurrentPoint;

        // ベクトルs
        Vector3 forceFromSouthPoleToCurrentPoint;

        // --- N極 ---
        // N極からの頂点への変位ベクトル(ベクトルn)
        Vector3 displacementFromNorthPoleToCurrentPoint;

        // ベクトルnの長さの2乗（これで単位ベクトルを割る）
        double lengthSquareFromNorthPoleToCurrentPoint;

        // ベクトルnの単位ベクトル
        Vector3 normalizedDisplacementFromNorthPoleToCurrentPoint;

        // ベクトルn
        Vector3 forceFromNorthPoleToCurrentPoint;
        // -----

        // 描画点から磁石の中心までの変位ベクトル
        Vector3 displacementFromMagnetToCurrentPoint;

        line.positionCount = 100;  // 描く線分の数

        Vector3 positionCurrentPoint = startPosition;

        float scaleToFitLocalPosition = 0.15f;

        for (int i = 1; i < line.positionCount; i++)
        {
            int k;
            if (lineIsFromNorthPole)
            {
                k = 1;
            }
            else
            {
                k = -1;
            }

            Vector3 positionBarMagnetNorthPoleHoge = transform.TransformPoint(northPoles[0].transform.position);
            Vector3 positionBarMagnetSouthPoleHoge = transform.TransformPoint(southPoles[0].transform.position);

            // 磁石の中心を設定
            Vector3 positionCenterOfBarMagnet = 
                (positionBarMagnetNorthPoleHoge + positionBarMagnetSouthPoleHoge) / 2;

            // --- N極 ---
            Vector3 sumOfForceFromNorthPoleToCurrentPoint = new Vector3();

            foreach (var northPole in northPoles)
            {
                Vector3 positionBarMagnetNorthPole = northPole.transform.position;

                // N極からの頂点への変位ベクトル(ベクトルn)
                displacementFromNorthPoleToCurrentPoint = positionCurrentPoint - positionBarMagnetNorthPole;

                // ベクトルnの長さの2乗（これで単位ベクトルを割る）
                lengthSquareFromNorthPoleToCurrentPoint =
                displacementFromNorthPoleToCurrentPoint.sqrMagnitude;

                // ベクトルnの単位ベクトル
                normalizedDisplacementFromNorthPoleToCurrentPoint =
                    displacementFromNorthPoleToCurrentPoint.normalized;

                // ベクトルn
                forceFromNorthPoleToCurrentPoint =
                    normalizedDisplacementFromNorthPoleToCurrentPoint / (float)lengthSquareFromNorthPoleToCurrentPoint;

                // ベクトルの和
                sumOfForceFromNorthPoleToCurrentPoint += forceFromNorthPoleToCurrentPoint;
            }

            // --- S極 ---
            Vector3 sumOfForceFromSouthPoleToCurrentPoint = new Vector3();

            foreach (var southPole in southPoles)
            {
                Vector3 positionBarMagnetSouthPole = southPole.transform.position;

                // S極からの頂点への変位ベクトル(ベクトルs)
                displacementFromSouthPoleToCurrentPoint = 1 * (positionCurrentPoint - positionBarMagnetSouthPole);

                // ベクトルsの長さの2乗（これで単位ベクトルを割る）
                lengthSquareFromSouthPoleToCurrentPoint =
                displacementFromSouthPoleToCurrentPoint.sqrMagnitude;

                // ベクトルsの単位ベクトル
                normalizedDisplacementFromSouthPoleToCurrentPoint =
                    displacementFromSouthPoleToCurrentPoint.normalized;

                // ベクトルs
                forceFromSouthPoleToCurrentPoint =
                    normalizedDisplacementFromSouthPoleToCurrentPoint / (float)lengthSquareFromSouthPoleToCurrentPoint;

                // ベクトルの和
                sumOfForceFromSouthPoleToCurrentPoint += forceFromSouthPoleToCurrentPoint;
            }

            // --- 線分の長さを決める ---
            displacementFromMagnetToCurrentPoint = positionCurrentPoint - positionCenterOfBarMagnet;  // 棒磁石の中心
            float lengthSquareFromMagnetToCurrent = displacementFromMagnetToCurrentPoint.magnitude;

            float lengthOfLine;
            if (lengthSquareFromMagnetToCurrent < 0.05f * scaleToFitLocalPosition)
            {
                lengthOfLine = 0.002f * scaleToFitLocalPosition;
            }
            else if (0.05f * scaleToFitLocalPosition <= lengthSquareFromMagnetToCurrent && lengthSquareFromMagnetToCurrent < 0.1f * scaleToFitLocalPosition)
            {
                lengthOfLine = 0.005f * scaleToFitLocalPosition;
            }
            else if (0.1f * scaleToFitLocalPosition <= lengthSquareFromMagnetToCurrent && lengthSquareFromMagnetToCurrent < 0.2f * scaleToFitLocalPosition)
            {
                lengthOfLine = 0.01f * scaleToFitLocalPosition;
            }
            else if (0.2f * scaleToFitLocalPosition <= lengthSquareFromMagnetToCurrent && lengthSquareFromMagnetToCurrent < 0.5f * scaleToFitLocalPosition)
            {
                lengthOfLine = 0.02f * scaleToFitLocalPosition;
            }
            else
            {
                lengthOfLine = 0.05f * scaleToFitLocalPosition;
            }

            // --- 描画 ---
            positionCurrentPoint += 
                k * (sumOfForceFromNorthPoleToCurrentPoint - sumOfForceFromSouthPoleToCurrentPoint).normalized * 
                lengthOfLine;
            line.SetPosition(i, positionCurrentPoint);

            /*
            // --- 中断判定 ---
            if (lineIsFromNorthPole)
            {
                if (lengthSquareFromSouthPoleToCurrentPoint < lengthSquareFromNorthPoleToCurrentPoint)
                {
                    line.positionCount = i;
                }
            }
            else
            {
                if (lengthSquareFromNorthPoleToCurrentPoint < lengthSquareFromSouthPoleToCurrentPoint)
                {
                    line.positionCount = i;
                }
            }
            */
        }
    }

    public void DeleteLines()
    {
        GameObject[] lines = GameObject.FindGameObjectsWithTag("CloneLine");

        foreach (GameObject line in lines)
        {
            Destroy(line);
        }
    }

    public void ShowMagneticForceLines()
    {
        transform.rotation = Quaternion.Euler(0, 0, -90);

        /*
        MotionAndAudioManager motionAndAudioManager = MotionAndAudioManager.Instance;

        motionAndAudioManager.PlayAudioClip(motionAndAudioManager.ACTap);

        // 移動中に磁力線を表示させると移動音が途絶えるので再度再生する
        if (motionAndAudioManager.isRotating || motionAndAudioManager.isMovingRoundly || motionAndAudioManager.isGoAndBack)
        {
            MyHelper.MyDelayMethod(this, 2f, () =>
            {
                motionAndAudioManager.PlayAudioClip(motionAndAudioManager.ACMoving);
            });
        }

        /*
        {  
            manageBarMagnet.PlayAudioClip(manageBarMagnet.ACMoving);
        }
        */

        MyHelper.MyScene scene = MyHelper.MyGetScene();
        if (scene == MyHelper.MyScene.Scene3D)
        {
            if (state == State.NotDrawing)
            {
                Debug.Log("draw 2D lines");
                DeleteLines();
                DrawLines3D();
                state = State.Drawing3D;
            }
            else if (state == State.Drawing3D)
            {
                Debug.Log("delete 3D lines");
                DeleteLines();
                state = State.NotDrawing;
            }
        }
        else
        {
            if (state == State.NotDrawing)
            {
                Debug.Log("draw 2D lines");
                DrawLines2D();
                state = State.Drawing2D;
            }
            else if (state == State.Drawing2D)
            {
                Debug.Log("delete lines");
                DeleteLines();
                state = State.NotDrawing;
            }
        }
    }
}
