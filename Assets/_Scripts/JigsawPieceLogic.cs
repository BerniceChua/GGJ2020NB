using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JigsawPieceLogic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
            if (top[SW] != bottom[SW])
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

    public static void Main()
    {
        //https://docs.google.com/spreadsheets/d/1v90ZW65jDo-c3_5oWHT7UYIYtDB3ma44uUdaoYh8TeQ/
        String tileTopLeft = "_|M|_|_|",
            tileTopRight = "_|_|_|M|",
            tileBottomLeft = "_|M|_|_|",
            tileBottomRight = "_|_|_|M|";
        Console.WriteLine("Top tiles fit? " + tilesFitHorizontal(tileTopLeft, tileTopRight));
        Console.WriteLine("Bottom tiles fit? " + tilesFitHorizontal(tileBottomLeft, tileBottomRight));
        Console.WriteLine("Do tiles fit?" + tilesFit(tileTopLeft, tileTopRight, tileBottomLeft, tileBottomRight));

    }
}