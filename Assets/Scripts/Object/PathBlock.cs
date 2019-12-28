public class PathBlock
{
    public PathBlock parent;
    public float position_x;
    public float position_y;
    public float t;//total cost
    public float c;//cost
    public float d;//distance

    public PathBlock(PathBlock parent, float position_x, float position_y, float t, float c, float d)
    {
        this.parent = parent;
        this.position_x = position_x;
        this.position_y = position_y;
        this.t = t;
        this.c = c;
        this.d = d;
    }
}
