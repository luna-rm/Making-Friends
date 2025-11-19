using UnityEngine;

public class FlowerMaze : MonoBehaviour {

    [SerializeField] private GameObject leftWall;
    [SerializeField] private GameObject rightWall;
    [SerializeField] private GameObject frontWall;
    [SerializeField] private GameObject backWall;

    [SerializeField] private GameObject floor;

    private GameObject unvisitedBlock;

    public bool isVisited {  get; private set; } = false;

    public void Visit() {
        isVisited = true;
        unvisitedBlock.SetActive(false);
    }

    public void ClearLeftWall() {
        leftWall.SetActive(false);
    }

    public void ClearRightWall() {
        rightWall.SetActive(false);
    }

    public void ClearFrontWall() {
        frontWall.SetActive(false);
    }

    public void ClearBackWall() {
        backWall.SetActive(false);
    }

}
