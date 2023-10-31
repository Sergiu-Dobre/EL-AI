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
        int numSquares = 1;
        int squareSize = mDivisions;

        for(int i = 0; i < iterations; i++)
        {
            int row = 0;
            for(int j=0; j < numSquares; j++)
            {
                int col = 0;
                for(int k=0; k < numSquares; k++)
                {
                    DiamontSquare(row, col, squareSize, mHeight);
                    col += squareSize;
                }
                row += squareSize;
            }
            numSquares *= 2;
            squareSize /= 2;
            mHeight *= 0.5f;
        }

        mesh.vertices = mVerts;
        mesh.uv = uvs;
        mesh.triangles= triangles;
       

        mesh.RecalculateBounds();  
        mesh.RecalculateNormals();
    }

    void DiamontSquare(int row, int col, int size, float offset)
    {
        int halfSize = (int)(size * 0.5);
        int topLeft = row * (mDivisions + 1) + col;
        int botLeft = (row + size) * (mDivisions + 1) + col;

        int mid = (int)(row + halfSize) * (mDivisions + 1) + (int)(col + halfSize);
        mVerts[mid].y = (mVerts[topLeft].y + mVerts[topLeft + size].y + mVerts[botLeft].y + mVerts[botLeft + size].y)*0.25f+Random.Range(-offset,offset);

        mVerts[topLeft + halfSize].y = (mVerts[topLeft].y + mVerts[topLeft + size].y + mVerts[mid].y)/3+Random.Range(-offset,offset);
        mVerts[mid - halfSize].y = (mVerts[topLeft].y + mVerts[botLeft].y + mVerts[mid].y)/3 +Random.Range(-offset,offset);
        mVerts[mid + halfSize].y = (mVerts[topLeft + size].y + mVerts[botLeft + size].y + mVerts[mid].y)/3 +Random.Range(-offset,offset);
        mVerts[botLeft + halfSize].y = (mVerts[botLeft].y + mVerts[botLeft + size].y + mVerts[mid].y)/3 +  Random.Range(+offset, offset);

    }
}
