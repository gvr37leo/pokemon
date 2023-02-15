using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using static Raylib_cs.Raymath;
using static Raylib_cs.KeyboardKey;
using Raylib_cs;
using static System.Math;
using System.Text.RegularExpressions;

public static class Utils {
    public static Vector2 zero = new Vector2(0, 0);
    public static Vector2 one = new Vector2(1, 1);
    public static float PI = (float)Math.PI;
    public static float TAU = PI * 2;
    public static Color gColor = BLACK;

    public static int mod(int x, int m) {
        return (x % m + m) % m;
    }

    public static void DrawRect(Vector2 pos, Vector2 size, Color color) {
        DrawRectangle((int)pos.x, (int)pos.y, (int)size.x, (int)size.y, color);
    }

    public static void RectCentered(Vector2 pos, Vector2 size, Color color) {
        DrawRect(pos - size / 2,size,color);
    }

    public static void DrawBorder(Vector2 pos, Vector2 size, Color color) {
        var br = pos + size;
        var tr = new Vector2(br.x, pos.y);
        var bl = new Vector2(pos.x, br.y);
        DrawLineV(pos, tr, color);
        DrawLineV(tr, br, color);
        DrawLineV(br, bl, color);
        DrawLineV(bl, pos, color);
    }

    public static void DrawTextCentered(string text,Vector2 pos, int fontsize, Color color) {
        DrawText(text, (int)pos.x - MeasureText(text, fontsize) / 2, (int)pos.y - fontsize / 2, fontsize, color);
    }

    public static void DrawTextMultiLine(string text, Vector2 pos, int fontsize,int linewidth,Color color) {
        var words = Regex.Split(text,@"(\s+)");
        Vector2 currentpos = zero;
        foreach (var word in words) {
            int width = MeasureText(word, fontsize);
            if(currentpos.x + width > linewidth && currentpos.x > 0) {
                currentpos.x = 0;
                currentpos.y += fontsize;
            }
            DrawText(word, (int)(currentpos.x + pos.x), (int)(currentpos.y + pos.y),fontsize,color);
            currentpos.x += width;
        }
    }

    public static Vector2 getInput() {
        Vector2 res = new Vector2();
        if (IsKeyDown(KEY_W) || IsKeyDown(KEY_UP)) {
            res.y -= 1;
        }
        if (IsKeyDown(KEY_S) || IsKeyDown(KEY_DOWN)) {
            res.y += 1;
        }
        if (IsKeyDown(KEY_A) || IsKeyDown(KEY_LEFT)) {
            res.x -= 1;
        }
        if (IsKeyDown(KEY_D) || IsKeyDown(KEY_RIGHT)) {
            res.x += 1;
        }
        return res;
    }

    public static float moveTo(float start,float target, float amount) {
        float dist = to(start, target);
            
        if(Abs(amount) > Abs(dist)) {
            return target;
        } else {
            return start + Sign(dist) * amount;
        }
    }

    public static float to(float a, float b) {
        return b - a;
    }

    public static bool inRange(float val, float min, float max) {
        return val >= min && val <= max;
    }

    public static bool pointRecCollission(Vector2 point, Vector2 min, Vector2 max) {
        return inRange(point.x,min.x,max.x) && inRange(point.y,min.y,max.y);
    }

    public static float circumerference(float radius) {
        return TAU * radius;
    }

    public static float lerp(float a, float b, float r) {
        return a + to(a, b) * r;
    }

    public static float inverseLerp(float val, float a, float b) {
        return to(a, val) / to(a, b);
    }

    public static float map(float val, float from1,float from2, float to1,float to2) {
        return lerp(to1, to2, inverseLerp(val, from1, from2));
    }

    public static T findBest<T>(List<T> list, Func<T, float> cb) {
        return list[findBestIndex(list, cb)];
    }

    public static int findBestIndex<T>(List<T> list, Func<T, float> cb) {
        if (list.Count == 0) return -1;
        float bestscore = cb(list[0]);
        int bestindex = 0;
        for (int i = 0; i < list.Count; i++) {
            float score = cb(list[i]);
            if (score > bestscore) {
                bestscore = score;
                bestindex = i;
            }
        }
        return bestindex;
    }

    public static T chooseRandom<T>(List<T> list) {
        return list[GetRandomValue(0,list.Count - 1)];
    }

    public static float GetTimeF() {
        return (float)GetTime();
    }

    public static (int, int) BinarySearchClosest(int[] array, int target) {
        int left = 0;
        int right = array.Length - 1;

        while (left <= right) {
            int middle = (left + right) / 2;
            if (array[middle] == target) {
                return (middle, middle);
            } else if (array[middle] < target) {
                left = middle + 1;
            } else {
                right = middle - 1;
            }
        }

        int leftClosest = left - 1;
        int rightClosest = right + 1;
        return (leftClosest, rightClosest);
    }
}


