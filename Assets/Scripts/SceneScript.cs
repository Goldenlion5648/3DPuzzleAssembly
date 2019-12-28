using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform prefab;
    void Start()
    {
        int cubeDim = 3;
        Vector3 startPos = new Vector3(0, 1.5f, 0);

        float sideLength = 15;

        float individualSideLength = sideLength / cubeDim + 2;

        float curXPos = 0;
        float curYPos = 1.5f;
        float curZPos = 0;
        for (int i = 0; i < (int)Mathf.Pow(cubeDim, 3); i++)
        {
            if (i % 3 == 0)
            {
                curXPos += individualSideLength;
                curZPos = startPos.z;

            }
            if (i % 9 == 0)
            {
                curYPos += individualSideLength;
                curXPos = startPos.x;
            }
            curZPos += individualSideLength;
            Instantiate(prefab, new Vector3(curXPos, curYPos, curZPos), Quaternion.identity);

            Debug.Log("made new Cube");
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
