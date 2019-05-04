using UnityEngine;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.Sharing;

namespace FeelPhysics.HoloMagnet36
{
    /// <summary>
    /// サーバIP手動設定パネルを、サーバに接続したら非表示にし、サーバから切断されたら表示する
    /// </summary>
    public class HoldBarMagnetZPosition : MonoBehaviour, IInputClickHandler
    {
        public void OnInputClicked(InputClickedEventData eventData)
        {
            eventData.Use(); // イベントが使われたことを記録して、他の処理に受け取られるのを防ぐ

            GameObject GlobalParams = GameObject.FindGameObjectWithTag("Global Params");
            if (GlobalParams != null)
            {
                SyncSpawnedGlobalParams syncSpawnedGlobalParams =
                    (SyncSpawnedGlobalParams)GlobalParams.
                    GetComponent<DefaultSyncModelAccessor>().SyncModel;
                bool shouldHoldBarMagnet = syncSpawnedGlobalParams.ShouldHoldBarMagnetZPosition.Value;
                if (!shouldHoldBarMagnet)
                {
                    syncSpawnedGlobalParams.ShouldHoldBarMagnetZPosition.Value = true;
                }
                else
                {
                    syncSpawnedGlobalParams.ShouldHoldBarMagnetZPosition.Value = false;
                }
            }
        }
    }
}