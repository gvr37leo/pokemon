

[Serializable]
public struct Vector2 {
    public float x;
    public float y;

    public Vector2(float x, float y) {
        this.x = x;
        this.y = y;
    }

    public Vector2 add(Vector2 v) {
        return new Vector2(x + v.x,y + v.y);
    }

    public Vector2 sub(Vector2 v) {
        return new Vector2(x - v.x,y-v.y);
    }

    public Vector2 mul(Vector2 v) {
        return new Vector2(x * v.x, y * v.y); ;
    }

    public Vector2 scale(float val) {
        return new Vector2(x * val, y * val);
    }

    public float length() {
        return MathF.Sqrt(x*x + y*y);
    }

    public Vector2 to(Vector2 v) {
        return v - this;
    }

    public Vector2 normalize() { 
        return scale(1 / length());
    }

    public float dot(Vector2 other) {
        return x * other.x + y * other.y;
    }

    public Vector2 projectOnto(Vector2 other) {
        return dot(other.normalize()) * other;
    }

    public static Vector2 Reflect(Vector2 normalout, Vector2 vecin) {
        var vecout = vecin * -1;
        var center = vecout.dot(normalout) * normalout;
        var vec2center = vecout.to(center);
        var refl = vecout + 2 * vec2center;
        return refl;
    }

    public Vector2 rot(float turns, Vector2 center = new Vector2()) {
        this = sub(center);
        float sint = MathF.Sin(turns * MathF.PI * 2);
        float cost = MathF.Cos(turns * MathF.PI * 2);
        float x = this.x * cost - this.y * sint;
        float y = this.x * sint + this.y * cost;
        this = add(center);
        return new Vector2(x,y);
    }

    public Vector2 lerp(Vector2 other, float t) {
        return this + to(other) * t;
    }

    public static Vector2 operator *(Vector2 a, Vector2 b) {
        return a.mul(b);
    }

    public static Vector2 operator *(float scale, Vector2 a) {
        return a * scale;
    }

    public static Vector2 operator *(Vector2 a, float scale) {
        return a.scale(scale);
    }

    public static Vector2 operator /(Vector2 a, float div) {
        return new Vector2(a.x / div, a.y / div);
    }

    public static Vector2 operator /(Vector2 a, Vector2 b) {
        return new Vector2(a.x / b.x,a.y/b.y);
    }

    public static Vector2 operator +(Vector2 a,Vector2 b) {
        return a.add(b);
    }

    public static Vector2 operator -(Vector2 a, Vector2 b) {
        return a.sub(b);
    }

    public static bool operator ==(Vector2 a, Vector2 b) {
        return a.x == b.x && a.y == b.y;
    }

    public static bool operator !=(Vector2 a, Vector2 b) {
        return !(a == b);
    }

    public Vector2 floor() {
        return new Vector2(MathF.Floor(x),MathF.Floor(y));
    }

    public override string ToString() {
        return $"x{x.ToString("F1")} y{y.ToString("F1")}";
    }

    public Vector2(System.Numerics.Vector2 v) {
        x = v.X; 
        y = v.Y;
    }

    public System.Numerics.Vector2 convert() {
        return new System.Numerics.Vector2(x,y);
    }

    public static implicit operator System.Numerics.Vector2(Vector2 v) => v.convert();
}
