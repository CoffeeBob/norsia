using UnityEngine;
using System.Collections;

public class Resolutions : MonoBehaviour {

    public GameObject button;
    public float heightMargin;
    public float widthMargin;

    private GameObject[] buttons;
    private GameObject fullscreenBtn;
    private static Resolutions self;
    private static bool isFullScreen;
    private Resolution[] resolutions;
    private Vector3 currentOffset;

	// Use this for initialization
	void Start () {
        self = this;
        resolutions = Screen.resolutions;
        isFullScreen = Screen.fullScreen;
        buttons = new GameObject[resolutions.Length];

        CreateButtons();
        PositionButtons();
    }
	
	// Update is called once per frame
	void Update () {
        PositionButtons();
	}

    private void CreateButtons()
    {
        currentOffset = Vector3.zero;
        if (!button)
            return;

        GameObject GO;

        // Create resolution buttons
        for (int i = 0; i < resolutions.Length; i++)
        {
            string resolution = resolutions[i].width + "x" + resolutions[i].height;

            GO = Instantiate(button) as GameObject;
            GO.transform.parent = transform;
            GO.transform.localPosition = Vector3.zero;
            GO.transform.position += Vector3.zero;

            GO.GetComponentInChildren<tk2dTextMesh>().text = resolution;
            GO.GetComponentInChildren<tk2dTextMesh>().anchor = TextAnchor.MiddleCenter;
            GO.GetComponentInChildren<tk2dTextMesh>().Commit();

            GO.AddComponent<ResolutionBtn>();
            GO.GetComponent<ResolutionBtn>().resolution = resolutions[i];

            buttons[i] = GO;
        }

        // Create fullscreen button
        GO = Instantiate(button) as GameObject;
        GO.transform.parent = transform;
        GO.transform.localPosition = Vector3.zero;
        GO.transform.position += Vector3.zero;

        GO.GetComponentInChildren<tk2dTextMesh>().text = "Toggle fullscreen";
        GO.GetComponentInChildren<tk2dTextMesh>().anchor = TextAnchor.MiddleCenter;
        GO.GetComponentInChildren<tk2dTextMesh>().Commit();
        GO.AddComponent<FullscreenBtn>();

        fullscreenBtn = GO;
    }
    private void DeleteButtons()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
    private void PositionButtons()
    {
        // Reset offset
        currentOffset = Vector3.zero;

        // Define button height and width
        GameObject GO;
        GO = Instantiate(button) as GameObject;
        float btnHeight = GO.GetComponent<tk2dButtonCustom>().Height;
        float btnWidth = GO.GetComponent<tk2dButtonCustom>().Width;
        Destroy(GO);

        float screenHeight = Screen.height - (transform.localPosition.y * 2);

        // Loop buttons
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].transform.localPosition = currentOffset;

            currentOffset += Vector3.down * (btnHeight + heightMargin);

            if (((currentOffset.y * -1) + (btnHeight * 4)) > screenHeight)
            {
                currentOffset += Vector3.right * (btnWidth + widthMargin);
                currentOffset.y = 0;
            }
        }

        fullscreenBtn.transform.localPosition = new Vector3(currentOffset.x + btnWidth + widthMargin, 0, 0);
    }

    public static void SetResolution(Resolution resolution)
    {
        Screen.SetResolution(resolution.width, resolution.height, isFullScreen);
    }

    public static void SetFullscreen(bool fullscreen)
    {
        Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, fullscreen);
    }

    public static void ToggleFullscreen()
    {
        Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, !Screen.fullScreen);
    }
}
