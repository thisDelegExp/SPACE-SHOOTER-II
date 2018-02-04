using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public int fieldHeight;
    public int fieldWidth;
    public int fieldDepth;
    public int fieldMaxInterval;

    public Vector3 offset;// relative to world zero

    public GameObject[] fieldPrefabs;
    public Vector2 scaleFactor; //x - min scale , y - max scale
    public float maxAsteroidSpeed;

    
	
	void Start ()
    {
        GenerateField();
	}
	
    void GenerateField()
    {
        Vector3[] spawns = CalculateSpawns(fieldWidth, fieldHeight, fieldDepth, fieldMaxInterval);
        foreach(Vector3 spawn in spawns)
        {
            
            GameObject asteroid= Instantiate(fieldPrefabs[Random.Range(0, fieldPrefabs.Length - 1)], spawn+offset, Random.rotation);
            asteroid.transform.localScale = GetScaleVector(scaleFactor);
            asteroid.GetComponent<Rigidbody>().AddForce(Random.insideUnitSphere * Random.Range(1, maxAsteroidSpeed));

        }
    }
    
    Vector3[] CalculateSpawns(int width,int height , int depth, int density)
    {
        List<Vector3> spawns = new List<Vector3>();
        for(float i=0; i<width; i+= Random.Range(1, density))
        {
            for(float j=0; j<height; j += Random.Range(1, density))
            {
                spawns.Add(new Vector3(i, j, CalculateZ(width,height)));
            }
        }
        return spawns.ToArray();
    }

    float CalculateZ(int x, int y)
    {
        float xCoord = x / fieldWidth;
        float yCoord = y / fieldHeight;

        return Mathf.PerlinNoise(xCoord, yCoord)*Random.Range(1,fieldDepth);
    }

	public Vector3 GetScaleVector(Vector2 scale)
    {
        float randFactor = Random.Range(scale.x, scale.y);
        return new Vector3(Random.Range(1,2), Random.Range(1, 2) , Random.Range(1,2) )* randFactor;
    }
}


