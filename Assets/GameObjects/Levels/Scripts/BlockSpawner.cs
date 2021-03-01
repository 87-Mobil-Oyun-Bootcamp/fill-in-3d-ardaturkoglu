using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    Vector3 blockPos = Vector3.zero;
    int totalSize = 0;

    public List<GameObject> CreateBlockFromImage(LevelInfo levelInfo, Transform transform)
    {
        List<GameObject> createdCubes = new List<GameObject>();

        for (int x = 0; x < levelInfo.sprite.texture.width; x++)
        {
            for (int y = 0; y < levelInfo.sprite.texture.height; y++)
            {
                Color color = levelInfo.sprite.texture.GetPixel(x, y);

                if (color.a == 0)
                {
                    continue;
                }
                totalSize += 1;
                blockPos = new Vector3(
                    levelInfo.size * (x - (levelInfo.sprite.texture.width * .5f)),
                    levelInfo.size * .01f,
                    levelInfo.size * (y - (levelInfo.sprite.texture.height * .5f)));

                GameObject cubeObj = Instantiate(levelInfo.baseObj, transform);
                cubeObj.transform.localPosition = blockPos;

                cubeObj.GetComponent<Renderer>().material.color = Color.gray;
                cubeObj.transform.localScale = Vector3.one * levelInfo.size;

                createdCubes.Add(cubeObj);
            }
        }

        return createdCubes;
    }

    public List<GameObject> CreateFillBlock(LevelInfo levelInfo, Transform transform, Transform start)
    {
        List<GameObject> fillCubes = new List<GameObject>();
        int count = 0;
        Debug.Log("Total Block No : " + totalSize);
        for (int x = 0; x < totalSize; x++)
        {
            for (int y = 0; y < 10 ; y++)
            {
                if (count == totalSize)
                    break;
                blockPos = new Vector3(
                levelInfo.size *x,
                levelInfo.size * .5f,
                levelInfo.size * y);
                count += 1;

                GameObject cubeObj = Instantiate(levelInfo.fillObj, start);
                cubeObj.transform.localPosition = blockPos;

                cubeObj.GetComponent<Renderer>().material.color = Color.blue;
                cubeObj.transform.localScale = Vector3.one * levelInfo.size;

                fillCubes.Add(cubeObj);
            } 
        }

        return fillCubes;
    }
}
