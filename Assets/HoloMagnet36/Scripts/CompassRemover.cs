using FeelPhysics.HoloMagnet36;
using HoloToolkit.Sharing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Global Paramsにアタッチされていて、方位磁針を削除する
/// </summary>
public class CompassRemover : MonoBehaviour {

    private bool hasShownCompass;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    private void Update()
    {
        GameObject GlobalParams = GameObject.FindGameObjectWithTag("Global Params");
        if (GlobalParams != null)
        {
            SyncSpawnedGlobalParams syncSpawnedGlobalParams =
                (SyncSpawnedGlobalParams)GlobalParams.
                GetComponent<DefaultSyncModelAccessor>().SyncModel;
            bool shouldShowCompass2D = syncSpawnedGlobalParams.ShouldShowCompass2D.Value;
            bool shouldShowCompass3D = syncSpawnedGlobalParams.ShouldShowCompass3D.Value;
            if (!(shouldShowCompass2D || shouldShowCompass3D))
            {
                if (hasShownCompass)
                {
                    // Update Managerに自分を登録する
                    UpdateManager um = GameObject.Find("UpdateManager").GetComponent<UpdateManager>();

                    GameObject[] allCompasses = GameObject.FindGameObjectsWithTag("Compass");
                    foreach (var compass in allCompasses)
                    {
                        Destroy(compass);
                    }
                }

                hasShownCompass = false;
            }
            else
            {
                hasShownCompass = true;
            }
        }
    }
}
