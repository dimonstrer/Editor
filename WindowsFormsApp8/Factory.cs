using System;

public abstract class Factory
{
    public Factory()
    {

    }
    public virtual Shape create(string s) { return null; }

}

class ShapesFactory : Factory
{
    public ShapesFactory()
    {

    }
    public override Shape create(string s)
    {
        Shape s1 = null;
        switch(s)
        {
            case "C":
                s1 = new Circle();
                break;
            case "T":
                s1 = new CTriangle();
                break;
            case "L":
                s1 = new Line();
                break;
            case "R":
                s1 = new CRectangle();
                break;
            case "G":
                s1 = new Group();
                break;
        }
        return s1;
    }
}