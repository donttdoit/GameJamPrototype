using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDetection : MonoBehaviour
{
    public static List<GameObject> s_gameObjects = new List<GameObject>();
    public static float s_offsetX = 15f;
    private Camera camera;

    private const string _TAG_FAIRY = "Fairy";
    private const string _TAG_GNOME = "Gnome";
    private const string _TAG_GOBLIN = "Goblin";
    private const string _TAG_POINT = "Point";

    

    private void Start()
    {
        camera = Camera.main;
    }

    private void Update()
    {
        DetectObjectWithRaycast();     

        if (s_gameObjects.Count == 2)
        {
            GoToPoint();
        }
    }

    public void DetectObjectWithRaycast()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                //Debug.Log("Target Position: " + hit.collider.gameObject.transform.position);
                // 0 объект = юнит, 1 объект = точка
                if (s_gameObjects.Count == 0 && 
                    (hit.collider.gameObject.CompareTag(_TAG_FAIRY) || hit.collider.gameObject.CompareTag(_TAG_GNOME) || hit.collider.gameObject.CompareTag(_TAG_GOBLIN)) || 
                    s_gameObjects.Count == 1 && hit.collider.gameObject.CompareTag(_TAG_POINT))
                {
                    s_gameObjects.Add(hit.collider.gameObject);
                    Debug.Log(hit.collider.gameObject.name);
                }
            }
        }
    }

    private void GoToPoint()
    {
        Unit unit = s_gameObjects[0].GetComponent<Unit>();
        s_gameObjects[0].transform.position = Vector3.MoveTowards(s_gameObjects[0].transform.position, s_gameObjects[1].transform.position - new Vector3(s_offsetX, 0, 0), unit.GetSpeed() * Time.deltaTime); ;
    }
}

