using UnityEngine;
using System.Collections;

public class ExitBtn : CustomButton {
    public override void btn_OnClick(tk2dButtonCustom source)
    {
        Application.Quit();
    }
}
