using System.Collections.Generic;
using UnityEngine;

public class DepthMaze : MazeCell {

    [SerializeField] private GameObject leftWall;
    [SerializeField] private GameObject rightWall;
    [SerializeField] private GameObject frontWall;
    [SerializeField] private GameObject backWall;

    [SerializeField] private GameObject floor;

    [SerializeField] private GameObject protection;

    private static int howManySinceLast = 5;
    [SerializeField] int howMany = 5;

    public override void Visit() {
        base.Visit();
        floor.SetActive(true);

        howManySinceLast++;
        if (howManySinceLast > howMany) {
            howManySinceLast = 0;
            protection.SetActive(true);
        }
    }

    public override void ClearLeftWall() {
        leftWall.SetActive(true);
    }

    public override void ClearRightWall() {
        rightWall.SetActive(true);
    }

    public override void ClearFrontWall() {
        frontWall.SetActive(true);
    }

    public override void ClearBackWall() {
        backWall.SetActive(true);
    }
}
