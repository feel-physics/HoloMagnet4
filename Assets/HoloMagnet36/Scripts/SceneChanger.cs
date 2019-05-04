using UnityEngine;
using HoloToolkit.Unity.InputModule;
using FeelPhysics.HoloMagnet36;
using HoloToolkit.Sharing;

public class SceneChanger : MonoBehaviour, IInputClickHandler {
    public void OnInputClicked(InputClickedEventData eventData)
    {
        var myUserId = SharingStage.Instance.Manager.GetLocalUser().GetID();
        GameObject.Find("BarMagnetSpawner").GetComponent<BarMagnetSpawner>().DeleteSomeonesAllMagnets(myUserId);
        MyHelper.MyLoadScene(MyHelper.MyScene.Scene3D);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
