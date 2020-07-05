using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using FeelPhysics.HoloMagnet36;
using HoloToolkit.Sharing;

public class MagneticForceLinesToggleVisible : MonoBehaviour, IInputClickHandler {
    public void OnInputClicked(InputClickedEventData eventData)
    {
        eventData.Use();

		ToggleMagneticForceLinesVisible();
	}


	public void ToggleMagneticForceLinesVisible()
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
                syncSpawnedGlobalParams.ShouldShowMagneticForceLines.Value = true;
            }
            else
            {
                syncSpawnedGlobalParams.ShouldShowMagneticForceLines.Value = false;
            }
        }

		return;
	}

}
