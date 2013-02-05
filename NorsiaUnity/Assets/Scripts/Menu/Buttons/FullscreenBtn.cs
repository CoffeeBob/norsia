using UnityEngine;
using System.Collections;

public class FullscreenBtn : CustomButton {
    
    public override void btn_OnClick(tk2dButtonCustom source)
    {
        Resolutions.ToggleFullscreen();
    }
}
