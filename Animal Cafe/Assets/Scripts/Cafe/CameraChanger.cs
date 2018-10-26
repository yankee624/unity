using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChanger : MonoBehaviour {

    public Camera[] cams;
    public int targetCam;

    public static Camera currentCam;
    public static Canvas currentCanvas;

	public void MakeCameraTop()
    {
        for(int i = 0; i < cams.Length; i++)
        {
            cams[i].depth = -1;
        }
        cams[targetCam].depth = 0;
        currentCam = cams[targetCam];
    }



    private void Start()
    {
        currentCam = Camera.main;
    }
}
