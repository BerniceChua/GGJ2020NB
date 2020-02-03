using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface ITileData {
    String getId();
    ITileData getUp();
    ITileData getDown();
    ITileData getLeft();
    ITileData getRight();
}

public class InfoOfEachTile : MonoBehaviour, ITileData {

    [SerializeField] private AudioClip m_sfxPickup;
    [SerializeField] private AudioClip m_sfxPutdown;
    [SerializeField] private AudioClip m_sfxInvalid;

    [SerializeField] private float m_tileWidth = 1;
    [SerializeField] private float m_tileHeight = 1;

    [SerializeField] private bool m_placed = false;

    public String Id;
    public InfoOfEachTile Up;
    public InfoOfEachTile Down;
    public InfoOfEachTile Left;
    public InfoOfEachTile Right;

    public String getId() { return Id; } /// return name of prefab
	public ITileData getUp() { return Up; }
    public ITileData getDown() { return Down; }
    public ITileData getLeft() { return Left; }
    public ITileData getRight() { return Right; }

    public InfoOfEachTile(String id)
    {
        Id = id;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (m_placed == true)
        {
            return;
        }

        Debug.Log("Inside OnTriggerEnter2D");
        if (collision.gameObject != null)
        {
            m_placed = true;
            GameManager.remainingPieces -= 1;

            this.GetComponent<MovePiece1>().m_IsPickedUp = false;
            Debug.Log(this.GetComponent<MovePiece1>().m_IsPickedUp);

            Vector2 collideTilePos = collision.gameObject.transform.position;

            float selfX = this.gameObject.transform.position.x;
            float selfY = this.gameObject.transform.position.y;

            float xDistance = Mathf.Abs(selfX - collideTilePos.x);
            float yDistance = Mathf.Abs(selfY - collideTilePos.y);

            if (xDistance > yDistance)
            {
                /// CHeck if on left or right;
                CheckIfLeftOrRight(selfX, collideTilePos);
            } else
            {
                /// Check if up or down;
                CheckIfUpOrDown(selfY, collideTilePos);
            }

            GetComponent<AudioSource>().PlayOneShot(m_sfxPutdown);

            //validate();
        }
    }

    private void CheckIfLeftOrRight(float selfX, Vector2 collisionPos)
    {
        if (selfX > collisionPos.x)
        {
            /// this means it's on the right of the placed obj.
            this.gameObject.transform.position = new Vector2(collisionPos.x + m_tileWidth, collisionPos.y);
        }
        else
        {
            /// this means on the left
            this.gameObject.transform.position = new Vector2(collisionPos.x - m_tileWidth, collisionPos.y);


        }
    }

    private void CheckIfUpOrDown(float selfY, Vector2 collisionPos)
    {
        if (selfY > collisionPos.y)
        {
            /// this means it's on the up of the placed obj.
            this.gameObject.transform.position = new Vector2(collisionPos.x, collisionPos.y + m_tileHeight);
        }
        else
        {
            /// this means on the down
            this.gameObject.transform.position = new Vector2(collisionPos.x, collisionPos.y - m_tileHeight);
        }
    }

    public static Boolean validateTile(ITileData tile)
    {
        return Program.gridValid(Program.miniGridFromTile(tile), 3, 3);
    }

    public Boolean validate()
    {
       return  validateTile(this);
    }

    public static void connectHorizontal(InfoOfEachTile left, InfoOfEachTile right)
    {
        if (left != null && right != null)
        {
            left.Right = right;
            right.Left = left;
        }
    }

    public static void connectVertical(InfoOfEachTile up, InfoOfEachTile down)
    {
        if (up != null && down != null)
        {
            up.Down = down;
            down.Up = up;
        }
    }
}


public class Program {
    private const String HOOKCODES = @"
_|M|_|_|,_|_|_|M|,_|_V_|_|,_|_|_V_|,_|_V_|_|,_|_|_V_|,_|_|M|_|,_|_|_|_|
_|M|_|_|,_|_|_|M|,_V_|M|_|,_|M|_|_V,_V_|_|M|,_|_|M|_V,M|_V_|_|,_|_|_V_|
_|_|_|_|,_|_V_|_|,M|_|_V_|,_|_|_|_|,_|_|M|_|,M|_|_|_|,_V_|M|_|,_|_|_|_V
_|_|_|_|,_V_|M|_|,_|M|_|_V,_|_|_|M|,M|_|_|_|,_|_|_|_|,M|_V_|_|,_|_|_V_|
_|_V_|_|,M|_|_V_|,_|M|_|_|,_|_V_|M|,_|_|_V_|,_|M|_|_|,_V_|_|M|,_|_|_|_V
_V_|_|_|,_|_|_M|_V,_|_|_|_|,_V_|_|_|,_|M|_|_V,_|_|_|M|,_|M|_|_|,_|_|_|M|
_|_V_|_|,M|_|_V_|,_|M|_|_|,_|_V_|M|,_|_|_V_|,_|M|_|_|,_|_|_|M|,_|_|_|_|
_V_|_|_|,_|_|_|_V,_|_|_|_|,_V_|_|_|,_|_|_|_V,_|M|_|_|,_|_|_|M|,_|_|_|_|
	";
    static String leftRotate(String str, int d)
    {
        String ans = str.Substring(d, str.Length - d) + str.Substring(0, d);
        return ans;
    }

    static String rightRotate(String str, int d)
    {
        return leftRotate(str, str.Length - d);
    }

    //	Code of a tile that got rotated clockwise
    public static String rotateClockwise(String code, int times)
    {
        return rightRotate(code, 2 * times);
    }

    //	Code of a tile that got rotated counter-clockwise
    public String rotateCounterClockwise(String code, int times)
    {
        return leftRotate(code, 2 * times);
    }

    /*
	public enum Direction : int {
		U = 0,
		UR = 1,
		R = 2,
		DR = 3,
		D = 4,
		DL = 5,
		L = 6,
		LU = 7,
	};

	public enum Direction : int {
		N = 0,
		NE = 1,
		E = 2,
		SE = 3,
		S = 4,
		SW = 5,
		W = 6,
		NW = 7,
	};
	*/
    public const int N = 0;
    public const int NE = 1;
    public const int E = 2;
    public const int SE = 3;
    public const int S = 4;
    public const int SW = 5;
    public const int W = 6;
    public const int NW = 7;
    public static Boolean tilesFitHorizontal(String left, String right)
    {
        if (left != null && right != null)
        {
            if (left[NE] != right[NW])
            {
                return false;
            }

            if (left[E] != right[W])
            {
                return false;
            }

            if (left[SE] != right[SW])
            {
                return false;
            }
        }

        return true;
    }

    public static Boolean tilesFitVertical(String top, String bottom)
    {
        if (top != null && bottom != null)
        {
            if (top[SE] != bottom[NE])
            {
                return false;
            }

            if (top[S] != bottom[N])
            {
                return false;
            }

            if (top[SW] != bottom[NW])
            {
                return false;
            }
        }
        return true;
    }


    //	Given 4 tiles make sure all tiles fit
    //	- null tiles means empty. Anything fits there.
    public static Boolean tilesFit(String tileTopLeft, String tileTopRight, String tileBottomLeft, String tileBottomRight)
    {
        //	check left right
        if (!tilesFitHorizontal(tileTopLeft, tileTopRight))
        {
            return false;
        }

        if (!tilesFitHorizontal(tileBottomLeft, tileBottomRight))
        {
            return false;
        }

        //	check top down
        if (!tilesFitVertical(tileTopLeft, tileBottomLeft))
        {
            return false;
        }

        if (!tilesFitVertical(tileTopRight, tileBottomRight))
        {
            return false;
        }

        //	check corners
        if (tileTopLeft != null && tileBottomRight != null)
        {
            if (tileTopLeft[SE] != tileBottomRight[NW])
            {
                return false;
            }
        }

        if (tileBottomLeft != null && tileTopRight != null)
        {
            if (tileBottomLeft[NE] != tileTopRight[SW])
            {
                return false;
            }
        }

        return true;
    }

    private static String getTileAt(int col, int row)
    {
        return getTileAt(col, row, 0);
    }

    private static String getTileAt(int col, int row, int rotation)
    {
        String[] lines = HOOKCODES.Split('\n').Where(l => l.Trim() != "").ToArray();
        if (row < 0 || row >= lines.Length)
        {
            return null;
        }

        String line = lines[row];
        if (col < 0 || col >= line.Length)
        {
            return null;
        }

        String code = line.Split(',')[col];
        return rotateClockwise(code, rotation);
    }



    private static String getTile(String id)
    {
        if (id == null)
        {
            return null;
        }
        String[] firstSplit = id.Split('|');
        int rotation = firstSplit.Length > 1 ? Int32.Parse(firstSplit.Last()) : 0;

        int idNum = Int32.Parse(firstSplit[0].Split('_').Last());
        int col = idNum % 8;
        int row = (int)Math.Floor((double)idNum / 8);

        //	 	Console.WriteLine(id + " = " + col + "," + row + "," + rotation);

        return getTileAt(col, row, rotation);
        // 
        //		id = id.ToUpper();
        //		int col = id[0] - 'A';
        //		int row = id[1] - '1';
        //		int rotation = id.Length < 3 ? 0 : id[2] - '0';
        //		return getTileAt(col, row, rotation);
    }

    private static String getTileFromGrid(String[,] grid, int x, int y)
    {
        if (x < grid.GetLowerBound(1) || x > grid.GetUpperBound(1))
        {
            return null;
        }
        if (y < grid.GetLowerBound(0) || y > grid.GetUpperBound(0))
        {
            return null;
        }

        return getTile(grid[y, x]);
    }

    public static Boolean gridValid(String[,] grid, int width, int height)
    {
        for (int y = 0; y < height - 1; y++)
        {
            for (int x = 0; x < width - 1; x++)
            {
                String topLeft = getTileFromGrid(grid, x, y);
                String topRight = getTileFromGrid(grid, x + 1, y);
                String bottomLeft = getTileFromGrid(grid, x, y + 1);
                String bottomRight = getTileFromGrid(grid, x + 1, y + 1);
                if (!tilesFit(topLeft, topRight, bottomLeft, bottomRight))
                {
                    return false;
                }
            }
        }

        return true;
    }

    private static void spread(ITileData tile, int x, int y, String[,] grid)
    {
        if (tile == null)
        {
            return;
        }
        if (x < grid.GetLowerBound(1) || x > grid.GetUpperBound(1))
        {
            return;
        }
        if (y < grid.GetLowerBound(0) || y > grid.GetUpperBound(0))
        {
            return;
        }
        if (grid[y, x] != "NOT VISITED")
        {
            return;
        }
        grid[y, x] = tile.getId();
        spread(tile.getLeft(), x - 1, y, grid);
        spread(tile.getRight(), x + 1, y, grid);
        spread(tile.getUp(), x, y - 1, grid);
        spread(tile.getDown(), x, y + 1, grid);
    }

    public static String[,] miniGridFromTile(ITileData tile)
    {
        String[,] grid = new String[3, 3];
        for (var y = grid.GetLowerBound(0); y <= grid.GetUpperBound(0); y++)
        {
            for (var x = grid.GetLowerBound(1); x <= grid.GetUpperBound(1); x++)
            {
                grid[y, x] = "NOT VISITED";
            }
        }

        spread(tile, 1, 1, grid);

        for (var y = grid.GetLowerBound(0); y <= grid.GetUpperBound(0); y++)
        {
            for (var x = grid.GetLowerBound(1); x <= grid.GetUpperBound(1); x++)
            {
                if (grid[y, x] == "NOT VISITED")
                {
                    grid[y, x] = null;
                }
            }
        }

        return grid;
    }

    public static Boolean validateTile(ITileData tile)
    {
        return gridValid(miniGridFromTile(tile), 3, 3);
    }

    public static void Main()
    {
        //https://docs.google.com/spreadsheets/d/1v90ZW65jDo-c3_5oWHT7UYIYtDB3ma44uUdaoYh8TeQ/
        String[,] grid = {
            {"_0", "_1", "_2", "_3"},
            {"_8", "_9", "_10", "_11"},
            {"_16", "_17", "_18", "_19"},
            {"_24", "_25", "_26", "_27"},
        };
        Console.WriteLine("Grid valid? " + gridValid(grid, 4, 4));

        InfoOfEachTile topLeft = new InfoOfEachTile("_9");
        InfoOfEachTile topRight = new InfoOfEachTile("_10");
        InfoOfEachTile bottomLeft = new InfoOfEachTile("_17");
        InfoOfEachTile bottomRight = new InfoOfEachTile("_18");
        InfoOfEachTile.connectHorizontal(topLeft, topRight);
        InfoOfEachTile.connectHorizontal(bottomLeft, bottomRight);
        InfoOfEachTile.connectVertical(topLeft, bottomLeft);
        InfoOfEachTile.connectVertical(topRight, bottomRight);

        String[,] miniGrid = miniGridFromTile(topLeft);
        for (var y = miniGrid.GetLowerBound(0); y <= miniGrid.GetUpperBound(0); y++)
        {
            for (var x = miniGrid.GetLowerBound(1); x <= miniGrid.GetUpperBound(1); x++)
            {
                Console.Write(miniGrid[y, x] == null ? "__ " : miniGrid[y, x] + " ");
            }
            Console.WriteLine();
        }
        Console.WriteLine("GRID VALID: " + validateTile(bottomRight));
    }
}
