using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using static Utils;
using Raylib_cs;


//todo scrolling
public class MenuItem {
    public string text;
    public string id;
    int rangemin;
    public Vector2 size;
    public int cursor;
    public List<MenuItem> children = new List<MenuItem>();
    public int fontsize = 40;
    public Vector2 menuposition = new Vector2(10, 10);
    public bool disabled = false;
    public Vector2 spacing = one * 10;
    public Action<MenuItem> cb = (item) => { };
    public int data;

    public MenuItem() {
        size = new Vector2(1,4);
    }

    public MenuItem touch(string id) {
        var child = children.Find(c => c.id == id);
        if (child != null) {
            return child;
        } else {
            var newchild = new MenuItem() { text = id,id = id };
            children.Add(newchild);
            return newchild;
        }
    }

    public MenuItem onClick(Action<MenuItem> cb) {
        this.cb = cb;
        return this;
    }

    public MenuItem Data(int data) {
        this.data = data;
        return this;
    }

    public MenuItem Id(string id) {
        this.id = id;
        return this;
    }

    public MenuItem getSelectedItem() {
        return children[cursor];
    }

    public int rangeSize() {
        return (int)(size.x * size.y);
    }

    public int rangeMax() {
        return rangemin + rangeSize();
    }

    public void setCursor(int index) {
        index = mod(index, children.Count);
        if(index < rangemin) {
            rangemin = index;
        }else if(index >= rangeMax()) {
            rangemin = index - rangeSize() + 1;
        }
        cursor = index;
    }

    public Vector2 getCursorV() {
        return index2vector(cursor);
    }

    public void setCursor(Vector2 v) {
        setCursor((int)(v.y * size.x + v.x));
    }

    public Vector2 getCellSize() {
        int maxwidth = findBest(children.Select(m => MeasureText(m.text, fontsize)).ToList(), w => w);
        var cellsize = new Vector2(maxwidth, fontsize);
        return cellsize;
    }

    public Vector2 getAbsSize() {
        int maxwidth = findBest(children.Select(m => MeasureText(m.text, fontsize)).ToList(), w => w);
        var cellsize = new Vector2(maxwidth, fontsize);
        return size * (cellsize + spacing);
    }

    public void Draw() {



        var cellsize = getCellSize();
        var rect = new Rect(menuposition, getAbsSize());
        
        //background
        DrawRectangleV(menuposition,rect.size(), GREEN);
        DrawBorder(rect.min, rect.size(), RED);

        for(int i = 0; i < rangeSize() && i < children.Count; i++) {
            int absi = i + rangemin;
            var child = children[absi];
            var pos = index2vector(i);
            var offset = pos * (cellsize + spacing) + menuposition;
            var textrect = new Rect(offset + spacing / 2,cellsize);

            //menutext
            Color textcolor = child.disabled ? GRAY : BLACK;
            DrawText(child.text,(int)textrect.min.x, (int)textrect.min.y,fontsize, textcolor);
            if(child.children.Count > 0) {
                //submenu hint
                RectCentered(textrect.edge(new Vector2(1,0.5f)),one * 10,RED);
            }

            //cursor
            if(absi == cursor) {
                RectCentered(textrect.edge(new Vector2(0, 0.5f)), one * 10, BLACK);
            }

            DrawBorder(textrect.min,textrect.size(),RED);
        }

        //cursor
        //int absCursor = (int)to(rangemin, cursor);
        //DrawRectangle(0, absCursor * fontsize + fontsize / 2, 10, 10, BLACK);


        //rangehints
        if(rangemin > 0) {
            RectCentered(rect.edge(new Vector2(0.5f, 0)), new Vector2(10, 10), BLACK);
        }
        if(rangeMax() < children.Count) {
            RectCentered(rect.edge(new Vector2(0.5f, 1)), new Vector2(10, 10), BLACK);
        }
        
    }

    Vector2 index2vector(int index) {
        return new Vector2((int)(index % size.x),(int)(index / size.x));
    }

    public MenuItem setSize(int x, int y) {
        size.x = x;
        size.y = y;
        return this;
    }

    
}