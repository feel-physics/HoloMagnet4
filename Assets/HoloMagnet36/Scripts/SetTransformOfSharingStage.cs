using UnityEngine;

public class SetTransformOfSharingStage : MonoBehaviour {

    // 初期設定
    private string passiveObjectName = "SharingStage";
    private GameObject passiveObject = null;
    private bool ignoreOnSharing = false;
    private bool hasLogged = false;

    // Use this for initialization
    void Start()
    {
        passiveObject = GameObject.Find(passiveObjectName);
    }

    // Update is called once per frame
    void Update()
    {
        MyHelper.SetTransFormOfObject(passiveObject, transform, ignoreOnSharing, hasLogged);
    }
}