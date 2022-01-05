using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform fishType;

    private float topX;
    private float topY;
    private float bottomX;
    private float bottomY;
    void Start()
    {
        topX = transform.position.x - (transform.localScale.x/2);//Top left X
        topY = transform.position.y + (transform.localScale.y/2);//Top left Y
        bottomX = transform.position.x + (transform.localScale.x/2);//Bottom right X
        bottomY = transform.position.y - (transform.localScale.y/2);//Bottom right Y

        StartCoroutine(RandomSpawn(0.5f));
    }

    // void Update()
    // {
    //     Instantiate(fishType, RandomVec2D(topX, topY, bottomX, bottomY), Quaternion.identity);
    // }

    private Vector2 RandomVec2D(float topX, float topY, float bottomX, float bottomY)
    {
        Vector2 vector2D = new Vector2(Random.Range(topX, bottomX), Random.Range(topY, bottomY));
        return vector2D;
    }


    private IEnumerator RandomSpawn(float time) 
    {
        while(true)
        {
            yield return new WaitForSeconds(time);
            if (randomBoolean()){
               Instantiate(fishType, RandomVec2D(topX, topY, bottomX, bottomY), Quaternion.identity);
            }
        }
    }

    private bool randomBoolean()
    {
        return (Random.value > 0.5f);
    }
}