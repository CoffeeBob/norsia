using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuHandler : MonoBehaviour {

    private static Dictionary<Menu.MenuTypes, Menu> subMenus;
    private static Menu.MenuTypes currentMenuType;
    private static Menu.MenuTypes lastMenuType;

    public Menu.MenuTypes CurrentMenuType { get { return currentMenuType; } }
    public static Menu.MenuTypes LastMenuType { get { return lastMenuType; } }

	// Use this for initialization
	void Start () {
        subMenus = new Dictionary<Menu.MenuTypes, Menu>();

        Menu[] subs = GetComponentsInChildren<Menu>();
        for (int i = 0; i < subs.Length; i++)
        {
            Menu.MenuTypes type = subs[i].MenuType;
            if (subMenus.ContainsKey(type))
                continue;

            subs[i].InstantHide();
            subMenus.Add(type, subs[i]);
        }

        if (subMenus.ContainsKey(Menu.MenuTypes.Main))
        {
            SwitchMenu(currentMenuType);
        }
	}

    // Update is called once per frame
    void Update()
    {
	
	}

    public static void SwitchMenu(Menu.MenuTypes type)
    {
        Menu menu;
        if (type != currentMenuType && subMenus.TryGetValue(currentMenuType, out menu))
        {
            menu.Hide();
        }

        if (subMenus.TryGetValue(type, out menu))
        {
            menu.Show();
        }

        lastMenuType = currentMenuType;
        currentMenuType = type;
    }
}
