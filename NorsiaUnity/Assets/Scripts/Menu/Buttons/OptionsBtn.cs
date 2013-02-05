using UnityEngine;
using System.Collections;

public class OptionsBtn : CustomButton
{
    public override void btn_OnClick(tk2dButtonCustom source)
    {
        MenuHandler.SwitchMenu(Menu.MenuTypes.Options);
    }
}
