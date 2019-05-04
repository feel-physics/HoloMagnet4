using System;
using HoloToolkit.Sharing;
using HoloToolkit.Sharing.Spawning;
using UnityEngine;

namespace FeelPhysics.HoloMagnet36
{
    /// <summary>
    /// 自分がシェアリングサーバに参加したときに、磁石を生成する
    /// </summary>
    public class BarMagnetSpawner : MonoBehaviour
    {
        /// <summary>
        /// PrefabSpawnManagerへの参照を取るためのフィールド
        /// </summary>
        [SerializeField]
        private PrefabSpawnManager spawnManager;

        [SerializeField]
        [Tooltip("Optional transform target, for when you want to spawn the object on a specific parent.  If this value is not set, then the spawned objects will be spawned on this game object.")]
        private Transform spawnParentTransform;

        /// <summary>
        /// デバッグログの表示先
        /// </summary>
        public TextMesh DebugLogText;

        /// <summary>
        /// ユーザーID
        /// </summary>
        private long myUserId;

        private void Awake()
        {
            // If we don't have a spawn parent transform, then spawn the object on this transform.
            if (spawnParentTransform == null)
            {
                spawnParentTransform = transform;
            }
        }

        /// <summary>
        /// シェアリングサーバーに接続するのを待つ
        /// </summary>
        private void Start()
        {
            SharingStage.Instance.SharingManagerConnected += this.Connected;
            this.DebugLogText.text += "\n[BarMagnetSpawner] Add event SharingManagerConnected";
        }

        /// <summary>
        /// シェアリングサーバに接続すると呼ばれる
        /// ここからさらにユーザが参加するのを待つ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Connected(object sender, System.EventArgs e)
        {
            this.DebugLogText.text += "\n[BarMagnetSpawner] Connected";
            SharingStage.Instance.SharingManagerConnected -= this.Connected;

            SharingStage.Instance.SessionUsersTracker.UserJoined += this.UserJoinedSession;
            this.DebugLogText.text += "\n[BarMagnetSpawner] Add event UserJoined";
            SharingStage.Instance.SessionUsersTracker.UserLeft += this.UserLeftSession;
        }

        /// <summary>
        /// 新しいユーザが、現在のセッションから退出すると呼ばれる
        /// </summary>
        /// <param name="user">現在のセッションから退出したユーザ</param>
        private void UserLeftSession(User user)
        {
            this.DebugLogText.text += "\n[BarMagnetSpawner] UserLeftSession(User user) > user.GetID(): " + user.GetID().ToString();

            // セッションから退出したユーザの棒磁石を削除する
            this.DeleteSomeonesAllMagnets(user.GetID());
        }

        /// <summary>
        /// 新しいユーザが、現在のセッションに参加すると呼ばれる
        /// </summary>
        /// <param name="joinedUser">現在のセッションに参加したユーザ</param>
        private void UserJoinedSession(User joinedUser)
        {
            this.myUserId = SharingStage.Instance.Manager.GetLocalUser().GetID();
            this.DebugLogText.text += "\n[BarMagnetSpawner] UserJoinedSession(User user) > user.GetID(): " +
                joinedUser.GetID().ToString();
            this.DebugLogText.text += "\n[BarMagnetSpawner] UserJoinedSession(User user) > " +
                "SharingStage.Instance.Manager.GetLocalUser().GetID(): " +
                SharingStage.Instance.Manager.GetLocalUser().GetID().ToString();

            // 他のユーザが参加したときは磁石の更新は行わない
            if (joinedUser.GetID() == this.myUserId)
            {
                this.DeleteSomeonesAllMagnets(this.myUserId);

                this.CreateMagnet(this.myUserId);
            }
        }

        /// <summary>
        /// 新たに参加したユーザのユーザIDの磁石がない場合、PrefabSpawnManagerを使ってSpawnする
        /// </summary>
        /// <param name="userId"></param>
        private void CreateMagnet(long userId)
        {
            Vector3 position = new Vector3(0, 0, 1.5f);
            Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, -90));
            var spawnedObject = new SyncSpawnedBarMagnet();
            this.spawnManager.Spawn(
                spawnedObject, position, rotation, spawnParentTransform.gameObject, "SpawnedBarMagnet", true);
        }

        /// <summary>
        /// セッション参加が複数回起きて自分が生成した磁石が複数になった場合のために、
        /// 自分が生成した磁石をすべて削除する
        /// </summary>
        public void DeleteSomeonesAllMagnets(long userId)
        {
            var magnets = GameObject.FindGameObjectsWithTag("Bar Magnet");
            foreach (var magnet in magnets)
            {
                var syncModelAccessor = magnet.GetComponent<DefaultSyncModelAccessor>();
                if (syncModelAccessor != null)
                {
                    var syncSpawnObject = (SyncSpawnedObject)syncModelAccessor.SyncModel;
                    if (syncSpawnObject.OwnerId == userId)
                    {
                        // 磁石のOnDestroyを走らせる
                        UnityEngine.Object.DestroyImmediate(magnet);

                        this.spawnManager.Delete(syncSpawnObject);
                    }
                }
            }
        }
    }
}