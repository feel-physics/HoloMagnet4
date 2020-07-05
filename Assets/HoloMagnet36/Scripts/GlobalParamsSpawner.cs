using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.Sharing.Spawning;
using HoloToolkit.Sharing;

namespace FeelPhysics.HoloMagnet36
{
    public class GlobalParamsSpawner : MonoBehaviour, IInputClickHandler
    {
        /// <summary>
        /// PrefabSpawnManagerへの参照を取るためのフィールド
        /// </summary>
        [SerializeField]
        private PrefabSpawnManager spawnManager;

        [SerializeField]
        [Tooltip("Optional transform target, for when you want to spawn the object on a specific parent.  If this value is not set, then the spawned objects will be spawned on this game object.")]
        private Transform spawnParentTransform;

        public string PrefabTag;
        private bool shouldExistPrefab = false;

        public void OnInputClicked(InputClickedEventData eventData)
        {
            eventData.Use(); // イベントが使われたことを記録して、他の処理に受け取られるのを防ぐ

			ToggleSpawnGlobalParams();
        }

        private void Awake()
        {
            // If we don't have a spawn parent transform, then spawn the object on this transform.
            if (spawnParentTransform == null)
            {
                spawnParentTransform = transform;
            }
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

		public void ToggleSpawnGlobalParams()
		{
            if (shouldExistPrefab)
            {
                var prefabs = GameObject.FindGameObjectsWithTag(PrefabTag);
                foreach (var prefab in prefabs)
                {
                    var syncModelAccessor = prefab.GetComponent<DefaultSyncModelAccessor>();
                    if (syncModelAccessor != null)
                    {
                        var syncSpawnObject = (SyncSpawnedObject)syncModelAccessor.SyncModel;
                        // 磁石のOnDestroyを走らせる
                        UnityEngine.Object.DestroyImmediate(prefab);
                        this.spawnManager.Delete(syncSpawnObject);
                    }
                }

                shouldExistPrefab = false;
            }
            else
            {
                Vector3 position = new Vector3(0, 0, 0f);
                Quaternion rotation = Quaternion.identity;
                var spawnedObject = new SyncSpawnedGlobalParams();  // 注意
                this.spawnManager.Spawn(
                    spawnedObject, position, rotation, spawnParentTransform.gameObject, PrefabTag, false);

                shouldExistPrefab = true;
            }
			return;
		}

    }
}