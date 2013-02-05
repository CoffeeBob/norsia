using UnityEngine;
using System.Collections;

public class ResolutionBtn : CustomButton {

    public Resolution resolution;

    public override void btn_OnClick(tk2dButtonCustom source)
    {
        Resolutions.SetResolution(resolution);
    }
}
