using UnityEngine;

public class MazeCell : MonoBehaviour {
    [SerializeField] private GameObject unvisitedBlock;
    public bool IsExit { get; private set; } = false;
    public bool IsVisited {  get; private set; } = false;

    public virtual void Visit() {
        IsVisited = true;
        unvisitedBlock.SetActive(false);
        NotSetAsExit();
    }

    public virtual void ClearLeftWall() {

    }

    public virtual void ClearRightWall() {

    }

    public virtual void ClearFrontWall() {

    }

    public virtual void ClearBackWall() {

    }
    public virtual void SetAsExit() {
        IsExit = true;
    }

    public virtual void NotSetAsExit() {
        IsExit = false;
    }
}
