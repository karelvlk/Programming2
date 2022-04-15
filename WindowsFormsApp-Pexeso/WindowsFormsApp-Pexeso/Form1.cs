using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp_Pexeso
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        void HideStartScreenElements()
        {
            startButton.Visible = false;
            arrowLabel.Visible = false;
            numericUpDown.Visible = false;
            opponent.Visible = true;
            opponentScore.Visible = true;
            you.Visible = true;
            youScore.Visible = true;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            HideStartScreenElements();
            Game game = new Game(this);
            game.StartGame();
        }

        public NumericUpDown GetNumericUpDown()
        {
            return this.numericUpDown;
        }

        public Label GetYourScore()
        {
            return this.youScore;
        }

        public Label GetOpponentScore()
        {
            return this.opponentScore;
        }

        public Label GetEndLabel()
        {
            return this.endLabel;
        }

        public Label GetHeading()
        {
            return this.heading;
        }
    }
    class GameGenerator
    {
        Form form;
        Game game;
        int pairs = 0;
        public GameGenerator(Form form, Game game)
        {
            this.form = form;
            this.game = game;
        }
        List<string> GenerateColors(int numberOfPairs)
        {
            string[] colors = new string[]{
                "#000000", "#FFFF00", "#1CE6FF", "#FF34FF", "#FF4A46", "#008941", "#006FA6", "#A30059",
                "#FFDBE5", "#7A4900", "#0000A6", "#63FFAC", "#B79762", "#004D43", "#8FB0FF", "#997D87",
                "#5A0007", "#809693", "#FEFFE6", "#1B4400", "#4FC601", "#3B5DFF", "#4A3B53", "#FF2F80",
                "#61615A", "#BA0900", "#6B7900", "#00C2A0", "#FFAA92", "#FF90C9", "#B903AA", "#D16100",
                "#DDEFFF", "#000035", "#7B4F4B", "#A1C299", "#300018", "#0AA6D8", "#013349", "#00846F",
                "#372101", "#FFB500", "#C2FFED", "#A079BF", "#CC0744", "#C0B9B2", "#C2FF99", "#001E09",
                "#00489C", "#6F0062", "#0CBD66", "#EEC3FF", "#456D75", "#B77B68", "#7A87A1", "#788D66",
                "#885578", "#FAD09F", "#FF8A9A", "#D157A0", "#BEC459", "#456648", "#0086ED", "#886F4C",

                "#34362D", "#B4A8BD", "#00A6AA", "#452C2C", "#636375", "#A3C8C9", "#FF913F", "#938A81",
                "#575329", "#00FECF", "#B05B6F", "#8CD0FF", "#3B9700", "#04F757", "#C8A1A1", "#1E6E00",
                "#7900D7", "#A77500", "#6367A9", "#A05837", "#6B002C", "#772600", "#D790FF", "#9B9700",
                "#549E79", "#FFF69F", "#201625", "#72418F", "#BC23FF", "#99ADC0", "#3A2465", "#922329",
                "#5B4534", "#FDE8DC", "#404E55", "#0089A3", "#CB7E98", "#A4E804", "#324E72", "#6A3A4C",
                "#83AB58", "#001C1E", "#D1F7CE", "#004B28", "#C8D0F6", "#A3A489", "#806C66", "#222800",
                "#BF5650", "#E83000", "#66796D", "#DA007C", "#FF1A59", "#8ADBB4", "#1E0200", "#5B4E51",
                "#C895C5", "#320033", "#FF6832", "#66E1D3", "#CFCDAC", "#D0AC94", "#7ED379", "#012C58"
                };
            List<string> colorList = new List<string>();

            Random rnd = new Random();
            for (int i = 0; i < numberOfPairs;)
            {
                int idx = rnd.Next(128);
                Console.WriteLine(idx);
                if (colors[idx] != "x")
                {
                    colorList.Add(String.Copy(colors[idx]));
                    colorList.Add(String.Copy(colors[idx]));
                    colors[idx] = "x";
                    i++;
                }
            }

            return colorList;
        }

        int getClosest2Power(int n)
        {
            int biggest = 1;
            for (int i = 1; i < n; i++)
            {
                if (i * i > n)
                {
                    return biggest;
                }
                else if (i * i > biggest * biggest)
                {
                    biggest = i;
                }
            }
            return biggest;
        }

        public Entity[] GenerateButtons()
        {
            int numberOfPairs = (int)((Form1)this.form).GetNumericUpDown().Value;
            this.pairs = numberOfPairs;
            int left = 0;
            int top = 0;
            int pow = getClosest2Power(numberOfPairs * 2);
            List<string> colors = GenerateColors(numberOfPairs);
            Random rnd = new Random();
            Entity[] entityList = new Entity[numberOfPairs*2];
            for (int i = 0; i < numberOfPairs * 2; i++)
            {
                if (left == pow)
                {
                    top++;
                    left = 0;
                }

                int idx = rnd.Next((numberOfPairs * 2) - i);
                string c = colors.ElementAt(idx);
                Entity ent = new Entity(c, left * 130, top * 130, this.game);
                entityList[i] = ent;
                colors.RemoveAt(idx);
                ent.CreateFormElement(this.form);
                left++;
            }

            return entityList;
        }
        public int GetNumberOfPairs()
        {
            return this.pairs;
        }
    }

    class Game
    {
        int pickID = -1;
        int pickID2 = -1;
        int youScore = 0;
        int opponentScore = 0;
        Entity firstPicked = null;
        Entity secondPicked = null;
        int pairsLeft = 0;
        Form form;
        GameGenerator gameGenerator;
        Entity[] entityList;
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

        public Game(Form form)
        {
            this.form = form;
        }

        public void StartGame()
        {
            gameGenerator = new GameGenerator(form, this);
            this.entityList = gameGenerator.GenerateButtons();
            this.pairsLeft = gameGenerator.GetNumberOfPairs();
            this.ProcessYourTurn();
        }

        void ProcessEnd()
        {
            this.EnableEntityClicking(false);
            if (youScore == opponentScore)
            {
                ((Form1)this.form).GetEndLabel().Text = "IT IS TIE!";
            } else if (youScore > opponentScore)
            {
                ((Form1)this.form).GetEndLabel().Text = "YOU WIN!";
            } else
            {
                ((Form1)this.form).GetEndLabel().Text = "BOB WINS!";
            }

            ((Form1)this.form).GetEndLabel().Visible = true;
            ((Form1)this.form).GetHeading().Text = "PEXESO";
        }

        public void ProcessClick(string color, Entity entity) // player click
        {
            if (pairsLeft == 0)
            {
                ProcessEnd();
            }
            else
            {
                if (this.firstPicked == null)
                {
                    this.firstPicked = entity;
                }
                else
                {
                    this.secondPicked = firstPicked;
                    this.firstPicked = entity;

                    timer.Interval = 2000;
                    timer.Enabled = true;
                    timer.Tick += HandleTimerYou;
                }
            }
        }

        void HandleTimerYou(Object source, EventArgs e)
        {
            timer.Enabled = false;
            timer.Tick -= HandleTimerYou;


            if (this.CompareChoosenColors(firstPicked.GetColor(), secondPicked.GetColor()))
            {
                this.firstPicked.HideElement();
                this.secondPicked.HideElement();
                this.youScore++;
                ((Form1)this.form).GetYourScore().Text = this.youScore.ToString();
                this.pairsLeft--;

                this.firstPicked = null;
                this.secondPicked = null;
                this.ProcessYourTurn();
            }
            else
            {
                this.firstPicked.TurnBackSideUp();
                this.secondPicked.TurnBackSideUp();

                this.firstPicked = null;
                this.secondPicked = null;
                this.ProcessBobsTurn();
            }
        }

        void ProcessBobsTurn()
        {
            if (pairsLeft == 0)
            {
                ProcessEnd();
            }
            else
            {
                this.EnableEntityClicking(false);
                ((Form1)this.form).GetHeading().Text = "BOB'S TURN";
                Random rnd = new Random();
                this.pickID = -1;
                while (this.pickID == -1 || !this.entityList[this.pickID].IsVisible())
                {
                    this.pickID = rnd.Next(entityList.Length - 1);
                }
                this.firstPicked = this.entityList[this.pickID];
                this.firstPicked.TurnUpSideUp();

                this.pickID2 = -1;
                while (this.pickID2 == -1 || !this.entityList[this.pickID2].IsVisible() || this.pickID == this.pickID2)
                {
                    this.pickID2 = rnd.Next(entityList.Length);
                }

                this.secondPicked = this.entityList[this.pickID2];
                this.secondPicked.TurnUpSideUp();

                timer.Interval = 2000;
                timer.Enabled = true;
                timer.Tick += HandleTimerOpponent;
            }
        }

        void HandleTimerOpponent(Object source, EventArgs e)
        {
            timer.Enabled = false;
            timer.Tick -= HandleTimerOpponent;

            if (this.CompareChoosenColors(firstPicked.GetColor(), secondPicked.GetColor()))
            {
                firstPicked.HideElement();
                secondPicked.HideElement();
                this.opponentScore++;
                ((Form1)this.form).GetOpponentScore().Text = this.opponentScore.ToString();
                this.pairsLeft--;

                this.firstPicked = null;
                this.secondPicked = null;
                this.pickID = -1;
                this.pickID2 = -1;
                this.ProcessBobsTurn();
            }
            else
            {
                firstPicked.TurnBackSideUp();
                secondPicked.TurnBackSideUp();

                this.firstPicked = null;
                this.secondPicked = null;
                this.pickID = -1;
                this.pickID2 = -1;
                this.ProcessYourTurn();
            }
            //this.pickID = -1;
            //firstPicked = null;
            //secondPicked = null;
            this.ProcessYourTurn();
        }

        void EnableEntityClicking(bool boolean)
        {
            for (int i = 0; i < entityList.Length; i++)
            {
                this.entityList[i].setEnable(boolean);
            }
        }

        void ProcessYourTurn()
        {
            if (pairsLeft == 0)
            {
                ProcessEnd();
            } else
            {
                this.EnableEntityClicking(true);
                ((Form1)this.form).GetHeading().Text = "YOUR TURN";
            }
        }

        bool CompareChoosenColors(string color1, string color2)
        {
            if (color1 == color2)
            {
                return true;
            } else
            {
                return false;
            }
        }
    }

    class Entity
    {
        string color;
        bool isShown;
        int left = 20;
        int top = 120;
        bool isEnabled = true;
        Label element;
        Game game;
        public Entity(string color, int leftOffset, int topOffset, Game game)
        {
            this.color = color;
            this.isShown = false;
            this.left += leftOffset;
            this.top += topOffset;
            this.game = game;
        }

        public void CreateFormElement(Form form)
        {
            this.element = new Label();
            this.element.AutoSize = true;
            this.element.BackColor = System.Drawing.Color.DarkGray;
            this.element.Font = new System.Drawing.Font("Yu Gothic UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.element.Location = new System.Drawing.Point(269, 139);
            this.element.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.element.Name = "element";
            this.element.Size = new System.Drawing.Size(100, 100);
            this.element.TextAlign = ContentAlignment.MiddleCenter;
            this.element.AutoSize = false;
            this.element.TabIndex = 4;
            this.element.Text = "Otoc me!";
            this.element.Left = this.left;
            this.element.Top = this.top;
            this.element.Click += new System.EventHandler(this.ProcessClick);
            form.Controls.Add(this.element);
        }
        public bool IsVisible()
        {
            return this.element.Visible;
        }

        public void TurnUpSideUp()
        {
            this.element.BackColor = ColorTranslator.FromHtml(this.color);
            this.element.Text = "";
        }

        void ProcessClick(object sender, System.EventArgs e)
        {
            if (this.isEnabled)
            {
                this.isShown = !this.isShown;
                if (this.isShown)
                {
                    this.TurnUpSideUp();
                    this.game.ProcessClick(this.GetColor(), this);
                }
            }

        }

        public void setEnable(bool boolean)
        {
            this.isEnabled = boolean;
        }

        public void HideElement()
        {
            this.element.Visible = false;
        }

        public void TurnBackSideUp()
        {
            this.element.BackColor = System.Drawing.Color.DarkGray;
            this.element.Text = "Otoc me!";
        }

        public string GetColor()
        {
            return color;
        }
    }

    
}
