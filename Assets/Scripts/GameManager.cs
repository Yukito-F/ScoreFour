using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameObject sphere;
    GameObject piller;

    int turn = 1; // 1:white, -1:black

    int[,,] field = new int[4,4,4]; // 縦,奥,横
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

                int y = 0;
                int x = (int)(temp.x + 1.5f);
                int z = (int)(1.5f - temp.z);

                if (field[3, x, z] == 0)
                {
                    var obj = Instantiate(sphere, temp, Quaternion.identity);
                    if (turn == -1)
                    {
                        obj.GetComponent<Renderer>().material.color = Color.black;
                    }
                    while (field[y, x, z] != 0) y++;

                    field[y, x, z] = turn;
                    if(CheckField(y, x, z))
                    {
                        Debug.Log("Game End");
                    }

                    turn *= -1;
                }

                // --------------------Debug--------------------
                // FieldDebug();
            }
            
        }
    }

    bool CheckField(int y, int x, int z)
    {
        // 直線
        int sumY = 0, sumX = 0, sumZ = 0;
        for (int i = 0; i < 4; i++)
        {
            sumY += field[i, x, z];
            sumX += field[y, i, z];
            sumZ += field[y, x, i];
        }
        if (Mathf.Abs(sumY) == 4 || Mathf.Abs(sumX) == 4 || Mathf.Abs(sumZ) == 4)
        {
            return true;
        }

        // 2次元的ナナメ（y固定）
        int sumRX = 0, sumLX = 0;
        for (int i = 0; i < 4; i++)
        {
            sumRX += field[y, i, i    ];
            sumLX += field[y, i, 3 - i];
        }
        if (Mathf.Abs(sumRX) == 4 || Mathf.Abs(sumLX) == 4)
        {
            return true;
        }

        // 2次元的ナナメ（x固定）
        sumRX = 0; sumLX = 0;
        for (int i = 0; i < 4; i++)
        {
            sumRX += field[i,     x, i];
            sumLX += field[3 - i, x, i];
        }
        if (Mathf.Abs(sumRX) == 4 || Mathf.Abs(sumLX) == 4)
        {
            return true;
        }
        
        // 2次元的ナナメ（z固定）
        sumRX = 0; sumLX = 0;
        for (int i = 0; i < 4; i++)
        {
            sumRX += field[i, i,     z];
            sumLX += field[i, 3 - i, z];
        }
        if (Mathf.Abs(sumRX) == 4 || Mathf.Abs(sumLX) == 4)
        {
            return true;
        }

        // 3次元的ナナメ
        int sumRFX = 0, sumRCX = 0, sumLFX = 0, sumLCX = 0;
        for (int i = 0; i < 4; i++)
        {
            sumRFX += field[i, i,     i];
            sumRCX += field[i, 3 - i, 3 - i];
            sumLFX += field[i, i,     3 - i];
            sumLCX += field[i, 3 - i, i];
        }
        if (Mathf.Abs(sumRFX) == 4 || Mathf.Abs(sumRCX) == 4 || Mathf.Abs(sumLFX) == 4 || Mathf.Abs(sumLCX) == 4)
        {
            return true;
        }

        return false;
    }

    void FieldDebug()
    {
        //string message = "";

        string l1 = "";
        string l2 = "";
        string l3 = "";
        string l4 = "";

        int index = 0;
        foreach (int item in field)
        {

            index++;
        }

        Debug.Log(l1);
        Debug.Log(l2);
        Debug.Log(l3);
        Debug.Log(l4);
    }
}
