using UnityEngine;
using System.Collections;

[RequireComponent(typeof(tk2dSprite))]
public class tk2dButtonCustom : MonoBehaviour
{
    #region Fields
    public Camera viewCamera;
    public string buttonNormal = "btn_normal";
    public string buttonHover = "btn_hover";

    private tk2dSprite sprite;
    private int buttonNormalSpriteId = -1, buttonHoverSpriteId = -1;

    public delegate void ButtonHandlerDelegate(tk2dButtonCustom source);
    public event ButtonHandlerDelegate OnClick;

    public float Width { get { return GetComponent<tk2dSprite>().GetBounds().size.x; } }
    public float Height { get { return GetComponent<tk2dSprite>().GetBounds().size.y; } }
    #endregion

    // Use this for initialization
	void Start () {
        FindCamera();
        sprite = GetComponent<tk2dSprite>();
        UpdateSpriteIds();
	}
	
	// Update is called once per frame
	void Update () {
        // If alpha of the sprite is less than 1 it's in transition, so don't allow clicks
        if (sprite.color.a < 1)
        {
            sprite.spriteId = buttonNormalSpriteId;
            return;
        }

        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        bool colHit = collider.Raycast(ray, out hitInfo, 1.0e8f);

        if (colHit)
        {
            // Check for raycast hit, change sprite and fire event if clicking
            sprite.spriteId = buttonHoverSpriteId;
            if (Input.GetMouseButtonDown(0))
            {
                if (OnClick != null)
                    OnClick(this);
            }
        }
        else
        {
            // Default sprite, to revert from hovering
            sprite.spriteId = buttonNormalSpriteId;
        }
	}

    public void UpdateSpriteIds()
    {
        buttonNormalSpriteId = (buttonNormal.Length > 0) ? sprite.GetSpriteIdByName(buttonNormal) : -1;
        buttonHoverSpriteId = (buttonHover.Length > 0) ? sprite.GetSpriteIdByName(buttonHover) : -1;
    }

    private void FindCamera()
    {
        if (viewCamera == null)
        {
            viewCamera = tk2dCamera.inst.mainCamera;
        }

        if (viewCamera == null)
        {
            viewCamera = Camera.main;
        }
    }
}
