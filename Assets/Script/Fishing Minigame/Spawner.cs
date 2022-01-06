using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform goodFish;
    public Transform badFish;

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

    private IEnumerator RandomSpawn(float time) 
    {
        while(true)
        {
            yield return new WaitForSeconds(time);

            if (randomBoolean(0.4f)){//Good fish
                Transform tempFish = Instantiate(goodFish, RandomVec2D(topX, topY, bottomX, bottomY), Quaternion.identity);
                tempFish.transform.localScale = RandomScale2D(1f, 3f);
            }

            if (randomBoolean(0.2f)){//Bad fish
                Transform tempFish = Instantiate(badFish, RandomVec2D(topX, topY, bottomX, bottomY), Quaternion.identity);
                tempFish.transform.localScale = RandomScale2D(2f, 3f);
            }
        }
    }

    private Vector2 RandomVec2D(float topX, float topY, float bottomX, float bottomY) //Random location
    {
        Vector2 vector2D = new Vector2(Random.Range(topX, bottomX), Random.Range(topY, bottomY));
        return vector2D;
    }

    private Vector2 RandomScale2D(float min, float max)//Values for scale
    {
        float scale = Random.Range(min, max);
        Vector2 scaler2D = new Vector2(-scale, scale);
        return scaler2D;
    }

    private bool randomBoolean(float x) //The larger the value the more likely 0.5f is 50%
    {
        return (Random.value < x);
    }
}