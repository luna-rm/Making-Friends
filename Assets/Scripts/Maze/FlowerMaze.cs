using UnityEngine;

public class FlowerMaze : MazeCell {

    [SerializeField] private GameObject leftWall;
    [SerializeField] private GameObject rightWall;
    [SerializeField] private GameObject frontWall;
    [SerializeField] private GameObject backWall;

    [SerializeField] private GameObject floor;
    [SerializeField] private GameObject exit;

    public override void Visit() {
        base.Visit();
        floor.SetActive(true);
    }

    public override void ClearLeftWall() {
        leftWall.SetActive(false);
    }

    public override void ClearRightWall() {
        rightWall.SetActive(false);
    }

    public override void ClearFrontWall() {
        frontWall.SetActive(false);
    }

    public override void ClearBackWall() {
        backWall.SetActive(false);
    }

    public override void SetAsExit() {
        base.SetAsExit(); 
        floor.SetActive(false);
        exit.SetActive(true);
    }

    public override void NotSetAsExit() {
        base.SetAsExit(); 
        floor.SetActive(true);
        exit.SetActive(false);
    }
}
