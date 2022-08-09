using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectCompiler
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string[] Identifiers = new string[] { "int", "char", "float", "string", "double", "bool" };
        List<int> Lint = new List<int>();
        List<float> Lfloat = new List<float>();
        string[] variable = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
        List<string> varibles = new List<string>();
        string[] looping = new string[] { "while", "if", "break", "continue", "end" };
        string[] num = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        string[] Symbol = new string[] { "( )", "{ };", ",", ";", "&&", "||", "<", ">", "=", "!", "==" };
        string[] Operators = new string[] { "*", "+", "-", "/", "%" };
        List<int> values = new List<int>();  
        List<string> Lvariables = new List<string>();
        int error = 0,check = 0,br = 0,val1 = 0,val2 = 0,resestance = 0 ,sum;
        string var1, var2, sumvalue;


        private void richTextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Lvariables.Clear();
            Lint.Clear();
            Lfloat.Clear();
            richTextBox2.Clear();
            richTextBox3.Clear();
            richTextBox4.Clear();
            string[] line = richTextBox1.Text.Split();
            for (int i = 0; i < richTextBox1.Lines.Length; i++)
            {

                for (int k = 0; k < Identifiers.Length; k++)
                {
                    if (richTextBox1.Lines[i].Contains(Identifiers[k]))
                    {
                        
                        string[] linechecker = richTextBox1.Lines[i].Split();

                        if (!richTextBox1.Lines[i].EndsWith(";"))
                        {
                            richTextBox2.Text += "Syntax error in line: " + i + "\n";
                            error++;
                        }

                        if (richTextBox1.Lines[i].StartsWith(Identifiers[k]) && linechecker.Length == 1)
                        {
                            richTextBox2.Text += "You can't create a stand alone indentifier. line: " + i + "\n";
                            error++;
                        }


                        for (int z = 0; z < linechecker.Length; z++)
                        {
                            for (int q = 0; q < Identifiers.Length; q++)
                            {
                                if (linechecker[z] == Identifiers[q] && Identifiers[q] != Identifiers[k])
                                {
                                    richTextBox2.Text += "You can't have more than 1 identifier in the same line. line: " + i + "\n";
                                    error++;
                                }
                            }
                        }
                    }
                }

                for (int k = 0; k < Symbol.Length; k++)
                {
                    string[] linechecker = line[i].Split();
                    if (linechecker.Length > 1)
                    {
                        if (richTextBox1.Lines[i].StartsWith(Symbol[k]) && (Symbol[k] != "{" && Symbol[k] != "}"))
                        {
                            richTextBox2.Text += "Syntax Error in line: " + i + "\n";
                            error++;
                        }
                    }

                }

                for (int k = 0; k < Operators.Length; k++)
                {
                    string[] linechecker = line[i].Split();


                    if (richTextBox1.Lines[i].StartsWith(Operators[k]))
                    {
                        richTextBox2.Text += "Syntax Error in line: " + i + "\n";
                        error++;
                    }

                }
                for (int k = 0; k < looping.Length; k++)
                {
                    string[] linechecker = richTextBox1.Lines[i].Split();
                    if (linechecker.Length > 1)
                    {
                        if (richTextBox1.Lines[i].StartsWith(looping[k]) && (linechecker[1] != "(" || richTextBox1.Lines[i].EndsWith(")")))
                        {
                            richTextBox2.Text += "Syntax error in line: " + i + "\n";
                            error++;
                        }
                    }  
                }


                if (i > 0)
                {
                    for (int k = 0; k < looping.Length; k++)
                    {
                        if (richTextBox1.Lines[i - 1].Contains(looping[k]) && (!richTextBox1.Lines[i].StartsWith("{")))
                        {
                            richTextBox2.Text += "Expected symbol { in line: " + i + "\n";
                            error++;
                        }
                    }
                }


                string[] cbrackets = richTextBox1.Lines[i].Split();
                int cb = 0;
                int ob = 0;
                for (int k = 0; k < cbrackets.Length; k++)
                {
                    if (cbrackets[k] == "(")
                    {
                        ob++;
                    }
                    if (cbrackets[k] == ")")
                    {
                        cb++;
                    }
                }

                if (ob != cb)
                {
                    if (ob > cb)
                    {
                        richTextBox2.Text += ("You are missing a ) in line: " + i + "\n");
                    }
                    if (ob < cb)
                    {
                        richTextBox2.Text += ("You are missing a ( in line: " + i + "\n");
                    }
                    error++;
                }

            }
            int countopened = 0;
            int countclosed = 0;
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == "{")
                {
                    countopened++;
                }
                if (line[i] == "};")
                {
                    countclosed++;
                }

            }
            if (countopened != countclosed)
            {
                if (countopened > countclosed)
                {
                    richTextBox2.Text += "You are missing a }; in your code" + "\n";
                    error++;
                }
                if (countopened < countclosed)
                {
                    richTextBox2.Text += "You are missing a { in your code" + "\n";
                    error++;
                }

            }
            if (error == 0)
            {
                for (int i = 0; i < richTextBox1.Lines.Length; i++)
                {
                    string[] linecompile = richTextBox1.Lines[i].Split();

                    for (int k = 0; k < Identifiers.Length; k++)
                    {
                        if (Identifiers[k] == "int")
                        {
                            for (int s = 0; s < linecompile.Length; s++)
                            {
                                for (int a = 0; a < variable.Length; a++)
                                {
                                    if (linecompile[s] == variable[a])
                                    {
                                        Lvariables.Add(variable[a]);

                                    }
                                }
                                for (int n = 0; n < num.Length; n++)
                                {
                                    if (linecompile[s] == num[n])
                                    {
                                        Lint.Add(int.Parse(num[n]));
                                    }
                                }
                            }
                        }
                        if (Identifiers[k] == "float")
                        {
                            for (int s = 0; s < linecompile.Length; s++)
                            {
                                for (int a = 0; a < variable.Length; a++)
                                {
                                    if (linecompile[s] == variable[a])
                                    {
                                        Lvariables.Add(variable[a]);
                                    }
                                }
                                for (int n = 0; n < num.Length; n++)
                                {
                                    if (linecompile[s] == num[n])
                                    {
                                        Lfloat.Add(int.Parse(num[n]));
                                    }
                                }
                            }
                        }
                    }
                }
            }
            int flag = 0;
            int outt = 0;
            for (int i = 0; i < richTextBox1.Lines.Length; i++)
            {
                string[] linechecker = richTextBox1.Lines[i].Split();
                for (int a = 0; a < Lvariables.Count; a++)
                {
                    for (int m = 0; m < looping.Length; m++)
                    {
                        if (linechecker[0] == looping[m])
                        {
                            outt = 1;
                            break;
                        }
                    }

                    if (outt == 1)
                    {
                        break;
                    }
                    for (int k = 0; k < linechecker.Length; k++)
                    {
                        if (linechecker[k] == Lvariables[a] && k != 0)
                        {
                            flag = 1;
                            break;
                        }
                    }
                    if (flag == 1)
                    {
                        break;
                    }
                }
                if (flag == 0 && Lvariables.Count != 0)
                {
                    richTextBox2.Text += "Undefined variable in line: " + i + "\n";
                    error++;
                }
            }



            button3.Text = error + " Errors";
            error = 0;
            compilesample();
            richTextBox4.Clear();
            for (int i = 0; i < varibles.Count; i++)
            {
                richTextBox4.Text += varibles[i] + " = " + values[i] + "\n";
            }
        
        }
        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox3.Clear();
            string[] Lines = richTextBox1.Text.Split();
            for (int i = 0; i < Lines.Length; i++)
            {
                for (int k = 0; k < Identifiers.Length; k++)
                {
                    if (Lines[i] == Identifiers[k])
                    {
                        richTextBox3.Text += "Identifier: " + Identifiers[k] + "\n";
                    }
                }

                for (int k = 0; k < variable.Length; k++)
                {
                    if (Lines[i] == variable[k])
                    {
                        richTextBox4.Text += variable[k] + "\n";

                    }
                }
                for (int k = 0; k < looping.Length; k++)
                {
                    if (Lines[i] == looping[k])
                    {
                        richTextBox3.Text += "resestanceerved word: " + looping[k] + "\n";
                        richTextBox4.Text += looping[k] + "\n";
                    }
                }

                for (int k = 0; k < num.Length; k++)
                {
                    if (Lines[i] == num[k])
                    {
                        richTextBox3.Text += "num: " + num[k] + "\n";
                    }
                }
                for (int k = 0; k < Symbol.Length; k++)
                {
                    if (Lines[i] == Symbol[k])
                    {
                        richTextBox3.Text += "Symbol: " + Symbol[k] + "\n";
                    }
                }
                for (int k = 0; k < Operators.Length; k++)
                {
                    if (Lines[i] == Operators[k])
                    {
                        richTextBox3.Text += "Operator: " + Operators[k] + "\n";
                    }
                }
            }
        }
        void compilesample()
        {
            varibles.Clear();
            values.Clear();
            for (int i = 0; i < richTextBox1.Lines.Length; i++)
            {
                string[] linechecker = richTextBox1.Lines[i].Split();
                for (int k = 0; k < looping.Length; k++)
                {
                    val1 = 0;
                    val2 = 0;
                    if (linechecker.Length > 1)
                    {
                        bool brust = false;

                        for (int m = 0; m < linechecker.Length; m++)
                        {
                            resestance = 7;
                            if (linechecker[m] == "+")
                            {
                                sumvalue = linechecker[m - 3];

                                int.TryParse(linechecker[m - 1], out resestance);
                                if (int.TryParse(linechecker[m - 1], out resestance) == true)
                                {
                                    val1 = int.Parse(linechecker[m - 1]);
                                }
                                else
                                {
                                    var1 = linechecker[m - 1];

                                    for (int j = 0; j < values.Count; j++)
                                    {
                                        if (varibles[j] == var1)
                                        {
                                            val1 = values[j];
                                            check = 1;
                                        }
                                    }
                                    if (check == 0)
                                    {
                                        richTextBox2.Text += var1 + "  has null value" + "\n";
                                    }

                                }

                                if (int.TryParse(linechecker[m + 1], out resestance) == true)
                                {
                                    val2 = int.Parse(linechecker[m + 1]);
                                }
                                else
                                {
                                    var2 = linechecker[m + 1];

                                    for (int j = 0; j < values.Count; j++)
                                    {
                                        if (varibles[j] == var2)
                                        {
                                            val2 = values[j];
                                        }
                                    }

                                }
                                sum = val1 + val2;
                                varibles.Add(sumvalue);
                                values.Add(sum);
                                br = 1;
                                break;

                            }
                            else if (linechecker[m] == "*")
                            {
                                sumvalue = linechecker[m - 3];

                                if (int.TryParse(linechecker[m - 1], out resestance) == true)
                                {
                                    val1 = int.Parse(linechecker[m - 1]);
                                }
                                else
                                {
                                    var1 = linechecker[m - 1];

                                    for (int j = 0; j < values.Count; j++)
                                    {
                                        if (varibles[j] == var1)
                                        {
                                            val1 = values[j];
                                        }
                                    }

                                }

                                if (int.TryParse(linechecker[m + 1], out resestance) == true)
                                {
                                    val2 = int.Parse(linechecker[m + 1]);
                                }
                                else
                                {
                                    var2 = linechecker[m + 1];

                                    for (int j = 0; j < values.Count; j++)
                                    {
                                        if (varibles[j] == var2)
                                        {
                                            val2 = values[j];
                                        }
                                    }

                                }

                                int sum = val1 * val2;
                                varibles.Add(sumvalue);
                                values.Add(sum);
                                br = 1;
                                break;

                            }
                            else if (linechecker[m] == "-")
                            {
                                sumvalue = linechecker[m - 3];

                                if (int.TryParse(linechecker[m - 1], out resestance) == true)
                                {
                                    val1 = int.Parse(linechecker[m - 1]);
                                }
                                else
                                {
                                    var1 = linechecker[m - 1];

                                    for (int j = 0; j < values.Count;j++)
                                    {
                                        if (varibles[j] == var1)
                                        {
                                            val1 = values[j];
                                        }
                                    }
                                }
                                if (int.TryParse(linechecker[m + 1], out resestance) == true)
                                {
                                    val2 = int.Parse(linechecker[m + 1]);
                                }
                                else
                                {
                                    var2 = linechecker[m + 1];

                                    for (int j = 0; j < values.Count; j++)
                                    {
                                        if (varibles[j] == var2)
                                        {
                                            val2 = values[j];
                                        }
                                    }

                                }
                                sum = val1 - val2;
                                varibles.Add(sumvalue);
                                values.Add(sum);
                                br = 1;
                                break;
                            }
                            else if (linechecker[m] == "/")
                            {
                                sumvalue = linechecker[m - 3];
                                if (int.TryParse(linechecker[m - 1], out resestance) == true)
                                {
                                    val1 = int.Parse(linechecker[m - 1]);
                                }
                                else
                                {
                                    var1 = linechecker[m - 1];
                                    for (int j = 0; j < values.Count; j++)
                                    {
                                        if (varibles[j] == var1)
                                        {
                                            val1 = values[j];
                                        }
                                    }
                                }
                                if (int.TryParse(linechecker[m + 1], out resestance) == true)
                                {
                                    val2 = int.Parse(linechecker[m + 1]);
                                }
                                else
                                {
                                    var2 = linechecker[m + 1];
                                    for (int j = 0; j < values.Count; j++)
                                    {
                                        if (varibles[j] == var2)
                                        {
                                            val2 = values[j];
                                        }
                                    }
                                }
                                 sum = val1 / val2;
                                varibles.Add(sumvalue);
                                values.Add(sum);
                                br = 1;
                                break;
                            }
                            else if (linechecker[m] == "=" && linechecker.Length == 5)
                            {
                                varibles.Add(linechecker[m - 1]);

                                if (bool.TryParse(linechecker[m + 1], out brust) == false)
                                {
                                    values.Add(int.Parse(linechecker[m + 1]));
                                    br = 1;
                                    break;
                                }
                            }
                        }
                        if (br == 1)
                        {
                            br = 0;
                            break;
                        }

                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
