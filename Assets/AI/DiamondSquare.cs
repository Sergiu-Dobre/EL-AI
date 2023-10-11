using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondSquare : MonoBehaviour
{
    public int mDivisions; //number of divisions 
    public float mSize;
    public float mHeight;

    Vector3[] mVerts;
    int mVertCont;

    // Start is called before the first frame update
    void Start()
    {
    CreateTerrain();
    }

    
    void CreateTerrain()
    {
     mVertCont= (mDivisions+1)*(mDivisions+1);
     mVerts= new Vector3[mVertCont];
        Vector2[] uvs = new Vector2[mVertCont];
        int[]triangles= new int[mDivisions*mDivisions*6];
        
        float halfSize = mSize*0.5f;
        float divisionSize = mSize / mDivisions;

        Mesh mesh= new Mesh();  
        GetComponent<MeshFilter>().mesh = mesh;

        int triangleOffSet=0;

        for (int i = 0; i <= mDivisions; i++)
        {
            for(int j=0; j <= mDivisions; j++)
            {
                mVerts[i * (mDivisions + 1) + j] = new Vector3(-halfSize + j * divisionSize, 0.0f, halfSize - i * divisionSize);
                uvs[i*(mDivisions+1)+j]= new Vector2((float)i/mDivisions, (float)j / mDivisions);

                if(i <mDivisions && j < mDivisions)
                {
                    int topLeft = i * (mDivisions + 1) + j;
                    int bottomLeft = (i + 1) * (mDivisions + 1) + j;

                    triangles[triangleOffSet] = topLeft;
                    triangles[triangleOffSet + 1] = topLeft+1;
                    triangles[triangleOffSet + 2] = bottomLeft+1;

                    triangles[triangleOffSet + 3] = topLeft;
                    triangles[triangleOffSet+4]=bottomLeft+1;
                    triangles[triangleOffSet+5]=bottomLeft;

                    triangleOffSet += 6;

                }
            }
        }

        mVerts[0].y = Random.Range(-mHeight, mHeight);
        mVerts[mDivisions].y= Random.Range(-mHeight, mHeight);
        mVerts[mVerts.Length-1].y=Random.Range(-mHeight, mHeight);
        mVerts[mVerts.Length-mDivisions].y=Random.Range(-mHeight, mHeight);

        int iterations = (int)Mathf.Log(mDivisions, 2);

        mesh.vertices = mVerts;
        mesh.uv = uvs;
        mesh.triangles= triangles;
       

        mesh.RecalculateBounds();  
        mesh.RecalculateNormals();
    }
}
