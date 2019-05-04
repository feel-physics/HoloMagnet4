using UnityEngine;
using HoloToolkit.Sharing;

namespace FeelPhysics.HoloMagnet36
{
    /// <summary>
    /// 棒磁石のz座標を1.5に固定する
    /// </summary>
    public class HoldZPostion : MonoBehaviour
    {
        private bool hasHoldZPostion = false;
        private GameObject sharingStage = null;
        private string objectTag = "SharingStage";

        // Use this for initialization
        void Start()
        {

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
                bool shouldHoldZPosition = syncSpawnedGlobalParams.ShouldHoldBarMagnetZPosition.Value;
                if (shouldHoldZPosition)  // z座標を固定する
                {
                    transform.localPosition = new Vector3(
                        transform.localPosition.x,
                        transform.localPosition.y,
                        1.5f);
                    transform.localRotation = Quaternion.Euler(0, 0, -90);
                }
            }
        }
    }
}