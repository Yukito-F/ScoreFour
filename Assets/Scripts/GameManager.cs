using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameObject sphere;
    GameObject piller;

    int[,,] field = new int[4,4,4]; // èc,âú,â°
    void Start()
    {
        sphere = Resources.Load("Sphere") as GameObject;
        // sphere = (GameObject)Resources.Load("Sphere");

        piller = Resources.Load("Piller") as GameObject;

        for(int i = 0; i < 4; i++)
        {
            for(int j = 0; j < 4; j++)
            {
                Vector3 temp = new Vector3(1.5f - i, 2, -1.5f + j);
                GameObject obj = Instantiate(piller, temp, Quaternion.identity);
                obj.transform.SetParent(GameObject.Find("Board").transform);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 temp = hit.transform.position;
                temp.y = 10;
                Instantiate(sphere, temp, Quaternion.identity);


                // --------------------Debug--------------------
                FieldDebug();
            }
            
        }
    }

    void FieldDebug()
    {
        int index = 0;
        string message = "";
        foreach (int item in field)
        {
            if (index % 16 == 0)
            {
                Debug.Log("Layer " + index / 12);
            }
            message += item;

            if (index % 16 < 15)
            {
                message += ",";
            }
            else
            {
                Debug.Log(message);
                message = "";
            }
            index++;
        }
    }
}
