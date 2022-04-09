using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp_Pexeso
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        class Logic
        {
            int youScore = 0;
            int opponentScore = 0;
            string firstPicked = "x";
            string secondPicked = "x";
            bool isBobsTurn = false;
            Form form;

            public Logic(Form form)
            {
                this.form = form;
            }
            void IncreaseOpponentScore()
            {
                youScore++;
                form.youScore.Text = youScore.ToString();
            }

            void IncreaseYouScore()
            {
                youScore++;
                form.youScore.Text = youScore.ToString();
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
            public Entity(string color, int leftOffset, int topOffset)
            {
                this.color = color;
                this.isShown = false;
                this.left += leftOffset;
                this.top += topOffset;
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
                this.element.Padding = new System.Windows.Forms.Padding(20, 38, 20, 38);
                this.element.Size = new System.Drawing.Size(80, 80);
                this.element.TabIndex = 4;
                this.element.Text = "Otoc me!";
                this.element.Left = this.left;
                this.element.Top = this.top;
                this.element.Click += new System.EventHandler(this.ProcessClick);
                form.Controls.Add(this.element);
            }

            public void SetGray()
            {
                this.element.BackColor = System.Drawing.Color.DarkGray;
            }

            void ProcessClick(object sender, System.EventArgs e)
            {
                if (this.isEnabled)
                {
                    this.isShown = !this.isShown;
                    if (this.isShown)
                    {
                        this.element.BackColor = ColorTranslator.FromHtml(this.color);
                    }
                }
                
            }

            public string GetColor()
            {
                return color;
            }

            public void Hide()
            {

            }
        }

        public List<string> GenerateColors(int numberOfPairs)
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
            for (int i = 1; i < n; i++ )
            {
                if (i*i > n)
                {
                    return biggest;
                }
                else if (i*i > biggest*biggest)
                {
                    biggest = i;
                }
            }
            return biggest;
        }

        void GenerateButtons()
        {
            int numberOfPairs = (int)numericUpDown.Value;
            int left = 0;
            int top = 0;
            int pow = getClosest2Power(numberOfPairs * 2);
            List<string> colors = GenerateColors(numberOfPairs);
            Random rnd = new Random();
            for (int i = 0; i < numberOfPairs*2; i++)
            {
                if (left == pow)
                {
                    top++;
                    left = 0;
                } 
                
                int idx = rnd.Next((numberOfPairs * 2) - i);
                string c = colors.ElementAt(idx);
                Entity ent= new Entity(c, left*130, top*130);
                colors.RemoveAt(idx);
                ent.CreateFormElement(this);
                left++;
            }
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

        // spatny muze delat velgi problem!
        void ShowStartScreenElements()
        {
            startButton.Visible = true;
            arrowLabel.Visible = true;
            numericUpDown.Visible = true;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            
            HideStartScreenElements();
            GenerateButtons(); 
        }
    }
}
