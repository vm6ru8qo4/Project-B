using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimitiveGenerator : MonoBehaviour
{
    [SerializeField] GameObject m_preCube;
    [SerializeField] GameObject m_preSphere;
    [SerializeField] Vector2 m_dimension = new Vector2(5, 5);
    // Start is called before the first frame update
    void Start()
    {
        Generate(m_preSphere, Random.Range(5, 10));
        Generate(m_preCube, Random.Range(5, 10));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Generate(GameObject primitive,int count)
    {
        for(int i = 0; i < count; i++)
        {
            var primitiveIns = GameObject.Instantiate(primitive,
                new Vector3(Random.Range(-m_dimension.x,m_dimension.x),3f,Random.Range(-m_dimension.y,m_dimension.y)),Quaternion.identity);
        }
    }
}
