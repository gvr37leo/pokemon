using static Utils;

public struct Rect {

    public Vector2 min;
    public Vector2 max;

    public Rect(Vector2 pos, Vector2 size) {
        min = pos;
        max = pos + size;
    }

    public Vector2 size() {
        return min.to(max);
    }

    public Vector2 edge(Vector2 dir) {
        return new Vector2(lerp(min.x,max.x,dir.x), lerp(min.y, max.y, dir.y));
    }

}
