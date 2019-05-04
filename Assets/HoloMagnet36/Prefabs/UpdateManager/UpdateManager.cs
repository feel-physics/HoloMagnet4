using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateManager : MonoBehaviour
{
    // 初期宣言
    public List<Compass2DManaged> list2D = new List<Compass2DManaged>();
    public List<Compass3DManaged> list3D = new List<Compass3DManaged>();

    // Update is called once per frame
    void Update()
    {
        var count2D = list2D.Count;
        for (var i = 0; i < count2D; i++)
        {
            list2D[i].UpdateMe();
        }

        var count3D = list3D.Count;
        for (var i = 0; i < count3D; i++)
        {
            //list3D[i].UpdateMe();
        }
    }
}