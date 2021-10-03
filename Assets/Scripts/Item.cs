using System.Collections;
using System.Linq;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Tile tile;
    public ItemSettingsOld settings;
    public SpriteRenderer spriteRenderer;

    public static readonly float yOffsetFromTile = 0.5f;
    bool dragging;


    public void Init(ItemSettingsOld settings, Tile tile)
    {
        this.settings = settings;
        this.tile = tile;
        tile.item = this;

        spriteRenderer.sprite = Resources.Load<Sprite>(settings.spritePath);
        transform.LookAt(Camera.main.transform); // todo

    }

    private void Start()
    {

    }

    private void OnMouseDown()
    {
        BeginDrag();
    }

    private void OnMouseUp()
    {
        Drop();
    }

    private void FixedUpdate()
    {
        if (dragging)
            Drag();
    }

    private void BeginDrag()
    {
        dragging = true;
    }

    private void Drag()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            var p = hit.point;
            p.y = yOffsetFromTile;
            transform.position = p;
        }
    }

    private void Drop()
    {
        dragging = false;
        SnapToTileBelow();

    }

    private void SnapToTileBelow()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            //transform.position = hit.transform.position + Vector3.up * yOffsetFromTile;
            Grid.instance.OnItemDropOnTile(this, hit.transform);

        }
        else
        {
            MoveToCurrentTile();
        }
    }

    public void MoveToTile(Tile t)
    {
        //transform.position = t.transform.position + Vector3.up * yOffsetFromTile;
        iTween.Stop(gameObject);
        iTween.MoveTo(gameObject, t.transform.position + Vector3.up * yOffsetFromTile, 0.3f);

    }

    public void MoveToCurrentTile()
    {
        //  transform.position = tile.transform.position + Vector3.up * yOffsetFromTile;
        iTween.Stop(gameObject);
        iTween.MoveTo(gameObject, tile.transform.position + Vector3.up * yOffsetFromTile, 0.3f);

        //        Debug.Log("MoveToCurrentTile");
    }

    public void Hide()
    {
        tile.item = null;
        Destroy(gameObject);
    }

    public void ChangeTile(Tile tile)
    {
        this.tile.item = null;
        this.tile = tile;
        this.tile.item = this;
    }

    public void PlayShakeAnimation(float delay)
    {
        iTween.PunchScale(gameObject, transform.localScale * 1.1f, 0.3f);
    }


}
