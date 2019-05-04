using HoloToolkit.Sharing;
using UnityEngine;

/// <summary>
/// 動く2つのオブジェクトのあいだに線を引く
/// </summary>
public class DrawLineFromSelfToBarMagnet : MonoBehaviour {

    void Update()
    {
        GameObject[] barMagnets;
        barMagnets = GameObject.FindGameObjectsWithTag("Bar Magnet");

        foreach (var barMagnet in barMagnets)
        {
            int ownerId = barMagnet.GetComponent<DefaultSyncModelAccessor>().SyncModel.OwnerId;
            int myUserId = SharingStage.Instance.Manager.GetLocalUser().GetID();
            if (ownerId == myUserId)
            {
                MyHelper.DrawDynamicLine(gameObject, barMagnet, gameObject);
            }
        }
    }
}
