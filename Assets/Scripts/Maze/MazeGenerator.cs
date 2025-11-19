using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MazeGenerator : MonoBehaviour {

    [SerializeField] private MazeCell flowerCell;
    [SerializeField] private MazeCell oceanCell;
    [SerializeField] private MazeCell farmCell;
        
    [SerializeField] private MazeTypeEnum MazeType = MazeTypeEnum.FLOWER;
    
    private MazeCell cellUse;

    [SerializeField] private int mazeWidth; 
    [SerializeField] private int mazeDepth; 
    private MazeCell[,] mazeGrid;

    [SerializeField] private Vector2Int startCell;
    [SerializeField] private Vector2Int endCell;

    private void Awake() {
        if(MazeType == MazeTypeEnum.FLOWER) {
            cellUse = flowerCell;
        } else if(MazeType == MazeTypeEnum.OCEAN) {
            cellUse = oceanCell;
        } else if(MazeType == MazeTypeEnum.FARM) {
            cellUse = farmCell;
        }
    }

    void Start() {
        mazeGrid = new MazeCell[mazeWidth, mazeDepth];

        for(int i = 0; i < mazeWidth; i++) {
            for(int j = 0; j < mazeDepth; j++) {
                mazeGrid[i, j] = Instantiate(cellUse, new Vector3(i*4, 0, j*4), Quaternion.identity);
            }
        }

        GenerateMaze(null, mazeGrid[startCell.x, startCell.y]);
        mazeGrid[endCell.x, endCell.y].SetAsExit();
    }

    private void GenerateMaze(MazeCell previousCell, MazeCell currentCell)
    {
        currentCell.Visit();
        ClearWalls(previousCell, currentCell);

        MazeCell nextCell;

        do
        {
            nextCell = GetNextUnvisitedCell(currentCell);

            if (nextCell != null)
            {
                GenerateMaze(currentCell, nextCell);
            }
        } while (nextCell != null);
    }

    private MazeCell GetNextUnvisitedCell(MazeCell currentCell)
    {
        var unvisitedCells = GetUnvisitedCells(currentCell);

        return unvisitedCells.OrderBy(_ => Random.Range(1, 10)).FirstOrDefault();
    }

    private IEnumerable<MazeCell> GetUnvisitedCells(MazeCell currentCell)
    {
        int x = (int)currentCell.transform.position.x/4;
        int z = (int)currentCell.transform.position.z/4;

        if (x + 1 < mazeWidth)
        {
            var cellToRight = mazeGrid[x + 1, z];
            
            if (cellToRight.IsVisited == false)
            {
                yield return cellToRight;
            }
        }

        if (x - 1 >= 0)
        {
            var cellToLeft = mazeGrid[x - 1, z];

            if (cellToLeft.IsVisited == false)
            {
                yield return cellToLeft;
            }
        }

        if (z + 1 < mazeDepth)
        {
            var cellToFront = mazeGrid[x, z + 1];

            if (cellToFront.IsVisited == false)
            {
                yield return cellToFront;
            }
        }

        if (z - 1 >= 0)
        {
            var cellToBack = mazeGrid[x, z - 1];

            if (cellToBack.IsVisited == false)
            {
                yield return cellToBack;
            }
        }
    }

    private void ClearWalls(MazeCell previousCell, MazeCell currentCell)
    {
        if (previousCell == null)
        {
            return;
        }

        if (previousCell.transform.position.x < currentCell.transform.position.x)
        {
            previousCell.ClearRightWall();
            currentCell.ClearLeftWall();
            return;
        }

        if (previousCell.transform.position.x > currentCell.transform.position.x)
        {
            previousCell.ClearLeftWall();
            currentCell.ClearRightWall();
            return;
        }

        if (previousCell.transform.position.z < currentCell.transform.position.z)
        {
            previousCell.ClearFrontWall();
            currentCell.ClearBackWall();
            return;
        }

        if (previousCell.transform.position.z > currentCell.transform.position.z)
        {
            previousCell.ClearBackWall();
            currentCell.ClearFrontWall();
            return;
        }
    }

}
