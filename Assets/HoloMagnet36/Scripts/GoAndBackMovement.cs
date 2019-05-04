using UnityEngine;
using HoloToolkit.Sharing;

namespace FeelPhysics.HoloMagnet36
{
    /// <summary>
    /// 棒磁石のz座標を1.5に固定する
    /// </summary>
    public class GoAndBackMovement : MonoBehaviour
    {
        public AudioClip ACMoving;
        AudioSource audioSource;
        private bool hasGoneAndBack = false;
        private bool isPlayingAudio = false;

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
                bool shouldGoAndBack = syncSpawnedGlobalParams.ShouldShowCompass3D.Value;
                if (shouldGoAndBack)  // z座標を固定する
                {
                    GameObject sharingStage = GameObject.Find("SharingStage");
                    transform.position =
                        sharingStage.transform.position +
                        sharingStage.transform.rotation * 
                        new Vector3(0.3f * Mathf.Sin(Time.time / 3f) - 0.04f, -0.2f, 1.0f);

                    transform.rotation = sharingStage.transform.rotation *
                        Quaternion.Euler(0, 0, -90);
                    //transform.rotation = Quaternion.identity;
                    //transform.rotation = Quaternion.Euler(
                    //    sharingStage.transform.rotation * new Vector3(0, 0, -90));

                    if (!hasGoneAndBack)
                    {
                        GameObject[] barMagnets = GameObject.FindGameObjectsWithTag("Bar Magnet");
                        if (gameObject == barMagnets[0])
                        {
                            MyHelper.MyDelayMethod(this, 2f, () =>
                            {
                                PlayAudioClip(ACMoving);
                            });
                            hasGoneAndBack = true;
                            isPlayingAudio = true;
                        }
                    }
                }
                else
                {
                    hasGoneAndBack = false;
                    if (isPlayingAudio)
                    {
                        StopAudioClip();
                        isPlayingAudio = false;
                    }
                }
            }
        }


        public void PlayAudioClip(AudioClip ac)
        {
            audioSource = GetComponents<AudioSource>()[0];
            audioSource.clip = ac;
            audioSource.loop = true;
            audioSource.Play();
        }

        public void StopAudioClip()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.Stop();
        }
    }
}