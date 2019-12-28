using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * 城镇划9格
 *  1 2 3
 *  4 5 6
 *  7 8 9
 */
public class MapCreator : MonoBehaviour
{
    public Vector2 size;
    public int town_num;
    public string[,] maploc;
    public List<Vector2> objloc;//obj location

    public struct Path
    {
        public Vector2 start;
        public Vector2 end;
        public float distance;

        public Path(Vector2 start, Vector2 end, float distance)
        {
            this.start = start;
            this.end = end;
            this.distance = distance;
        }
    }


    private void Awake()
    {
        objloc = new List<Vector2>();
        maploc = new string[(int)size.x, (int)size.y];
        //initialization
        initialGround();
        //town
        createTown();
        //forest
        createForest();
        //river
        createRiver();
        //path
        createPath();
    }

    void createPath()
    {
        List<Path> paths = new List<Path>();
        for (int i = 0; i < objloc.Count; i++)
        {
            float dist = float.MaxValue;
            Vector2 start = objloc[i];
            Vector2 end = Vector2.zero;
            for (int j = 0; j < objloc.Count; j++)
            {
                if (objloc[i] == objloc[j])
                    continue;
                float nextdist = Vector2.Distance(objloc[i], objloc[j]);
                if (dist > nextdist && !ContainSamePath(paths,objloc[i],objloc[j]))
                {
                    end = objloc[j];
                    dist = nextdist;
                }
            }
            paths.Add(new Path(start, end, dist));
        }
        //create close list
        bool[,] closed = new bool[maploc.GetLength(0), maploc.GetLength(1)];
        for (int i = 0; i < maploc.GetLength(0); i++)
        {
            for (int j = 0; j < maploc.GetLength(1); j++)
            {
                //禁止进入方块放进closedlist
                if (maploc[i, j] == "w")
                {
                    closed[i, j] = true;
                }
                else
                {
                    closed[i, j] = false;
                }
            }
        }
        //paths.Clear();
        //paths.Add(new Path(new Vector2(2, 5), new Vector2(72, 43), 10));
        createPathTile(closed, paths);
    }

    bool ContainSamePath(List<Path> paths, Vector2 start, Vector2 end)
    {
        foreach (Path p in paths)
        {
            if (p.start == end && p.end == start)
                return true;
        }
        return false;
    }

    /* A* to create path tile
        只有上下左右4个方向
         */
    void createPathTile(bool[,] closed, List<Path> paths)
    {
        List<PathBlock> openlist = new List<PathBlock>();
        foreach (Path p in paths)
        {
            Debug.Log(p.start + "," + p.end);
            PathBlock startPnt = new PathBlock(null, p.start.x, p.start.y, 0, 0, 0);
            openlist.Clear();
            // 1. add start point to openlist
            openlist.Add(startPnt);
            FindPath(openlist, startPnt, p, (bool[,])closed.Clone());
            //create path in map loc
            setPathTile(openlist, p);
        }
    }

    void FindPath(List<PathBlock> openlist, PathBlock startPnt, Path p, bool[,] closed)
    {
        float cloest = float.MaxValue;
        while (!ContainEnd(openlist, p.end.x, p.end.y))
        {
            //if (openlist.Count >= 60 && MahattanDistance(startPnt.position_x,p.end.x, startPnt.position_y,p.end.y)< cloest)
            //    openlist.Clear();
            //2. find avaliable paths and put them into openlist
            //int[] offset = new int[] { 1, 0, -1, 0, 0, 1, 0, -1, 1, 1, -1, 1, 1, -1, -1, -1 };
            int[] offset = new int[] { 1, 0, -1, 0, 0, 1, 0, -1};
            for (int i = 0; i < 8; i = i + 2)
            {
                float block_x = startPnt.position_x + offset[i];
                float block_y = startPnt.position_y + offset[i + 1];
                if (block_x < 0 || block_x >= size.x || block_y < 0 || block_y >= size.y)
                    continue;
                if (p.end.x - p.start.x >= 0)
                {
                    if (block_x < p.start.x)
                        continue;
                }
                else if (block_x > p.start.x)
                    continue;

                if (p.end.y - p.start.y >= 0)
                {
                    if (block_y < p.start.y)
                        continue;
                }
                else if (block_y > p.start.y)
                    continue;

                Vector2 block = new Vector2(block_x, block_y);
                if (closed[(int)block.x, (int)block.y] != true)
                {
                    //cost
                    float c = 0;
                    //if (i >= 8)
                    //{
                    //    c = startPnt.c + 13;
                    //}
                    //else
                    //{
                        c = startPnt.c + 10;
                    //}

                    //distance
                    float d =  MahattanDistance(p.end.x, block.x, p.end.y, block.y) * 10;

                    PathBlock thispb = new PathBlock(startPnt, block.x, block.y, c + d, c, d);
                    PathBlock samepb = ContainSamePos(openlist, thispb.position_x, thispb.position_y);
                    if (samepb != null)
                    {
                        if (samepb.t < thispb.t)
                        {
                            samepb = thispb;
                        }
                    }
                    else
                    {
                        openlist.Add(thispb);
                    }
                }
            }
            //move parent point from openlist to closedlist
            openlist.Remove(startPnt);
            closed[(int)startPnt.position_x, (int)startPnt.position_y] = true;
            //calculate G + H = F where G is cost, H is distance to end point;
            float lowest = float.MaxValue;
            foreach (PathBlock pb in openlist)
            {
                if (lowest > pb.t)
                {
                    lowest = pb.t;
                    startPnt = pb;
                }

                if (cloest > pb.d)
                    cloest = pb.d;
            }
        }
        
    }

    float MahattanDistance(float x1, float x2, float y1, float y2)
    {
        return Mathf.Abs(x1-x2) + Mathf.Abs(y1- y2);
    }

    PathBlock ContainSamePos(List<PathBlock> openlist, float x,float y)
    {
        foreach (PathBlock pb in openlist)
        {
            if (pb.position_x == x&& pb.position_y == y)
                return pb;
        }
        return null;
    }

    void setPathTile(List<PathBlock> openlist,Path p)
    {
        PathBlock end = returnEnd(openlist, p.end.x, p.end.y);
        while(end != null)
        {
            if (!maploc[(int)end.position_x, (int)end.position_y].Equals("t"))
            {
                maploc[(int)end.position_x, (int)end.position_y] = "p";
            }
            end = end.parent;
        }
    }

    PathBlock returnEnd(List<PathBlock> openlist, float x, float y)
    {
        foreach (PathBlock pb in openlist)
        {
            if (pb.position_x == x && pb.position_y == y)
                return pb;
        }
        return null;
    }

    bool ContainEnd(List<PathBlock> openlist, float x, float y)
    {
        foreach (PathBlock pb in openlist)
        {
            if (pb.position_x == x && pb.position_y == y)
                return true;
        }

        return false;
    }

    //create river
    void createRiver()
    {

        for (int k = 0; k < 6; k++)
        {
            int x = (int)Random.Range(0, size.x - 1);
            int startX = x;
            int y = (int)Random.Range(0, size.y - 1);
            int dirX = 1;//方向
            int dirY = 1;
            while (checkPlace(x, y, "f"))
            {
                x = (int)Random.Range(0, size.x - 1);
                y = (int)Random.Range(0, size.y - 1);
            }

            int row = Random.Range(6, 12) + Random.Range(-2, 2);

            for (int i = 0; i < row; i++)
            {
                int col = Random.Range(6, 12) + Random.Range(-2, 2);
                for (int j = 0; j < col; j++)
                {
                    if (maploc[x, y].Equals("g"))
                    {
                        maploc[x, y] = "w";
                    }
                    else if (maploc[x, y].Equals("f"))
                    {
                        if (Random.Range(0, 1f) > 0.8f)
                        {
                            maploc[x, y] = "w";
                        }
                    }
                    else if (maploc[x, y].Equals("t"))
                    {
                        break;
                    }

                    if (x + dirX > 0 && x + dirX < size.x)
                    {
                        x = x + dirX;
                    }
                    else
                        break;
                }
                x = startX + Random.Range(-2, 2);
                while (x < 0 || x >= size.x)
                {
                    x = startX + Random.Range(-2, 2);
                }
                if (y + dirY > 0 && y + dirY < size.y)
                    y = y + dirY;
                else
                    break;
            }
        }
    }

    /*9格之间是不是同一类型
        1 2 3
        4 5 6
        7 8 9

        5是(x,y) 1 2 3 4 6 7 8 9是不是同一类型p
        如果是同一类型true 不是false
     */
    bool checkPlace(int x,int y,string p)
    {
        int[] offset = new int[16] { -1, -1, 0, -1, 1, -1, -1, 0, 1, 0, -1, 1, 0, 1, 1, 1 };
        int count = 0;
        for (int i = 0; i < offset.GetLength(0); i = i+2)
        {
            if (x + offset[i] > 0 && x + offset[i] < size.x && y + offset[i + 1] > 0 && y + offset[i + 1] < size.y)
            {
                if (maploc[x + offset[i], y + offset[i + 1]].Equals(p))
                {
                    count++;
                }
            }
            else
                count++;

        }

        if (count >= 3)
            return true;
        else
            return false;

    }

    void initialGround()
    {
        for (int i = 0; i < maploc.GetLength(0); i++)
        {
            for (int j = 0; j < maploc.GetLength(1); j++)
            {
                maploc[i, j] = "g";
            }
        }
    }

    void createTown()
    {
        float horizontal = size.x / 3-3;
        float vertical = size.y / 3-3;
        float prevX = 1;
        float prevY = size.y-1;
        for (int i = 1; i <= 9; i++)
        {
            int x = (int)Random.Range(prevX, prevX + horizontal);
            int y = (int)Random.Range(prevY, prevY - vertical);
            if (maploc[x, y].Equals("t"))
            {
                x = (int)Random.Range(prevX + 4f, prevX + horizontal - 4f);
                y = (int)Random.Range(prevY - 4f, prevY - vertical - 4f);
            }
            //Debug.Log(x + "," + y);
            maploc[x, y] = "t";
            maploc[x-1, y] = "t";
            maploc[x, y-1] = "t";
            maploc[x-1, y - 1] = "t";
            objloc.Add(new Vector2(x, y));
            prevX = prevX + horizontal;
            if (i % 3 == 0)
            {
                prevX = 1;
                prevY = prevY - vertical;
            }
        }
    }

    void createForest()
    {    
        int dirX = 1;//方向
        int dirY = 1;
        float r = 0.5f;//随机生成率
        for (int i = 0; i < 12; i++)
        {
            bool isTown = false;
            int x = 0;
            int y = 0;
            if (i < 6)
            {
                x = (int)Random.Range(0, size.x - 1);
                y = (int)Random.Range(0, size.y / 2 -1 );
            }
            else
            {
                x = (int)Random.Range(0, size.x - 1);
                y = (int)Random.Range(size.y / 2, size.y - 1);
            }
            int startX = x;
            int startY = y;
            int row = (int)Random.Range(8, 20) + Random.Range(-2, 2);
            //行数
            for (int j = 0; j < row; j++)
            {
                int col = (int)Random.Range(10, 15) + Random.Range(-2, 2);
                //列数
                for (int k = 0; k < col; k++)
                {
                    //Debug.Log(x+","+y);
                    //如果不是城镇就是森林
                    if (Random.Range(0f, 1f) > r)
                    {
                        if (!maploc[x, y].Equals("t"))
                        {
                            maploc[x, y] = "f";
                        }
                        else
                        {
                            isTown = true;
                        }
                    }
                    //增加列数
                    if (x + dirX * 1 > 0 && x + dirX * 1 < size.x)
                    {
                        x = x + dirX;
                    }
                    else
                    {
                        dirX = -dirX;
                        x = startX + dirX;
                    }

                    if (r <= 0.1f)
                    {
                        r = 0.5f;
                    }
                    else
                    {
                        r = r - r / (float)(col * row);
                    }
                }
                //回归开始的点
                x = startX + Random.Range(-4, 4);
                while (x < 0 || x >= size.x)
                {
                    x = startX + Random.Range(-2, 2);
                }
                if (!isTown)
                {
                    if (y + dirY > 0 && y + dirY < size.y)
                        y = y + dirY;
                    else
                        y = y - dirY;
                }
                else
                {
                    y = startY;
                    dirY = -dirY;
                    if (y - dirY > 0 && y - dirY < size.y)
                        y = y - dirY;
                    else
                        y = startY + dirY;
                }
            }
        }
    }

}
