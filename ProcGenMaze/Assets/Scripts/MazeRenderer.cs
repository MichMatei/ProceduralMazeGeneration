using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRenderer : MonoBehaviour
{
    private int height;

    [SerializeField]
    [Range(1, 100)]
    private int width = 10;

    [SerializeField]
    private float size = 1f;

    [SerializeField]
    private Transform wallPrefab = null;

    [SerializeField]
    private Transform theCube = null;

    Vector3 position;
    Vector3 positionForNewRow;

    Vector3 topPositionSaved;
    Vector3 downPositionSaved;
    Vector3 leftPositionSaved;
    Vector3 rightPositionSaved;
    Quaternion upDownRotationSaved;
    Quaternion leftRightRotationSaved;

    // Start is called before the first frame update
    void Start()
    {
        height = width * 6;
        positionForNewRow = position;
        theCube.localScale = Vector3.one * width;
        theCube.position = Vector3.right * ((float)width / 2 - 0.5f) + Vector3.up * (((float)width / 2 + 0.5f) * -1)
                         + Vector3.forward * ((float)width / 2 - 0.5f);
        var maze = MazeGenerator.Generate(height, width);
        Draw(maze);
    }

    private void Draw(Cell[,] maze)
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                var cell = maze[i, j];

                //the next if statements handle the correct position and rotation for the walls in realation to each side of the cube
                if (i < width)
                {
                    topPositionSaved = position + (Vector3.forward * size / 2);
                    downPositionSaved = position + (Vector3.back * size / 2);
                    leftPositionSaved = position + (Vector3.left * size / 2);
                    rightPositionSaved = position + (Vector3.right * size / 2);

                    upDownRotationSaved = Quaternion.Euler(0, 0, 0);
                    leftRightRotationSaved = Quaternion.Euler(0, 90, 0);
                }
                else if (i < width * 2)
                {
                    topPositionSaved = position + (Vector3.down * size / 2);
                    downPositionSaved = position + (Vector3.up * size / 2);
                    leftPositionSaved = position + (Vector3.left * size / 2);
                    rightPositionSaved = position + (Vector3.right * size / 2);

                    upDownRotationSaved = Quaternion.Euler(90, 0, 0);
                    leftRightRotationSaved = Quaternion.Euler(0, 90, 0);
                }
                else if (i < width * 3)
                {
                    topPositionSaved = position + (Vector3.back * size / 2);
                    downPositionSaved = position + (Vector3.forward * size / 2);
                    leftPositionSaved = position + (Vector3.left * size / 2);
                    rightPositionSaved = position + (Vector3.right * size / 2);

                    upDownRotationSaved = Quaternion.Euler(0, 0, 0);
                    leftRightRotationSaved = Quaternion.Euler(0, 90, 0);
                }
                else if (i < width * 4)
                {
                    topPositionSaved = position + (Vector3.up * size / 2);
                    downPositionSaved = position + (Vector3.down * size / 2);
                    leftPositionSaved = position + (Vector3.left * size / 2);
                    rightPositionSaved = position + (Vector3.right * size / 2);

                    upDownRotationSaved = Quaternion.Euler(90, 0, 0);
                    leftRightRotationSaved = Quaternion.Euler(0, 90, 0);
                }
                else if (i < width * 5)
                {
                    topPositionSaved = position + (Vector3.down * size / 2);
                    downPositionSaved = position + (Vector3.up * size / 2);
                    leftPositionSaved = position + (Vector3.back * size / 2);
                    rightPositionSaved = position + (Vector3.forward * size / 2);

                    upDownRotationSaved = Quaternion.Euler(90, 0, 0);
                    leftRightRotationSaved = Quaternion.Euler(0, 0, 0);
                }
                else if (i < width * 6)
                {
                    topPositionSaved = position + (Vector3.down * size / 2);
                    downPositionSaved = position + (Vector3.up * size / 2);
                    leftPositionSaved = position + (Vector3.forward * size / 2);
                    rightPositionSaved = position + (Vector3.back * size / 2);

                    upDownRotationSaved = Quaternion.Euler(90, 0, 0);
                    leftRightRotationSaved = Quaternion.Euler(0, 0, 0);
                }

                //the next 4 regions, if enabled, they help spawn all of the false walls of the maze, and turn their mesh renderer off
                //so that the whole layout of the maze with all of its walls, true state or false, can be inspected.
                if (cell.UpWall == true)      
                {
                    var topWall = Instantiate(wallPrefab, transform);
                    topWall.name = "TopWall of Cell[" + i + "][" + j + "]";
                    topWall.position = topPositionSaved;
                    topWall.localScale = new Vector3(size, topWall.localScale.y, topWall.localScale.z);
                    topWall.rotation = upDownRotationSaved;
                }
                #region
                //else if (cell.UpWall == false)
                //{
                //    var topWall = Instantiate(wallPrefab, transform);
                //    topWall.gameObject.GetComponent<MeshRenderer>().enabled = false;
                //    topWall.name = "TopWall of Cell[" + i + "][" + j + "]";
                //    topWall.position = position + Vector3.forward * size / 2;
                //    topWall.localScale = new Vector3(size, topWall.localScale.y, topWall.localScale.z);
                //}
                #endregion

                if (cell.DownWall == true)
                {
                    var downWall = Instantiate(wallPrefab, transform);
                    downWall.name = "DownWall of Cell[" + i + "][" + j + "]";
                    downWall.position = downPositionSaved;
                    downWall.localScale = new Vector3(size, downWall.localScale.y, downWall.localScale.z);
                    downWall.rotation = upDownRotationSaved;
                }
                #region
                //else if (cell.DownWall == false)
                //{
                //    var DownWall = Instantiate(wallPrefab, transform);
                //    DownWall.gameObject.GetComponent<MeshRenderer>().enabled = false;
                //    DownWall.name = "DownWall of Cell[" + i + "][" + j + "]";
                //    DownWall.position = position + Vector3.back * size / 2;
                //    DownWall.localScale = new Vector3(size, DownWall.localScale.y, DownWall.localScale.z);
                //}
                #endregion

                if (cell.LeftWall == true)
                {
                    var leftWall = Instantiate(wallPrefab, transform);
                    leftWall.name = "LeftWall of Cell[" + i + "][" + j + "]";
                    leftWall.position = leftPositionSaved;
                    leftWall.localScale = new Vector3(size, leftWall.localScale.y, leftWall.localScale.z);
                    leftWall.rotation = leftRightRotationSaved;
                }
                #region
                //else if (cell.LeftWall == false)
                //{
                //    var leftWall = Instantiate(wallPrefab, transform);
                //    leftWall.gameObject.GetComponent<MeshRenderer>().enabled = false;
                //    leftWall.name = "LeftWall of Cell[" + i + "][" + j + "]";
                //    leftWall.position = position + Vector3.left * size / 2;
                //    leftWall.localScale = new Vector3(size, leftWall.localScale.y, leftWall.localScale.z);
                //    leftWall.eulerAngles = new Vector3(0, 90, 0);
                //}
                #endregion

                if (cell.RightWall == true)
                {
                    var rightWall = Instantiate(wallPrefab, transform);
                    rightWall.name = "RightWall of Cell[" + i + "][" + j + "]";
                    rightWall.position = rightPositionSaved;
                    rightWall.localScale = new Vector3(size, rightWall.localScale.y, rightWall.localScale.z);
                    rightWall.rotation = leftRightRotationSaved;
                }
                #region
                //else if (cell.RightWall == false)
                //{
                //    var rightWall = Instantiate(wallPrefab, transform);
                //    rightWall.gameObject.GetComponent<MeshRenderer>().enabled = false;
                //    rightWall.name = "RightWall of Cell[" + i + "][" + j + "]";
                //    rightWall.position = position + Vector3.right * size / 2;
                //    rightWall.localScale = new Vector3(size, rightWall.localScale.y, rightWall.localScale.z);
                //    rightWall.eulerAngles = new Vector3(0, 90, 0);
                //}
                #endregion
                
                //the next if statements handle how the lines of the maze should be spawned
                //they will always be spwaned from left to right with the exception of the last 2 sides of the maze
                if (i < width * 4)
                {
                    position = position + Vector3.right;
                }
                else if (i < width * 5)
                {
                    position = position + Vector3.forward;
                }
                else
                {
                    position = position + Vector3.back;
                }
                
            }

            //the next if statements handle where the start of the new line should be, in correlation with the actual pozition on the maze
            if (i < width - 1)
            {
                position = positionForNewRow;
                position = position + Vector3.forward;
            }
            else if (i == width - 1)
            {
                position = positionForNewRow;
                position = position + Vector3.forward;
                position = position + Vector3.down;
            }
            else if (i < width * 2 - 1)
            {
                position = positionForNewRow;
                position = position + Vector3.down;
            }
            else if (i == width * 2 - 1)
            {
                position = positionForNewRow;
                position = position + Vector3.back;
                position = position + Vector3.down;
            }
            else if (i < width * 3 - 1)
            {
                position = positionForNewRow;
                position = position + Vector3.back;
            }
            else if (i == width * 3 - 1)
            {
                position = positionForNewRow;
                position = position + Vector3.back;
                position = position + Vector3.up;
            }
            else if (i < width * 4 - 1)
            {
                position = positionForNewRow;
                position = position + Vector3.up;
            }
            else if (i == width * 4 - 1)
            {
                position = positionForNewRow;
                position = position + Vector3.left;
                position = position + Vector3.forward;
            }
            else if (i < width * 5 - 1)
            {
                position = positionForNewRow;
                position = position + Vector3.down;
            }
            else if (i == width * 5 - 1)
            {
                position = positionForNewRow;
                position = position + Vector3.up * (width - 1);
                position = position + Vector3.right * (width + 1);
                position = position + Vector3.forward * (width - 1);
            }
            else if (i < width * 6 - 1)
            {
                position = positionForNewRow;
                position = position + Vector3.down;
            }
            positionForNewRow = position;
        }
    }

    //the update is here just to prevent the user from having to ALT+F4 eerytime he is running the build.
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}