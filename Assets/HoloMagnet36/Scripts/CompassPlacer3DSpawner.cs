﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.Sharing.Spawning;
using HoloToolkit.Sharing;

namespace FeelPhysics.HoloMagnet36
{
    public class CompassPlacer3DSpawner : MonoBehaviour, IInputClickHandler
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

        public void OnInputClicked(InputClickedEventData eventData)
        {
            eventData.Use(); // イベントが使われたことを記録して、他の処理に受け取られるのを防ぐ

			ToggleCompass3D();
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

		public void ToggleCompass3D()
		{
            GameObject GlobalParams = GameObject.FindGameObjectWithTag("Global Params");
            if (GlobalParams != null)
            {
                SyncSpawnedGlobalParams syncSpawnedGlobalParams =
                    (SyncSpawnedGlobalParams)GlobalParams.
                    GetComponent<DefaultSyncModelAccessor>().SyncModel;
                bool shouldShowCompass = syncSpawnedGlobalParams.ShouldShowCompass3D.Value;
                if (!shouldShowCompass)
                {
                    syncSpawnedGlobalParams.ShouldShowCompass3D.Value = true;

                    Vector3 position = new Vector3(0, 0, 0f);
                    Quaternion rotation = Quaternion.identity;
                    var spawnedObject = new SyncSpawnedCompassPlacer3D();  // 要編集
                    this.spawnManager.Spawn(
                        spawnedObject, position, rotation, spawnParentTransform.gameObject,
                        "CompassPlacer3D", false);  // 要編集
                }
                else
                {
                    syncSpawnedGlobalParams.ShouldShowCompass3D.Value = false;

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
                }
            }

			return;
		}

    }
}