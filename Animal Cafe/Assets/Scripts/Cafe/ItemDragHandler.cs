using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDragHandler : MonoBehaviour, IBeginDragHandler,IDragHandler, IEndDragHandler
{
    public GameObject selectedItem;
    public SelectedItemCollision selectedItemCollision;

    public RectTransform rect;
    public Sprite sprite;

    public Vector2 vec;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
        sprite = GetComponent<Image>().sprite;
        selectedItemCollision = selectedItem.GetComponent<SelectedItemCollision>();
    }

    private void Update()
    {


        //RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, Input.mousePosition, CameraChanger.currentCam, out vec);
        //if (rect.rect.Contains(vec))
        //{
        //    Debug.Log("containsss");
        //}

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        selectedItem.transform.position = transform.position;
        selectedItem.GetComponent<Image>().sprite = sprite;
        ChangeAlpha(selectedItem, 255);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 pos = Input.mousePosition;
        pos.z = 100;
        // screen 상 마우스 좌표를 world 좌표로 변환해줌. z는 distance from camera(여기선 canvas가 camera에서 100떨어져있으므로..)
        // canvas(camera)가 여러개이므로 현재 camera에 맞추기 위해 currentCam 씀.
        Vector3 worldPoint = CameraChanger.currentCam.ScreenToWorldPoint(pos);
        selectedItem.transform.position = worldPoint;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(sprite.name == "tissue")
        {
            // tissue를 선택하여 poo에 갖다놓으면 poo 사라지고 selectedItem은 안보이도록.
            if (selectedItemCollision.collidePoo)
            {
                Destroy(selectedItemCollision.collidedObject);
            }
            ChangeAlpha(selectedItem, 0);
        }
    }


    public void ChangeAlpha(GameObject obj, float alpha)
    {
        Color temp = obj.GetComponent<Image>().color;
        temp.a = alpha;
        obj.GetComponent<Image>().color = temp;
    }


}
