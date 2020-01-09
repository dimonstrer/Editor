using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

public abstract class Shape : IObservable
{
    protected List<IObserver> observers;
    protected int x;
    protected int y;
    protected Pen CurrentPen;
    protected static Pen CheckedPen = new Pen(Color.Red, 2);
    protected static Pen UncheckedPen = new Pen(Color.Black, 2);
    protected GraphicsPath myPath;
    public Shape()
    {
        observers = new List<IObserver>();
        x = 0;
        y = 0;
        myPath = new GraphicsPath();
        CurrentPen = UncheckedPen;
    }
    public Shape(int ax, int ay)
    {
        x = ax;
        y = ay;
        myPath = new GraphicsPath();
        CurrentPen = UncheckedPen;
    }
    public virtual void draw(PaintEventArgs e)
    {
        e.Graphics.DrawPath(CurrentPen, myPath);
    }
    public virtual bool isClicked(int ax, int ay)
    {
        return (myPath.IsVisible(ax, ay)) || myPath.IsOutlineVisible(ax, ay, CurrentPen);
    }
    public virtual void changeState()
    {
        if (CurrentPen == CheckedPen)
        {
            CurrentPen = UncheckedPen;
            return;
        }
        if (CurrentPen == UncheckedPen)
        {
            CurrentPen = CheckedPen;
            return;
        }
    }
    public virtual bool getCurrentState()
    {
        if (CurrentPen == CheckedPen)
            return true;
        else if (CurrentPen == UncheckedPen)
            return false;
        return false;
    }
    public virtual void move(int dx, int dy) { }
    public virtual bool checkBounds(int dx, int dy, int Fheight, int Fwidth) { return false; }
    public virtual void save(StreamWriter sw) { }
    public virtual void load(StreamReader rw, Factory factory) { }

    public void AddObserver(IObserver obs)
    {
        observers.Add(obs);
    }

    public void RemoveObserver(IObserver obs)
    {
        observers.Remove(obs);
    }

    public void NotifyEveryone(string type, object obj)
    {
        foreach (IObserver obs in observers)
            obs.Update(type, obj);
    }
}

class Circle : Shape
{
    private int width;
    private int height;
    public Circle() : base()
    {
        width = 10;
        height = 10;
        myPath.AddEllipse(x, y, width, height);
    }
    public Circle(int ax, int ay, int awidth, int aheight) : base(ax, ay)
    {
        width = awidth;
        height = aheight;
        myPath.AddEllipse(x, y, width, height);
    }

    public override void move(int dx, int dy)
    {
        x += dx;
        y += dy;
        myPath.Reset();
        myPath.AddEllipse(x, y, width, height);
    }
    public override bool checkBounds(int dx, int dy, int Fheight, int Fwidth)
    {
        if (x + dx < 0 || y + dy < 0 || x + width + dx > Fwidth || y + height + dy > Fheight)
            return false;
        return true;
    }
    public override void save(StreamWriter sw)
    {
        sw.WriteLine("C");
        sw.WriteLine(x.ToString() + "\n" + y.ToString() + "\n" + width.ToString() + "\n" + height.ToString());
    }
    public override void load(StreamReader rw, Factory factory)
    {
        x = int.Parse(rw.ReadLine());
        y = int.Parse(rw.ReadLine());
        width = int.Parse(rw.ReadLine());
        height = int.Parse(rw.ReadLine());
        myPath.Reset();
        myPath.AddEllipse(x, y, width, height);
    }
}

class CRectangle : Shape
{
    private int width;
    private int height;
    public CRectangle() : base()
    {
        width = 10;
        height = 10;
        myPath.AddRectangle(new Rectangle(x, y, width, height));
    }
    public CRectangle(int ax, int ay, int dx, int dy) : base(ax, ay)
    {
        myPath = new GraphicsPath();
        CurrentPen = UncheckedPen;
        width = dx;
        height = dy;
        myPath.AddRectangle(new Rectangle(x, y, width, height));
    }
    public override void move(int dx, int dy)
    {
        x += dx;
        y += dy;
        myPath.Reset();
        myPath.AddRectangle(new Rectangle(x, y, width, height));
    }
    public override bool checkBounds(int dx, int dy, int Fheight, int Fwidth)
    {
        if (x + dx < 0 || y + dy < 0 || x + width + dx > Fwidth || y + height + dy > Fheight)
            return false;
        return true;
    }
    public override void save(StreamWriter sw)
    {
        sw.WriteLine("R");
        sw.WriteLine(x.ToString() + "\n" + y.ToString() + "\n" + width.ToString() + "\n" + height.ToString());
    }
    public override void load(StreamReader rw, Factory factory)
    {
        x = int.Parse(rw.ReadLine());
        y = int.Parse(rw.ReadLine());
        width = int.Parse(rw.ReadLine());
        height = int.Parse(rw.ReadLine());
        myPath.Reset();
        myPath.AddRectangle(new Rectangle(x, y, width, height));
    }
}

class CTriangle : Shape
{
    private Point[] Points = new Point[4];
    public CTriangle() : base()
    {
    }
    public CTriangle(Point a, Point b, Point c)
    {
        CurrentPen = UncheckedPen;
        Points[0] = a;
        Points[1] = b;
        Points[2] = c;
        Points[3] = a;
        myPath = new GraphicsPath();
        myPath.AddLines(Points);
    }
    public override void move(int dx, int dy)
    {
        for (int i = 0; i < 4; i++)
        {
            Points[i].X += dx;
            Points[i].Y += dy;
        }
        myPath.Reset();
        myPath.AddLines(Points);
    }
    public override bool checkBounds(int dx, int dy, int Fheight, int Fwidth)
    {
        for (int i = 0; i < 4; i++)
        {
            if (Points[i].X + dx < 0 || Points[i].Y + dy < 0 || Points[i].X + dx > Fwidth || Points[i].Y + dy > Fheight)
                return false;
        }
        return true;
    }
    public override void save(StreamWriter sw)
    {
        sw.WriteLine("T");
        for (int i = 0; i < 3; i++)
            sw.WriteLine(Points[i].X.ToString() + "\n" + Points[i].Y.ToString());
    }
    public override void load(StreamReader rw, Factory factory)
    {
        for (int i = 0; i < 3; i++)
        {
            Points[i].X = int.Parse(rw.ReadLine());
            Points[i].Y = int.Parse(rw.ReadLine());
        }
        Points[3] = Points[0];
        myPath.Reset();
        myPath.AddLines(Points);
    }
}
class Line : Shape
{
    private int bx, by;
    public Line() : base()
    {
    }
    public Line(int ax, int ay, int abx, int aby) : base(ax, ay)
    {
        CurrentPen = UncheckedPen;
        bx = abx;
        by = aby;
        myPath = new GraphicsPath();
        myPath.AddLine(x, y, bx, by);
    }
    public override void move(int dx, int dy)
    {
        x += dx;
        bx += dx;
        y += dy;
        by += dy;
        myPath.Reset();
        myPath.AddLine(x, y, bx, by);

    }
    public override bool checkBounds(int dx, int dy, int Fheight, int Fwidth)
    {
        if (x + dx > Fwidth || x + dx < 0 || y + dy > Fheight || y + dy < 0)
            return false;
        if (bx + dx > Fwidth || bx + dx < 0 || by + dy > Fheight || by + dy < 0)
            return false;
        return true;
    }
    public override void save(StreamWriter sw)
    {
        sw.WriteLine("L");
        sw.WriteLine(x.ToString() + "\n" + y.ToString());
        sw.WriteLine(bx.ToString() + "\n" + by.ToString());
    }
    public override void load(StreamReader rw, Factory factory)
    {
        myPath = new GraphicsPath();
        x = int.Parse(rw.ReadLine());
        y = int.Parse(rw.ReadLine());
        bx = int.Parse(rw.ReadLine());
        by = int.Parse(rw.ReadLine());
        myPath.Reset();
        myPath.AddLine(x, y, bx, by);
    }
}

class Group : Shape
{
    private int maxcount;
    private int count;
    private bool CurrentState;
    Shape[] arr;
    public Group()
    {

    }
    public Group(int mx)
    {
        maxcount = mx;
        count = 0;
        arr = new Shape[maxcount];
        for (int i = 0; i < maxcount; i++)
            arr[i] = null;
        CurrentState = false;
    }
    public bool addShape(Shape s1)
    {
        if (count >= maxcount)
            return false;
        arr[count] = s1;
        count++;
        return true;
    }
    public override bool checkBounds(int dx, int dy, int Fheight, int Fwidth)
    {
        for (int i = 0; i < count; i++)
            if (arr[i].checkBounds(dx, dy, Fheight, Fwidth) == false)
                return false;
        return true;
    }
    public override void move(int dx, int dy)
    {
        for (int i = 0; i < count; i++)
            arr[i].move(dx, dy);
    }
    public override void draw(PaintEventArgs e)
    {
        for (int i = 0; i < count; i++)
            arr[i].draw(e);
    }
    public override bool getCurrentState()
    {
        return CurrentState;
    }
    public override void changeState()
    {
        if (CurrentState == false)
        {
            for (int i = 0; i < count; i++)
            {
                if (arr[i].getCurrentState() == false)
                    arr[i].changeState();
            }
            CurrentState = true;
        }
        else
        {
            for (int i = 0; i < count; i++)
            {
                if (arr[i].getCurrentState() == true)
                    arr[i].changeState();
            }
            CurrentState = false;
        }
    }
    public override bool isClicked(int ax, int ay)
    {
        for (int i = 0; i < count; i++)
            if (arr[i].isClicked(ax, ay) == true)
                return true;
        return false;
    }
    public Shape getObject(int i)
    {
        return arr[i];
    }
    public int getSize()
    {
        return count;
    }
    public override void save(StreamWriter sw)
    {
        sw.WriteLine("G");
        sw.WriteLine(count.ToString());
        for (int i = 0; i < count; i++)
            arr[i].save(sw);
    }
    public override void load(StreamReader rw, Factory factory)
    {
        count = int.Parse(rw.ReadLine());
        string type = string.Empty;
        arr = new Shape[count];
        for (int i = 0; i < count; i++)
        {
            type = rw.ReadLine();
            arr[i] = factory.create(type);
            if (arr[i] != null)
                arr[i].load(rw, factory);
        }
    }
}