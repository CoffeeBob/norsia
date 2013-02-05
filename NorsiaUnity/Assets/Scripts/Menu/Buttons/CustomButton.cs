using UnityEngine;
using System.Collections;

public abstract class CustomButton : MonoBehaviour {

    protected tk2dButtonCustom btn;

	protected void Start () {
        btn = GetComponent<tk2dButtonCustom>();
        btn.OnClick += btn_OnClick;
	}

    public abstract void btn_OnClick(tk2dButtonCustom source);
}
