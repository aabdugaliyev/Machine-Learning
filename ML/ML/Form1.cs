using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ML
{
    public partial class form : Form
    {
        List<Tuple<string, Image>> teams = new List<Tuple<string, Image>>();
        List<Label> labelList = new List<Label>();
        int team1Index = 0;
        int team2Index = 0;

        public form()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            fillTeams();

            team1Index = 0;
            team2Index = 5;

            changeTeam1(team1Index);
            changeTeam2(team2Index);

            walp.Image = Properties.Resources.walp;
            Width = walp.Image.Width;
            Height = walp.Image.Height;
            walp.Location = new Point(0, 0);
            walp.SizeMode = PictureBoxSizeMode.AutoSize;
            team1.BackColor = Color.Transparent;
            team2.BackColor = Color.Transparent;
            team1.Parent = walp;
            team2.Parent = walp;
            t1Label.BackColor = Color.Transparent;
            t1Label.Parent = walp;
            t2Label.BackColor = Color.Transparent;
            t2Label.Parent = walp;

            t1Left.BackColor = Color.Transparent;
            t1Left.Parent = walp;
            t1Left.BackgroundImageLayout = ImageLayout.Stretch;

            t1Right.BackColor = Color.Transparent;
            t1Right.Parent = walp;
            t1Right.BackgroundImageLayout = ImageLayout.Stretch;

            t2Left.BackColor = Color.Transparent;
            t2Left.Parent = walp;
            t2Left.BackgroundImageLayout = ImageLayout.Stretch;

            t2Right.BackColor = Color.Transparent;
            t2Right.Parent = walp;
            t2Right.BackgroundImageLayout = ImageLayout.Stretch;

            team1.BackgroundImageLayout = ImageLayout.Stretch;
            team1.SizeMode = PictureBoxSizeMode.StretchImage;
            team2.BackgroundImageLayout = ImageLayout.Stretch;
            team2.SizeMode = PictureBoxSizeMode.StretchImage;

            t1Label.ForeColor = Color.WhiteSmoke;
            t1Label.Font = new Font("Arial", 14 , FontStyle.Bold);

            t2Label.ForeColor = Color.WhiteSmoke;
            t2Label.Font = new Font("Arial", 14, FontStyle.Bold);

            t1Win.ForeColor = Color.DarkBlue;
            t1Win.Font = new Font("Arial", 14, FontStyle.Bold);

            t2Win.ForeColor = Color.DarkRed;
            t2Win.Font = new Font("Arial", 14, FontStyle.Bold);

            draw.ForeColor = Color.White;
            draw.Font = new Font("Arial", 14, FontStyle.Bold);

            t1Win.BackColor = Color.Transparent;
            t1Win.Parent = walp;

            t2Win.BackColor = Color.Transparent;
            t2Win.Parent = walp;

            draw.BackColor = Color.Transparent;
            draw.Parent = walp;

            button1.BackColor = Color.White;
            button1.Parent = walp;
            button1.Text = "Calculate";
            

            //walp.Hide();
            int step = 100;
            for (int i = 0; i < 100; i++)
            {
                Label l = new Label();
                l.Location = new Point(step, 300);
                l.BackColor = Color.Transparent;
                l.Width = 5;
                l.Height = 20;
                Controls.Add(l);
                labelList.Add(l);
                l.Parent = walp;
                step += 5;


            }
        }

        public void fillTeams()
        {
            teams.Add(Tuple.Create("Roma", Image.FromFile("roma.png")));
            teams.Add(Tuple.Create("Atletico", Image.FromFile("atletico.png")));
            teams.Add(Tuple.Create("Barcelona", Image.FromFile("barcelona.png")));
            teams.Add(Tuple.Create("Bayern", Image.FromFile("bayern.png")));
            teams.Add(Tuple.Create("Chelsea", Image.FromFile("chelsea.png")));
            teams.Add(Tuple.Create("Juventus", Image.FromFile("juventus.png")));
            teams.Add(Tuple.Create("Liverpool", Image.FromFile("liverpool.png")));
            teams.Add(Tuple.Create("MC", Image.FromFile("mc.png")));
            teams.Add(Tuple.Create("PSG", Image.FromFile("psg.png")));
            teams.Add(Tuple.Create("RealMadrid", Image.FromFile("realmadrid.png")));
            teams.Add(Tuple.Create("Sevilla", Image.FromFile("sevilla.png")));
            teams.Add(Tuple.Create("Tottenham", Image.FromFile("tottenham.png")));
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public void changeTeam1(int index)
        {
            team1.Image = teams[index].Item2;
            t1Label.Text = teams[index].Item1;
        }

        public void changeTeam2(int index)
        {
            team2.Image = teams[index].Item2;
            t2Label.Text = teams[index].Item1;
        }

        private void t1Left_Click(object sender, EventArgs e)
        {
            team1Index--;
            if (team1Index < 0)
            {
                team1Index = teams.Count - 1;
            }
            
            changeTeam1(team1Index);
        }

        private void t1Right_Click(object sender, EventArgs e)
        {
            team1Index++;
            if (team1Index == teams.Count)
            {
                team1Index = 0;
            }
            changeTeam1(team1Index);
        }

        private void t2Left_Click(object sender, EventArgs e)
        {
            team2Index--;
            if (team2Index < 0)
            {
                team2Index = teams.Count - 1;
            }

            changeTeam2(team2Index);
        }

        private void t2Right_Click(object sender, EventArgs e)
        {
            team2Index++;
            if (team2Index == teams.Count)
            {
                team2Index = 0;
            }
            changeTeam2(team2Index);
        }




        public static string ReadData(string str)
        {
            string[] lines = File.ReadAllLines(str);
            string data = "";

            foreach (string l in lines)
            {
                data += l;
            }

            return data;
        }

        public static List<string> ConvertToList(string[] arr)
        {
            List<string> listString = new List<string>();

            for (int i = 0; i < arr.Length; i++)
            {
                listString.Add(arr[i]);
            }

            return listString;
        }

        //Берем инфу через название команды, т.е счеты, показатели игроков
        public static List<string> FindTeamScores(List<string> list, string teamName)
        {
            List<string> TeamNameList = new List<string>();
            string teamStringSepparated = "";

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Substring(0, teamName.Length) == teamName)
                {
                    string currentString = list[i];
                    teamStringSepparated = currentString.Substring(teamName.Length + 1, currentString.Length - teamName.Length - 2);

                    int betweenPosition = teamStringSepparated.IndexOf('[');
                    TeamNameList.Add(teamStringSepparated.Substring(1, betweenPosition - 2));
                    TeamNameList.Add(teamStringSepparated.Substring(betweenPosition + 1, teamStringSepparated.Length - betweenPosition - 2));
                }

            }

            return TeamNameList;
        }

        //Сплитим голы, находим среднее количество голов забитых и пропущенных
        static Tuple<double, double> GoalCounting(string str)
        {
            double GoalDid = 0;
            double GoalGet = 0;
            double totalGames = 0;

            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == ':')
                {
                    double a = Int32.Parse(str[i - 1].ToString());
                    double b = Int32.Parse(str[i + 1].ToString());

                    GoalDid += a;
                    GoalGet += b;
                    totalGames++;

                }
            }

            double AverageGoalDid = GoalDid / totalGames;
            double AverageGoalGet = GoalGet / totalGames;
            return Tuple.Create(AverageGoalDid, AverageGoalGet);
        }

        static int[] calculateWins(string str)
        {
            int[] calcWin = new int[3];
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == ':')
                {
                    double a = Int32.Parse(str[i - 1].ToString());
                    double b = Int32.Parse(str[i + 1].ToString());

                    if (a > b)
                    {
                        calcWin[0]++;
                    }
                    else if (a < b)
                    {
                        calcWin[1]++;
                    }
                    else
                    {
                        calcWin[2]++;
                    }

                }
            }
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine(calcWin[i].ToString());
            }
            return calcWin;

        }

        //Записываем показатели игроков З,П,Н,В
        static List<string> teamSorting(string str)
        {
            int x = 0, y = 0;
            List<string> cor = new List<string>();
            string[] teamComposition = str.Split(' ');

            for (int i = 0; i < teamComposition.Length; i++)
            {

                for (int j = 0; j < teamComposition[i].Length; j++)
                {
                    if (teamComposition[i][j] == '(')
                    {
                        x = j + 1;

                    }
                    else if (teamComposition[i][j] == ')')
                    {
                        y = j;
                        cor.Add(teamComposition[i].Substring(x, y - x));
                    }
                }
            }


            return cor;
        }


        //Среднее значение каждой категории З,П,Н,В
        static List<double> splitComposition(List<string> strArray)
        {
            List<string[]> listOfSplittedCompositions = new List<string[]>();
            for (int i = 0; i < strArray.Count; i++)
            {
                string[] splittedTeam = strArray[i].Split(',');
                listOfSplittedCompositions.Add(splittedTeam);
            }

            List<List<double>> averageList = new List<List<double>>();


            for (int i = 0; i < listOfSplittedCompositions.Count; i++)
            {
                List<double> scores = new List<double>();
                for (int j = 0; j < listOfSplittedCompositions[i].Length; j++)
                {
                    StringBuilder sb = new StringBuilder(listOfSplittedCompositions[i][j]);
                    sb.Replace('.', ',');
                    string s = sb.ToString();
                    double d = Double.Parse(s);
                    scores.Add(d);
                }
                averageList.Add(scores);
            }


            List<double> aver = new List<double>();
            for (int i = 0; i < averageList.Count; i++)
            {
                double d = 0;
                for (int j = 0; j < averageList[i].Count; j++)
                {
                    d += averageList[i][j];
                }
                aver.Add(d / averageList[i].Count);
            }

            /*  for (int i = 0; i < aver.Count; i++)
              {
                  Console.WriteLine(aver[i].ToString());
              }*/


            /*  foreach (double d in aver)
              {
                  Console.WriteLine(d.ToString());
              }*/
            return aver;

        }

        static void Menu()
        {
            Console.WriteLine("This is football prediction app, enter 2 teams...");
        }

        static double TotalAverage(List<double> list)
        {
            double totalAverage = 0;

            for (int i = 0; i < list.Count; i++)
            {
                totalAverage += list[i];

            }

            return totalAverage / list.Count;
        }

        static double[] computeTotal(int[] t1, int[] t2)
        {
            double[] total = new double[3];
            total[0] = t1[0] + t2[1];
            total[1] = t1[2] + t2[2];
            total[2] = t1[1] + t2[0];

            total[0] = (total[0] / 14) * 100;
            total[1] = (total[1] / 14) * 100;
            total[2] = (total[2] / 14) * 100;

            return total;
        }

        static double[] calc(double[] d, double per1, double per2)
        {
            double[] win = new double[2];
            win[0] = d[0] * per1 * 2;
            win[1] = d[1] * per2 * 2;
            return win;
        }

        void Calc()
        {
            string force = ReadData("dataset.txt");
            string[] datasetArray = force.Split('~');
            List<string> dataset = ConvertToList(datasetArray);

            Menu();
            Console.Write("Team 1 is: ");
            string t1 = t1Label.Text;

            Console.Write("Team 2 is: ");
            string t2 = t2Label.Text + " A";
            List<string> Team1 = FindTeamScores(dataset, t1);
            List<string> Team2 = FindTeamScores(dataset, t2);

            Tuple<double, double> team1GoalStat = GoalCounting(Team1[0]);
            Tuple<double, double> team2GoalStat = GoalCounting(Team2[0]);

            int[] team1Wins = calculateWins(Team1[0]);
            int[] team2Wins = calculateWins(Team2[0]);

            List<string> team1TeamStat = teamSorting(Team1[1]);
            List<string> team2TeamStat = teamSorting(Team2[1]);

            List<double> team1AverageStat = splitComposition(team1TeamStat);
            List<double> team2AverageStat = splitComposition(team2TeamStat);

            double t1Average = TotalAverage(team1AverageStat);
            double t2Average = TotalAverage(team2AverageStat);

            double all = t1Average + t2Average;
            double per1 = t1Average * 100 / all;
            double per2 = t2Average * 100 / all;


            Console.WriteLine(per1.ToString() + "\n" + per2.ToString() + "\n");
            double[] prediction = computeTotal(team1Wins, team2Wins);
            double[] wins = calc(prediction, per1, per2);

            per1 = ((per1 * 2) / 100) * prediction[0];
            per2 = ((per2 * 2) / 100) * prediction[2];
            double dr = 100 - per1 - per2;

            //double draw = 100 - wins[0] + wins[1];

            t1Win.Text = per1.ToString();
            t2Win.Text = per2.ToString();
            draw.Text = dr.ToString();

            for (int i = 0; i < 100; i++)
            {
                if (i < (int)per1)
                    labelList[i].BackColor = Color.DarkBlue;
                else if ((i - per1) < (int)dr)
                    labelList[i].BackColor = Color.White;
                else if ((i - per1 - dr) < (int)per2)
                    labelList[i].BackColor = Color.DarkRed;
            }


            Console.WriteLine("\nTeam " + t1 + " on average scores " + team1GoalStat.Item1 + " on average misses " +
                              team1GoalStat.Item2 + ".\n " + "Their average Goalkeeper, Defenders, Semidefenders and Attackers are " +
                              team1AverageStat[0] + " " + team1AverageStat[1] + " " + team1AverageStat[2] + " " + team1AverageStat[3] + ".\n"
                );

            Console.WriteLine("Team " + t2 + " on average scores " + team2GoalStat.Item1 + " on average misses " +
                              team2GoalStat.Item2 + ".\n " + "Their average Goalkeeper, Defenders, Semidefenders and Attackers are " +
                              team2AverageStat[0] + " " + team2AverageStat[1] + " " + team2AverageStat[2] + " " + team2AverageStat[3] + ".\n"
                );

            Console.WriteLine("Probability of winning " + t1 + " is: " + wins[0] + "%.\n");
            Console.WriteLine("Probability of winning " + t2 + " is: " + wins[1] + "%.\n");
            double draww = 100 - wins[0] - wins[1];
            Console.WriteLine("Draw is " + draww);
            Console.ReadLine();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Calc();
        }
    }
}
