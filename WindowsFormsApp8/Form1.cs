using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApp8
{
    public partial class Form1 : Form, IObserver
    {
        public Storage tmps = new Storage(1);
        public int currFig;
        public bool isMoved = false;
        // 1 - Circle
        // 2 - Rectangle
        // 3 - Line
        // 4 - Triangle
        int Triang;
        Storage s1 = new Storage(10);
        Point click = new Point();
        Point upclick = new Point();
        Point LastPos = new Point();
        Point moveP = new Point();
        Point First;
        Point Second = new Point();
        Point Third = new Point();
        Dictionary<String, Command> commands = new Dictionary<String, Command>();
        Stack<Command> history = new Stack<Command>();
        public Form1()
        {
            InitializeComponent();
            commands[Keys.Right.ToString()] = new MoveCommand(s1, this, 10, 0);
            commands[Keys.Left.ToString()] = new MoveCommand(s1, this, -10, 0);
            commands[Keys.Up.ToString()] = new MoveCommand(s1, this, 0, -10);
            commands[Keys.Down.ToString()] = new MoveCommand(s1, this, 0, 10);
            commands[Keys.Delete.ToString()] = new DeleteCommand(s1, this);
            commands["Group"] = new GroupCommand(s1, this);
            commands["Ungroup"] = new UngroupCommand(s1, this);
            commands["AddShape"] = new AddShapeCommand(s1, this);
            commands["Select"] = new SelectCommand(s1, this);
            commands["MouseMove"] = new MouseMoveCommand(s1, this);
            currFig = 0;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            for (int i = 0; i < s1.getSize(); i++) // Рисуем всё
                if (s1.getObject(i) != null)
                    s1.getObject(i).draw(e);
            if (tmps.getObject(0) != null)
            {
                tmps.getObject(0).draw(e);
                tmps.deleteObj(0);
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            click = e.Location;
            LastPos = click;
            if (e.Button == MouseButtons.Left&& TriangleButt.Checked)
                    if (Triang == 3)
                        Third = e.Location;
                    if (Triang == 2)
                        Second = e.Location;
                    if (Triang == 1)
                        First = e.Location;
            if (e.Button == MouseButtons.Right) // Правая кнопка
                checkCommands("Select");
        }

        private void myPic_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                moveP = e.Location;
                checkCommands("MouseMove");
                if (isMoved)
                    myPic.Invalidate();
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            upclick = e.Location;
            if (isMoved)
            {
                checkCommands("Move");
                isMoved = false;
                return;
            }
            if (e.Button == MouseButtons.Left)
            {
                if (e.X < 0 || e.Y < 0 || e.X > myPic.Width || e.Y > myPic.Height)
                    return;
                if (TriangleButt.Checked)
                {
                    if (Triang == 2)
                    {
                        Triang++;
                        return;
                    }
                    if (Triang == 1)
                    {
                        Triang++;
                        return;
                    }
                    Triang = 1;
                }
                checkCommands("AddShape");
            }
        }

        private void unCheckButt(object sender) // Выбор единственного в тулстрипе
        {
            foreach (ToolStripItem item in toolStrip1.Items)
            {
                if ((item != sender) && (item is ToolStripButton))
                    ((ToolStripButton)item).Checked = false;
            }
        }

        private void CircleButt_Click(object sender, EventArgs e) // Выбор круга
        {
            unCheckButt(sender);
            CircleButt.Checked = true;
            currFig = 1;
        }

        private void RectButt_Click(object sender, EventArgs e) // Выбор прямоугольника
        {
            unCheckButt(sender);
            RectButt.Checked = true;
            currFig = 2;
        }

        private void LineButt_Click(object sender, EventArgs e) // Выбор линии
        {
            unCheckButt(sender);
            LineButt.Checked = true;
            currFig = 3;
        }

        private void TriangleButt_Click(object sender, EventArgs e) // Выбор треугольника
        {
            unCheckButt(sender);
            TriangleButt.Checked = true;
            currFig = 4;
            Triang = 1;
        }

        private void GroupButt_Click(object sender, EventArgs e)
        {
            checkCommands("Group");
        }

        private void UngroupButt_Click(object sender, EventArgs e)
        {
            checkCommands("Ungroup");
        }

        private void undo()
        {
            Command lastcommand = history.Pop();
            lastcommand.unexecute();
            myPic.Invalidate();
            if (history.Count == 0)
                UndoB.Enabled = false;
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Z && !(history.Count == 0)) //Операция возврата
            {
                undo();
                return;
            }
            checkCommands(e.KeyCode.ToString());
        }

        private void LoadButt_Click(object sender, EventArgs e)
        {
            Factory fact = new ShapesFactory();
            var filePath = string.Empty;
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Txt files (*.txt)|*.txt|All files (*.*)|*.*";
            if (open.ShowDialog() == DialogResult.OK)
                filePath = open.FileName;
            else return;
            using (StreamReader inputFile = new StreamReader(filePath))
            {
                s1.load(inputFile, fact);
                inputFile.Close();
            }
            myPic.Invalidate();
        }

        private void SaveButt_Click(object sender, EventArgs e)
        {
            string myPath = string.Empty;
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            save.FilterIndex = 2;
            save.RestoreDirectory = true;
            if (save.ShowDialog() == DialogResult.OK)
                myPath = save.FileName;
            else
                return;
            if (!myPath.Contains(".txt"))
                myPath += ".txt";
            using (StreamWriter outputFile = new StreamWriter(myPath))
            {
                s1.save(outputFile);
                outputFile.Flush();
                outputFile.Close();
            }
        }

        private void UndoB_Click(object sender, EventArgs e)
        {
            undo();
        }

        private void checkCommands(string s)
        {
            Command command;
            if (s == "Move")
                command = new MoveCommand(s1, this, LastPos.X - click.X, LastPos.Y - click.Y);
            else
                commands.TryGetValue(s, out command);
            if (command != null)
            {
                Command newcommand = command.clone();
                if (newcommand.execute())
                {
                    if (s == "Move")
                        newcommand.unexecute();
                    history.Push(newcommand);
                    UndoB.Enabled = true;
                }
                myPic.Invalidate();
            }
        }
        public Point getClick()
        {
            return click;
        }
        public Point getUpClick()
        {
            return upclick;
        }
        public Point getFirst()
        {
            return First;
        }
        public Point getSecond()
        {
            return Second;
        }
        public Point getThird()
        {
            return Third;
        }
        public Point getMoveP()
        {
            return moveP;
        }
        public Point getLastPos()
        {
            return LastPos;
        }
        public void setLastPos(Point last)
        {
            LastPos = last;
        }
        public int getWidth()
        {
            return myPic.Width;
        }
        public int getHeight()
        {
            return myPic.Height;
        }

        public void Update(string type, object obj)
        {
            
        }

        private void myPic_Click(object sender, EventArgs e)
        {

        }
    }
}