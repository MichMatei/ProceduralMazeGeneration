using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public struct Cell
{
    public bool UpWall;
    public bool DownWall;
    public bool LeftWall;
    public bool RightWall;
}

public static class MazeGenerator
{
    private static Cell[,] ApplySidewinder(Cell[,] maze, int height, int width)
    {
        List<int> myCellList = new List<int>();
        var random = new System.Random();

        for (int i = 0; i < height - 1; i++)
        {
            for (int j = 0; j < width; j++)
            {
                //picking a random direction between North and East, N is 0 and E is 1
                int x = random.Next(2);

                //adding the cell that we are currently in, inside the list
                myCellList.Add(j);

                if (x == 1)           //picked east
                {
                    if (j < width - 1)//we can go east, we do go east and we link the current cell with the one east of us
                    {
                        maze[i, j].RightWall = false;
                        maze[i, j + 1].LeftWall = false;
                    }
                    else
                    {                 //we can't go east so we pick north and randomly chose which cell from the list of cells we link north,
                                      //we link the two cells then we clear the list
                        int y = random.Next(myCellList[0], j + 1);

                        maze[i, y].UpWall = false;
                        maze[i + 1, y].DownWall = false;

                        myCellList.Clear();
                    }
                }
                else if (x == 0)   //picked north
                {                  //we randomly chose which cell will be link with it's northen neighboour from the list, we link them,
                                   //and then we clear the list
                    int y = random.Next(myCellList[0], j + 1);

                    maze[i, y].UpWall = false;
                    maze[i + 1, y].DownWall = false;

                    myCellList.Clear();
                }
            }
        }   
        
        //Insturctions for the last line in the maze so that the whole line doesn't have any left or right walls, with the exception
        //of the wall most east and the one most west, the bounds.
        for (int i = height - 1; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (i == height - 1 && j < width - 1)
                {
                    maze[i, j].RightWall = false;
                    maze[i, j + 1].LeftWall = false;
                }
                else if (i == height - 1 && j == width - 1)
                {
                    maze[i, j].LeftWall = false;
                }
            }
        }

        //The next instructions make sure that the last two sides of the cube are both connected to the 4th side of the cube, and that
        //the 4th side isn't connected with the first side.
        for (int i = 0; i < width; i++)
        {
            maze[width * 4 - 1, i].UpWall = true;
            maze[width * 4 - 1, i].LeftWall = false;
            maze[width * 4 - 1, i].RightWall = false;

            maze[width * 4, i].DownWall = true;
            maze[width * 4, i].LeftWall = false;
            maze[width * 4, i].RightWall = false;

            maze[width * 5 - 1, i].UpWall = true;
            maze[width * 5 - 1, i].LeftWall = false;
            maze[width * 5 - 1, i].RightWall = false;

            maze[width * 5, i].DownWall = true;
            maze[width * 5, i].LeftWall = false;
            maze[width * 5, i].RightWall = false;
        }

        maze[width * 4, width - 1].RightWall = true;

        maze[width * 5, 0].LeftWall = true;

        maze[width * 5 - 1, 0].LeftWall = true;
        maze[width * 5 - 1, width - 1].RightWall = true;

        return maze;
    }


    public static Cell[,] Generate(int height, int width)
    {
        Cell[,] maze = new Cell[height, width];

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                maze[i, j].UpWall = true;
                maze[i, j].DownWall = true;
                maze[i, j].LeftWall = true;
                maze[i, j].RightWall = true;
            }
        }

        return ApplySidewinder(maze, height, width);
        //return maze;
    }
}