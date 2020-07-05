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

	private SyncSpawnedGlobalParams syncSpawnedGlobalParams = null;

	// Use this for initialization
	void Start()
	{
		
	}
	
	// Update is called once per frame
	void Update()
	{
		//現状SyncSpawnedGlobalParamsの生成タイミングを確認する術がないので、Updateでチェックし続けて取得する機構で実装.
		if( syncSpawnedGlobalParams == null)
		{
			GameObject GlobalParams = GameObject.FindGameObjectWithTag("Global Params");
			if (GlobalParams != null)
			{
				SyncSpawnedGlobalParams syncSpawnedGlobalParams = 
					(SyncSpawnedGlobalParams)GlobalParams.GetComponent<DefaultSyncModelAccessor>( ).SyncModel;
			}
		}
	}


	public void AdvanceSequence()
	{
		if (syncSpawnedGlobalParams == null)
		{
			return;
		}
		switch((AppSequenceState)syncSpawnedGlobalParams.AppSequenceStateToInt.Value)
		{
			case AppSequenceState.SetupAnchor:
				break;
			case AppSequenceState.StartSharing:
				break;
			case AppSequenceState.PlaneMagneticField:
				break;
			case AppSequenceState.ThreeDimensionalMagneticField:
				break;
			case AppSequenceState.MagneticFieldLines:
				break;
		}

		return;
	}


}
