using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class optionsScreen : MonoBehaviour
{
    public Toggle fullscreenT;
    public Toggle vsyncT;

    public List<ResolutionItem> resolutions = new List<ResolutionItem>();

    // Start is called before the first frame update
    void Start()
    {
        fullscreenT.isOn = Screen.fullScreen; 

        if(QualitySettings.vSyncCount == 0){
            vsyncT.isOn = false;
        }
        else{
            vsyncT.isOn = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyGraphics(){

        Screen.fullScreen = fullscreenT.isOn;

        if(vsyncT.isOn){
            QualitySettings.vSyncCount = 1;
        }
        else{
            QualitySettings.vSyncCount = 0;
        }

    }
}


[System.Serializable]
public class ResolutionItem{

    public int horizontal;
    public int vertical;
}
