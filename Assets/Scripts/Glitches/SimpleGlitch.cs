using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SimpleGlitch : MonoBehaviour
{
    public Tilemap tilemap;                         
    /*public bool useBounds = false;                  
    public Vector3Int boundsMin = Vector3Int.zero;  
    public Vector3Int boundsMax = Vector3Int.one; */ 

    public List<Vector3Int> targetCells = new List<Vector3Int>();

    public float glitchChance = 0.05f;
    public float glitchOffset = 0.1f;   
    public float glitchTime = 0.05f;
    public bool startGlitch = false;

    TilemapCollider2D tilemapCollider;

    void Start()
    {
        if (tilemap == null) tilemap = GetComponent<Tilemap>();
        tilemapCollider = tilemap.GetComponent<TilemapCollider2D>();
    }

    void Update()
    {
        
        if (startGlitch && Random.value < glitchChance)
        {
            StartCoroutine(DoGlitch());
        }
    }

    IEnumerator DoGlitch()
    {
        
        Vector3Int cell = PickRandomCell();
        if (cell == null) yield break;

        TileBase tb = tilemap.GetTile(cell);
        if (tb == null)
            yield break; 

        Matrix4x4 originalMatrix = tilemap.GetTransformMatrix(cell);
        Color originalColor = tilemap.GetColor(cell);

       
        if (tilemapCollider != null)
            tilemapCollider.enabled = false;


        float offsetX = Random.Range(-glitchOffset, glitchOffset);
        Vector3 translation = new Vector3(offsetX, 0f, 0f);

        Matrix4x4 glitchMatrix = Matrix4x4.TRS(translation, Quaternion.identity, Vector3.one);
        tilemap.SetTransformMatrix(cell, glitchMatrix);

        Color glitchColor = new Color(Random.value, Random.value, Random.value, originalColor.a);
        tilemap.SetColor(cell, glitchColor);

        yield return new WaitForSeconds(glitchTime);

        tilemap.SetTransformMatrix(cell, originalMatrix);
        tilemap.SetColor(cell, originalColor);

        if (tilemapCollider != null)
            tilemapCollider.enabled = true;
    }

    Vector3Int PickRandomCell()
    {

        /*if (useBounds)
        {
            List<Vector3Int> cellsInBounds = new List<Vector3Int>();
            for (int x = boundsMin.x; x <= boundsMax.x; x++)
                for (int y = boundsMin.y; y <= boundsMax.y; y++)
                    for (int z = boundsMin.z; z <= boundsMax.z; z++)
                    {
                        Vector3Int c = new Vector3Int(x, y, z);
                        if (tilemap.HasTile(c)) cellsInBounds.Add(c);
                    }

            if (cellsInBounds.Count == 0) return default;
            return cellsInBounds[Random.Range(0, cellsInBounds.Count)];
        }*/ //didn't work
        if (true)
        {
            // Select randomly from the targetCells list (if there is nothing in the list, select a random tile from the tilemap)
            if (targetCells != null && targetCells.Count > 0)
            {
                return targetCells[Random.Range(0, targetCells.Count)];
            }
            else
            {
                // fallback: randomly select a filled cell from the tilemap's cellBounds
                var bounds = tilemap.cellBounds;
                List<Vector3Int> filled = new List<Vector3Int>();
                for (int x = bounds.xMin; x <= bounds.xMax; x++)
                {
                    for (int y = bounds.yMin; y <= bounds.yMax; y++)
                    {
                        Vector3Int c = new Vector3Int(x, y, 0);
                        if (tilemap.HasTile(c)) filled.Add(c);
                    }
                }
                if (filled.Count == 0) return default;
                return filled[Random.Range(0, filled.Count)];
            }
        }
    }
}
