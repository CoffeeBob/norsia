using UnityEngine;
using System.Collections;

public class BackBtn : CustomButton {

    public override void btn_OnClick(tk2dButtonCustom source)
    {
        MenuHandler.SwitchMenu(MenuHandler.LastMenuType);
    }
}
