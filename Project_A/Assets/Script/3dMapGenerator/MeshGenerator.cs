using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    public SquareGrid squareGrid;

    public void GenerateMesh(int[,] map, float squareSize)
    {
        squareGrid = new SquareGrid(map, squareSize);
    }

    private void OnDrawGizmos()
    {
        if (squareGrid != null)
        {
            for (int x = 0; x < squareGrid.squares.GetLength(0); x++)
            {
                for (int y = 0; y < squareGrid.squares.GetLength(1); y++)
                {
                    Gizmos.color = (squareGrid.squares[x, y].TopLeft.active) ? Color.black : Color.white;
                    Gizmos.DrawCube(squareGrid.squares[x, y].TopLeft.Position, Vector3.one * 0.4f);
                    
                    Gizmos.color = (squareGrid.squares[x, y].TopRight.active) ? Color.black : Color.white;
                    Gizmos.DrawCube(squareGrid.squares[x, y].TopRight.Position, Vector3.one * 0.4f);
                    
                    Gizmos.color = (squareGrid.squares[x, y].BottomRight.active) ? Color.black : Color.white;
                    Gizmos.DrawCube(squareGrid.squares[x, y].BottomRight.Position, Vector3.one * 0.4f);
                    
                    Gizmos.color = (squareGrid.squares[x, y].BottomLeft.active) ? Color.black : Color.white;
                    Gizmos.DrawCube(squareGrid.squares[x, y].BottomLeft.Position, Vector3.one * 0.4f);
                    
                    Gizmos.color = Color.gray;
                    Gizmos.DrawCube(squareGrid.squares[x,y].CentreTop.Position,Vector3.one * 0.15f);
                    Gizmos.DrawCube(squareGrid.squares[x,y].CentreRight.Position,Vector3.one * 0.15f);
                    Gizmos.DrawCube(squareGrid.squares[x,y].CentreLeft.Position,Vector3.one * 0.15f);
                    Gizmos.DrawCube(squareGrid.squares[x,y].CentreBottom.Position,Vector3.one * 0.15f);
                    
                }
            }
        }
    }

    public class SquareGrid
    {
        public Square[,] squares;

        public SquareGrid(int[,] map, float squareSize)
        {
            var nodeCountX = map.GetLength(0);
            var nodeCountY = map.GetLength(1);

            var mapWidth = nodeCountX * squareSize;
            var mapHeight = nodeCountY * squareSize;

            ControlNode[,] controlNodes = new ControlNode[nodeCountX, nodeCountY];

            for (int x = 0; x < nodeCountX; x++)
            {
                for (int y = 0; y < nodeCountY; y++)
                {
                    var pos = new Vector3(-mapWidth / 2 + x * squareSize + squareSize / 2, 0,
                        -mapHeight / 2 + y * squareSize + squareSize / 2);
                    controlNodes[x, y] = new ControlNode(pos, map[x, y] == 1, squareSize);
                }
            }

            squares = new Square[nodeCountX - 1, nodeCountY - 1];
            for (int x = 0; x < nodeCountX - 1; x++)
            {
                for (int y = 0; y < nodeCountY - 1; y++)
                {
                    squares[x, y] = new Square(controlNodes[x, y + 1], controlNodes[x + 1, y + 1],
                        controlNodes[x + 1, y],
                        controlNodes[x, y]);
                }
            }
        }
    }

    public class Square
    {
        public ControlNode TopLeft, TopRight, BottomRight, BottomLeft;
        public Node CentreTop, CentreRight, CentreBottom, CentreLeft;

        public Square(ControlNode _topLeft, ControlNode _topRight, ControlNode _bottomRight, ControlNode _bottomLeft)
        {
            TopLeft = _topLeft;
            TopRight = _topRight;
            BottomRight = _bottomRight;
            BottomLeft = _bottomLeft;

            CentreTop = TopLeft.right;
            CentreRight = BottomRight.above;
            CentreBottom = BottomLeft.right;
            CentreLeft = BottomLeft.above;
        }
    }

    public class Node
    {
        public Vector3 Position;
        public int VertexIndex = -1;

        public Node(Vector3 _pos)
        {
            Position = _pos;
        }
    }

    public class ControlNode : Node
    {
        public bool active;
        public Node above, right;

        public ControlNode(Vector3 _pos, bool _active, float squareSize) : base(_pos)
        {
            active = _active;
            above = new Node(Position + Vector3.forward * squareSize / 2f);
            right = new Node(Position + Vector3.right * squareSize / 2f);
        }
    }
}