using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WindowsFormsApp8;

public abstract class Command
{
    protected Storage mystorage;
    protected Form1 myform;
    protected bool isSuccess;
    public Command(Storage stor, Form1 form)
    {
        isSuccess = false;
        mystorage = stor;
        myform = form;
    }
    public virtual bool execute() { return false; }
    public virtual void unexecute() { }
    public virtual Command clone() { return null; }
    ~Command()
    {

    }
}

class MoveCommand : Command
{
    private List<Shape> temp;
    private int dx;
    private int dy;
    public MoveCommand(Storage stor, Form1 form, int adx, int ady) : base(stor, form)
    {
        temp = new List<Shape>();
        dx = adx;
        dy = ady;
    }
    public override bool execute()
    {
        int Fheight = myform.getHeight();
        int Fwidth = myform.getWidth();
        for (int i = 0; i < mystorage.getSize(); i++)
            if (mystorage.getObject(i) != null && mystorage.getObject(i).getCurrentState()
                && (mystorage.getObject(i).checkBounds(dx, dy, Fheight, Fwidth) || myform.isMoved))
            {
                mystorage.getObject(i).move(dx, dy);
                isSuccess = true;
                temp.Add(mystorage.getObject(i));
            }
        return isSuccess;
    }
    public override void unexecute()
    {
        foreach (Shape s in temp)
            s.move(-dx, -dy);
    }
    public override Command clone()
    {
        return new MoveCommand(mystorage, myform, dx, dy);
    }
    ~MoveCommand()
    {
        temp.Clear();
    }
}

class DeleteCommand : Command
{
    private List<Shape> temp;
    public DeleteCommand(Storage stor, Form1 form) : base(stor, form)
    {
        temp = new List<Shape>();
    }
    public override bool execute()
    {
        for (int i = 0; i < mystorage.getSize(); i++)
            if (mystorage.getObject(i) != null && mystorage.getObject(i).getCurrentState())
            {
                mystorage.deleteObj(i);
                isSuccess = true;
                temp.Add(mystorage.getObject(i));
            }
        return isSuccess;
    }
    public override void unexecute()
    {
        foreach (Shape s in temp)
        {
            mystorage.addObj(s, 0);
        }
    }
    public override Command clone()
    {
        return new DeleteCommand(mystorage, myform);
    }
    ~DeleteCommand()
    {
        temp.Clear();
    }
}

class GroupCommand : Command
{
    private Group newgroup;
    public GroupCommand(Storage stor, Form1 form) : base(stor, form)
    {
        newgroup = new Group(100);
    }
    public override bool execute()
    {
        for (int i = 0; i < mystorage.getSize(); i++)
            if (mystorage.getObject(i) != null && mystorage.getObject(i).getCurrentState())
            {
                newgroup.addShape(mystorage.getObject(i));
                mystorage.deleteObj(i);
                isSuccess = true;
            }
        if (isSuccess)
        {
            mystorage.addObj(newgroup, 0);
            newgroup.changeState();
        }
        return isSuccess;
    }
    public override void unexecute()
    {
        for (int i = 0; i < mystorage.getSize(); i++)
            if (mystorage.getObject(i) != null && mystorage.getObject(i) == newgroup)
                mystorage.deleteObj(i);
        for (int i = 0; i < newgroup.getSize(); i++)
        {
            mystorage.addObj(newgroup.getObject(i), 0);
        }
        newgroup = null;
    }
    public override Command clone()
    {
        return new GroupCommand(mystorage, myform);
    }
}

class UngroupCommand : Command
{
    private Group temp;
    public UngroupCommand(Storage stor, Form1 form) : base(stor, form)
    {

    }
    public override bool execute()
    {
        for (int i = 0; i < mystorage.getSize(); i++)
            if (mystorage.getObject(i) != null && mystorage.getObject(i).getCurrentState()
                && mystorage.getObject(i) is Group)
            {
                temp = mystorage.getObject(i) as Group;
                mystorage.deleteObj(i);
                isSuccess = true;
            }
        if (isSuccess)
        {
            for (int i = 0; i < temp.getSize(); i++)
                mystorage.addObj(temp.getObject(i), 0);
        }
        return true;
    }
    public override void unexecute()
    {
        for (int i = 0; i < temp.getSize(); i++)
            mystorage.deleteObj(temp.getObject(i));
        mystorage.addObj(temp, 0);
    }
    public override Command clone()
    {
        return new UngroupCommand(mystorage, myform);
    }
    ~UngroupCommand()
    {
        temp = null;
    }
}

class AddShapeCommand : Command
{
    private Shape s1;
    public AddShapeCommand(Storage stor, Form1 form) : base(stor, form)
    {
        s1 = null;
    }
    public override bool execute()
    {
        Point First = myform.getClick();
        Point Second = myform.getUpClick();
        if (myform.currFig == 3) // Линия
            s1 = new Line(First.X, First.Y, Second.X, Second.Y);
        if (myform.currFig == 4) //Треугольник
        {
            s1 = new CTriangle(myform.getFirst(), myform.getSecond(), myform.getThird());
        }
        if (myform.currFig == 1) // Круг
        {
            if (First.X < Second.X) // Если начинаем слева направо
                if (First.Y < Second.Y) // Сверху вниз
                    s1 = new Circle(First.X, First.Y, Math.Abs(Second.X - First.X), Math.Abs(Second.Y - First.Y));
                else //Снизу вверх
                    s1 = new Circle(First.X, Second.Y, Math.Abs(Second.X - First.X), Math.Abs(Second.Y - First.Y));
            else // Справа налево
                if (First.Y < Second.Y) // Сверху вниз
                s1 = new Circle(Second.X, First.Y, Math.Abs(Second.X - First.X), Math.Abs(Second.Y - First.Y));
            else // Снизу вверх
                s1 = new Circle(Second.X, Second.Y, Math.Abs(Second.X - First.X), Math.Abs(Second.Y - First.Y));
        }
        if (myform.currFig == 2) // Прямоугольник
        {
            if (First.X < Second.X) // Если начинаем слева направо
                if (First.Y < Second.Y) // Сверху вниз
                    s1 = new CRectangle(First.X, First.Y, Math.Abs(Second.X - First.X), Math.Abs(Second.Y - First.Y));
                else //Снизу вверх
                    s1 = new CRectangle(First.X, Second.Y, Math.Abs(Second.X - First.X), Math.Abs(Second.Y - First.Y));
            else // Справа налево
                if (First.Y < Second.Y) // Сверху вниз
                s1 = new CRectangle(Second.X, First.Y, Math.Abs(Second.X - First.X), Math.Abs(Second.Y - First.Y));
            else // Снизу вверх
                s1 = new CRectangle(Second.X, Second.Y, Math.Abs(Second.X - First.X), Math.Abs(Second.Y - First.Y));
        }
        if (s1 != null)
        {
            mystorage.addObj(s1, 0);
            isSuccess = true;
        }
        return isSuccess;
    }
    public override void unexecute()
    {
        mystorage.deleteObj(s1);
    }
    public override Command clone()
    {
        return new AddShapeCommand(mystorage, myform);
    }
    ~AddShapeCommand()
    {
        s1 = null;
    }
}

class SelectCommand : Command
{
    private List<Shape> temp;
    private Point click;
    public SelectCommand(Storage stor, Form1 form) : base(stor, form)
    {
        temp = new List<Shape>();
        click = myform.getClick();
    }
    public override bool execute()
    {
        for (int i = 0; i < mystorage.getSize(); i++)
            if (mystorage.getObject(i) != null && mystorage.getObject(i).isClicked(click.X, click.Y))
            {
                mystorage.getObject(i).changeState();
                isSuccess = true;
                temp.Add(mystorage.getObject(i));
                break;
            }
        return isSuccess;
    }
    public override void unexecute()
    {
        foreach (Shape s in temp)
            s.changeState();
    }
    public override Command clone()
    {
        return new SelectCommand(mystorage, myform);
    }
}

class MouseMoveCommand : Command
{
    private Point First;
    private Point Second;
    private Point LastPos;
    private Shape s1;
    public MouseMoveCommand(Storage stor, Form1 form) : base(stor, form)
    {
        First = myform.getClick();
        Second = myform.getMoveP();
        LastPos = myform.getLastPos();
    }
    public override bool execute()
    {
        //Движение фигур мышью
        bool isMove = false;
        for (int i = 0; i < mystorage.getSize(); i++) //Проверка на клик по выделенной фигуре
            if (mystorage.getObject(i) != null
                && mystorage.getObject(i).isClicked(Second.X, Second.Y)
                && mystorage.getObject(i).getCurrentState())
            {
                isMove = true;
                break;
            }

        if (isMove) //Движение всех фигур
        {
            bool isPose = false;
            for (int i = 0; i < mystorage.getSize(); i++)
                if (mystorage.getObject(i) != null
                    && mystorage.getObject(i).getCurrentState()
                    && mystorage.getObject(i).checkBounds(Second.X - LastPos.X,
                    Second.Y - LastPos.Y, myform.Height, myform.Width))
                {
                    isPose = true;
                    mystorage.getObject(i).move(Second.X - LastPos.X, Second.Y - LastPos.Y);
                    myform.isMoved = true;
                }
            if(isPose)
                myform.setLastPos(Second);
        }
        //Предварительное рисование фигур при создании
        if (myform.isMoved != true)
        {
            if (myform.currFig == 3) // Линия
                s1 = new Line(First.X, First.Y, Second.X, Second.Y);
            if (myform.currFig == 1) // Круг
            {
                if (First.X < Second.X) // Если начинаем слева направо
                    if (First.Y < Second.Y) // Сверху вниз
                        s1 = new Circle(First.X, First.Y, Math.Abs(Second.X - First.X), Math.Abs(Second.Y - First.Y));
                    else //Снизу вверх
                        s1 = new Circle(First.X, Second.Y, Math.Abs(Second.X - First.X), Math.Abs(Second.Y - First.Y));
                else // Справа налево
                    if (First.Y < Second.Y) // Сверху вниз
                    s1 = new Circle(Second.X, First.Y, Math.Abs(Second.X - First.X), Math.Abs(Second.Y - First.Y));
                else // Снизу вверх
                    s1 = new Circle(Second.X, Second.Y, Math.Abs(Second.X - First.X), Math.Abs(Second.Y - First.Y));
            }
            if (myform.currFig == 2) // Прямоугольник
            {
                if (First.X < Second.X) // Если начинаем слева направо
                    if (First.Y < Second.Y) // Сверху вниз
                        s1 = new CRectangle(First.X, First.Y, Math.Abs(Second.X - First.X), Math.Abs(Second.Y - First.Y));
                    else //Снизу вверх
                        s1 = new CRectangle(First.X, Second.Y, Math.Abs(Second.X - First.X), Math.Abs(Second.Y - First.Y));
                else // Справа налево
                    if (First.Y < Second.Y) // Сверху вниз
                    s1 = new CRectangle(Second.X, First.Y, Math.Abs(Second.X - First.X), Math.Abs(Second.Y - First.Y));
                else // Снизу вверх
                    s1 = new CRectangle(Second.X, Second.Y, Math.Abs(Second.X - First.X), Math.Abs(Second.Y - First.Y));
            }
            myform.tmps.addObj(s1, 0);
        }
        return isSuccess;
    }
    public override Command clone()
    {
        return new MouseMoveCommand(mystorage, myform);
    }
}

class SubscribeCommand : Command
{
    private IObservable obs;
    private IObserver sub;
    public SubscribeCommand(Storage stor, Form1 form, IObservable observable, IObserver subscriber) : base(stor, form)
    {
        obs = observable;
        sub = subscriber;
    }
    public override bool execute()
    {
        obs.AddObserver(sub);
        isSuccess = true;
        return isSuccess;
    }
    public override void unexecute()
    {
        obs.RemoveObserver(sub);
    }
    public override Command clone()
    {
        return new SubscribeCommand(mystorage, myform, obs, sub);
    }
}

class UnsubscribeCommand : Command
{
    private IObservable obs;
    private IObserver sub;
    public UnsubscribeCommand(Storage stor, Form1 form, IObservable observable, IObserver subscriber) : base(stor, form)
    {
        obs = observable;
        sub = subscriber;
    }
    public override bool execute()
    {
        obs.RemoveObserver(sub);
        isSuccess = true;
        return isSuccess;
    }
    public override void unexecute()
    {
        obs.AddObserver(sub);
    }
    public override Command clone()
    {
        return new UnsubscribeCommand(mystorage, myform, obs, sub);
    }
}