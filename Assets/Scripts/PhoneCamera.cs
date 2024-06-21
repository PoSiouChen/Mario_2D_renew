using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneCamera : MonoBehaviour
{
    public bool isCamAvailable;
    private WebCamTexture backCam;
    private Texture defaultBackground;

    public RawImage background; //顯示相機畫面

    private void Start()
    {
        defaultBackground = background.texture;
        WebCamDevice[] devices = WebCamTexture.devices; //拿到所有可用鏡頭

        if(devices.Length == 0){ //沒任何鏡頭
            Debug.Log("No camera detected");
            isCamAvailable = false;
            return;
        }

        for(int i = 0; i < devices.Length; i++){ //找到哪個是後鏡頭
            if(!devices[i].isFrontFacing){
                backCam = new WebCamTexture(devices[i].name, Screen.width, Screen.height);
            }
        }

        if(backCam == null){
            Debug.Log("Unable to find back camera");
            return;
        }

        backCam.Play(); //啟動相機
        background.texture = backCam;
        isCamAvailable = true;
    }

    public void Update()
    {
        if(!isCamAvailable){
            return;
        }

        //相機翻轉
        float scaleY = backCam.videoVerticallyMirrored? -1f:1f;
        background.rectTransform.localScale = new Vector3(1f, scaleY, 1f);

        int orient = -backCam.videoRotationAngle;
        background.rectTransform.localEulerAngles = new Vector3(0, 0, orient);

    }

    public void StopCamera()
    {
        if (backCam != null)
        {
            backCam.Stop();
            isCamAvailable = false;
        }
    }
}
