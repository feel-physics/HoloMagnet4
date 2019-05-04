using UnityEngine;

public class CompassPlacer2D : MonoBehaviour
{
    float pitchCompass = 0.07f;
    int numCompassX = 31;
    int numCompassY = 11;

    // Use this for initialization
    void Start()
    {
        GameObject compass = (GameObject)Resources.Load("Compass180509/Compass2D180509");
        GameObject sharingStage = GameObject.Find("SharingStage");
        for (int d = 0; d < 1; d++)
        {
            //for (int h = 0; h < numCompass; h++)
            for (int h = 0; h < numCompassY; h++)
            {
                //for (int w = 0; w < numCompass; w++)
                for (int w = 0; w < numCompassX; w++)
                {
                    var localPositionCompassCloned = new Vector3(
                            pitchCompass * w - numCompassX / 2.0f * pitchCompass,  // x軸に対し対称に±方向に方位磁針を並べる
                            pitchCompass * h - numCompassY / 2.0f * pitchCompass + 0.02f,  // y軸に対し対称に±方向に方位磁針を並べる
                            1.5f);
                    //var compassCloned = Instantiate(compass, new Vector3(
                    //pitchCompass * w - numCompassX / 2.0f * pitchCompass,  // x軸に対し対称に±方向に方位磁針を並べる
                    //pitchCompass * h - numCompassY / 2.0f * pitchCompass + 0.02f,  // y軸に対し対称に±方向に方位磁針を並べる
                    //1.5f),
                    var compassCloned = Instantiate(compass, 
                        new Vector3(0, 0, 0), Quaternion.identity);
                    compassCloned.transform.parent = sharingStage.transform;
                    compassCloned.transform.localPosition = localPositionCompassCloned;
                }
            }
        }

        //compass.SetActive(false);
    }
}