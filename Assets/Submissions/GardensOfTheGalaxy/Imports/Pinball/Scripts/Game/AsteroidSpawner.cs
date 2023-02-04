using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public AudioSource tapSoundEffect;

    public GameObject targetObject;
    public float a;
    public float b;
    public float speed;

    public EllipticalOrbit[] asteroids;

    private int currIndex = 0;

    private void Start()
    {
        for (int alpha = 0; alpha < 72000; alpha += 300)
        {
            EllipticalOrbit nextAsteroid = NextAsteroid();
            nextAsteroid.Initialize(a, b, speed, alpha, targetObject);
            nextAsteroid.gameObject.GetComponent<RandomRotator>().tapSoundEffect = tapSoundEffect;
        }
        this.gameObject.SetActive(false);
    }
    private EllipticalOrbit NextAsteroid()
    {
        EllipticalOrbit result = (EllipticalOrbit) Instantiate(asteroids[currIndex], transform);
        if (currIndex >= asteroids.Length - 1)
        {
            currIndex = 0;
        }
        else
        {
            currIndex++;
        }
        return result;
    }
}
