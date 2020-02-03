using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
        id = id.ToUpper();
        int col = id[0] - 'A';
        int row = id[1] - '1';
        int rotation = id.Length < 3 ? 0 : id[2] - '0';
        return getTileAt(col, row, rotation);
    }

    private static String getTileFromGrid(String[][] grid, int x, int y)
    {
        if (y < 0 || y >= grid.Length)
        {
            return null;
        }

        String[] line = grid[y];
        if (x < 0 || x >= line.Length)
        {
            return null;
        }

        return getTile(grid[y][x]);
    }

    public static Boolean gridValid(String[][] grid, int width, int height)
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

    public static void Main()
    {
        //https://docs.google.com/spreadsheets/d/1v90ZW65jDo-c3_5oWHT7UYIYtDB3ma44uUdaoYh8TeQ/
        String[][] grid = {
            new[]{"A1", "B1", "C1", "D1"},
            new[]{"A2", "B2", "C2", "D2"},
            new[]{"A3", "B3", "C3", "D3"},
            new[]{"A4", "B4", "C4", "D4"},
        };
        Console.Write("Grid valid? " + gridValid(grid, 4, 4));
    }
}