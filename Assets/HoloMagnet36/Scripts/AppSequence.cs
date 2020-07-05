using FeelPhysics.HoloMagnet36;
using HoloToolkit.Sharing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//アプリのメイン部分のシーケンス管理用.
public class AppSequence : MonoBehaviour {

	//メインとなるコンテンツのシーケンス一覧.
	public enum AppSequenceState{
		//ワールドアンカーの設置.
		SetupAnchor,
		//シェアリング開始.
		StartSharing,
		//平面での磁界の可視化.
		PlaneMagneticField,
		//3次元での磁界の可視化.
		ThreeDimensionalMagneticField,
		//磁力線の可視化.
		MagneticFieldLines,
	}

	[SerializeField]private GlobalParamsSpawner globalParamsSpawner = null;
	[SerializeField]private HoldBarMagnetZPosition holdBarMagnetZPosition = null;
	[SerializeField]private CompassPlacer2DSpawner compassPlacer2DSpawner = null;
	[SerializeField]private CompassPlacer3DSpawner compassPlacer3DSpawner = null;
	[SerializeField]private MagneticForceLinesToggleVisible magneticForceLinesToggleVisible = null;

	private SyncSpawnedGlobalParams syncSpawnedGlobalParams = null;

	// Use this for initialization
	void Start()
	{
		
	}
	
	// Update is called once per frame
	void Update()
	{
		CheckSyncSpawnedGlobalParams();

		return;
	}


	private void CheckSyncSpawnedGlobalParams( )
	{
		//現状SyncSpawnedGlobalParamsの生成タイミングを確認する術が不明なので、Updateでチェックし続けて取得する機構で実装.
		if (syncSpawnedGlobalParams == null)
		{
			GameObject GlobalParams = GameObject.FindGameObjectWithTag("Global Params");
			if (GlobalParams != null)
			{
				syncSpawnedGlobalParams = (SyncSpawnedGlobalParams)GlobalParams.GetComponent<DefaultSyncModelAccessor>( ).SyncModel;
			}
		}

		return;
	}

	//アプリのシーケンスを進行させる処理.
	public void AdvanceSequence()
	{
		//SyncSpawnedGlobalParamsが見つかっていない場合は、まだ生成されていないとして、処理を進める.
		if( syncSpawnedGlobalParams == null)
		{
			if (globalParamsSpawner == null)
			{
				return;
			}
			globalParamsSpawner.ToggleSpawnGlobalParams();
			CheckSyncSpawnedGlobalParams();
			syncSpawnedGlobalParams.AppSequenceStateToInt.Value = (int)AppSequenceState.StartSharing;
			return;
		}
		switch((AppSequenceState)syncSpawnedGlobalParams.AppSequenceStateToInt.Value)
		{
			case AppSequenceState.SetupAnchor:
				if (globalParamsSpawner == null)
				{
					return;
				}
				globalParamsSpawner.ToggleSpawnGlobalParams();
				syncSpawnedGlobalParams.AppSequenceStateToInt.Value = (int)AppSequenceState.StartSharing;
				break;

			case AppSequenceState.StartSharing:
				if (holdBarMagnetZPosition == null || compassPlacer2DSpawner == null)
				{
					return;
				}
				holdBarMagnetZPosition.ToggleHoldBarMagnetZPosition();
				compassPlacer2DSpawner.ToggleCompass2D();
				syncSpawnedGlobalParams.AppSequenceStateToInt.Value = (int)AppSequenceState.PlaneMagneticField;
				break;

			case AppSequenceState.PlaneMagneticField:
				if (holdBarMagnetZPosition == null || compassPlacer2DSpawner == null || compassPlacer3DSpawner == null)
				{
					return;
				}
				holdBarMagnetZPosition.ToggleHoldBarMagnetZPosition();
				StartCoroutine(CallToggleCompass2DAnd3D(true));
				syncSpawnedGlobalParams.AppSequenceStateToInt.Value = (int)AppSequenceState.ThreeDimensionalMagneticField;
				break;

			case AppSequenceState.ThreeDimensionalMagneticField:
				if (compassPlacer2DSpawner == null || compassPlacer3DSpawner == null || magneticForceLinesToggleVisible == null)
				{
					return;
				}
				StartCoroutine(CallToggleCompass2DAnd3D(false));
				magneticForceLinesToggleVisible.ToggleMagneticForceLinesVisible();
				syncSpawnedGlobalParams.AppSequenceStateToInt.Value = (int)AppSequenceState.MagneticFieldLines;
				break;

			case AppSequenceState.MagneticFieldLines:
				if (compassPlacer2DSpawner == null || magneticForceLinesToggleVisible == null)
				{
					return;
				}
				compassPlacer2DSpawner.ToggleCompass2D();
				magneticForceLinesToggleVisible.ToggleMagneticForceLinesVisible();
				syncSpawnedGlobalParams.AppSequenceStateToInt.Value = (int)AppSequenceState.StartSharing;
				break;

			default:
				syncSpawnedGlobalParams.AppSequenceStateToInt.Value = (int)AppSequenceState.StartSharing;
				break;
		}

		return;
	}

	//2Dと3Dのコンパスの表示切り替え時にDestroyの影響と思われるが同じフレームでは正常に動作されられないため、1フレーム待って実行させるためのコルーチン.
	private IEnumerator CallToggleCompass2DAnd3D(bool isFirst2D)
	{
		if (isFirst2D)
		{
			compassPlacer2DSpawner.ToggleCompass2D();
		}
		else
		{
			compassPlacer3DSpawner.ToggleCompass3D();
		}
		yield return null;
		if (isFirst2D)
		{
			compassPlacer3DSpawner.ToggleCompass3D();
		}
		else
		{
			compassPlacer2DSpawner.ToggleCompass2D();
		}
	}


}
