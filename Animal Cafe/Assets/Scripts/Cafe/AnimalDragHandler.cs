using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AnimalDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public CafeUIManager cafeUIManager;

    public GameObject[] restWalls;
    public GameObject[] cafeWalls;

    private bool goToRest = false;
    private bool goToCafe = false; 

    // UI Element가 아닌 일반 object에 대해서 drag하기위해서는 camera에 physics 2d raycast 추가해야함.
    public void OnDrag(PointerEventData eventData)
    {
        Vector3 pos = Input.mousePosition;
        pos.z = 100;
        // screen 상 마우스 좌표를 world 좌표로 변환해줌. z는 distance from camera(여기선 canvas가 camera에서 100떨어져있으므로..)
        // canvas(camera)가 여러개이므로 현재 camera에 맞추기 위해 currentCam 씀.
        Vector3 worldPoint = CameraChanger.currentCam.ScreenToWorldPoint(pos);
        transform.position = worldPoint;



        //Vector2 pos;
        //Canvas parentCanvasOfImageToMove = Canvas.
        //RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvasOfImageToMove.transform as RectTransform, data.position, parentCanvasOfImageToMove.worldCamera, out pos);
        //imageToMove.transform.position = parentCanvasOfImageToMove.transform.TransformPoint(pos);

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(goToRest)
        {
            goToRest = false;
            cafeUIManager.RandomPositioningInsideWalls(gameObject, restWalls, "RestCanvas");
            GetComponent<AnimalButton>().rest = true;
        }
        if (goToCafe)
        {
            goToCafe = false;
            cafeUIManager.RandomPositioningInsideWalls(gameObject, cafeWalls, "Canvas");
            GetComponent<AnimalButton>().rest = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "RestingRoom")
        {
            goToRest = true;
        }
        if(collision.gameObject.name == "Cafe")
        {
            goToCafe = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "restingroom")
        {
            goToRest = false;
        }
        if (collision.gameObject.name == "Cafe")
        {
            goToCafe = false;
        }
    }


    void Start()
    {
        cafeUIManager = FindObjectOfType<CafeUIManager>();
        cafeWalls = cafeUIManager.cafeWalls;
        restWalls = cafeUIManager.restWalls;
    }
}
