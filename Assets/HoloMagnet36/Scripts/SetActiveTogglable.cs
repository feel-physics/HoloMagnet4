using UnityEngine;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.Sharing;

namespace FeelPhysics.HoloMagnet36
{
    /// <summary>
    /// サーバIP手動設定パネルを、サーバに接続したら非表示にし、サーバから切断されたら表示する
    /// </summary>
    public class SetActiveTogglable : MonoBehaviour, IInputClickHandler
    {
        public GameObject DebugLog;

        private bool hasShownDebugLog = true;

        public void OnInputClicked(InputClickedEventData eventData)
        {
            eventData.Use(); // イベントが使われたことを記録して、他の処理に受け取られるのを防ぐ

            GameObject GlobalParams = GameObject.FindGameObjectWithTag("Global Params");
            if (GlobalParams != null)
            {
                SyncSpawnedGlobalParams syncSpawnedGlobalParams =
                    (SyncSpawnedGlobalParams)GlobalParams.
                    GetComponent<DefaultSyncModelAccessor>().SyncModel;
                bool shouldShowDebugLog = syncSpawnedGlobalParams.ShouldShowDebugLog.Value;
                if (!shouldShowDebugLog)
                {
                    syncSpawnedGlobalParams.ShouldShowDebugLog.Value = true;
                }
                else
                {
                    syncSpawnedGlobalParams.ShouldShowDebugLog.Value = false;
                }
            }
        }

        private void Update()
        {
            GameObject GlobalParams = GameObject.FindGameObjectWithTag("Global Params");
            if (GlobalParams != null)
            {
                SyncSpawnedGlobalParams syncSpawnedGlobalParams =
                    (SyncSpawnedGlobalParams)GlobalParams.
                    GetComponent<DefaultSyncModelAccessor>().SyncModel;
                bool shouldShowDebugLog = syncSpawnedGlobalParams.ShouldShowDebugLog.Value;
                if (!shouldShowDebugLog)  // 消す
                {
                    if (hasShownDebugLog)
                    {
                        DebugLog.SetActive(false);
                        hasShownDebugLog = false;
                    }
                }
                else  // 表示する
                {
                    if (!hasShownDebugLog)
                    {
                        DebugLog.SetActive(true);
                        hasShownDebugLog = true;
                    }
                }
            }
        }
    }
}