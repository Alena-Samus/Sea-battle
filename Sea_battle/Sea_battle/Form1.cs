using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sea_battle
{
    public partial class Form1 : Form
    {//Создание рандомного объекта для заполнения ячеек
        Random rnd = new Random();
        string Chars = " hox";

        //Заголовок строк
        string Title = "ABCDEFGHIJ";

        //Массивы для хранения кораблей, отображаемых на карте
        char[,] HomeMap = new char[11, 11];
        char[,] EnemyMap = new char[11, 11];

        //Метод для отображения карты на форме
        public void ShowMap(char[,] Map, TableLayoutPanel TP)
        {
            for (int i = 0; i < 11; i++)
                for (int j = 0; j < 11; j++)
                {
                    Label a = TP.Controls[i * 11 + j] as Label;
                    a.Text = Convert.ToString(Map[i, j]);
                }
                    

        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Заполнение таблицы игрока метками
            for (int i = 0; i < 11; i++)
                for(int j = 0; j < 11; j++)
                {
                    Label a = new Label();
                    a.Dock = DockStyle.Fill;//
                    a.AutoSize = false;
                    a.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                    a.Font = new System.Drawing.Font("wingdings", 18);
                    a.Text = Convert.ToString(Chars[0]);
                    HomeTP.Controls.Add(a, j, i);
                }

            //Заполнение таблицы противника метками
            for (int i = 0; i < 11; i++)
                for (int j = 0; j < 11; j++)
                {
                    Label a = new Label();
                    a.Dock = DockStyle.Fill;//
                    a.AutoSize = false;
                    a.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                    a.Font = new System.Drawing.Font("wingdings", 18);
                    a.Text = Convert.ToString(Chars[0]);
                    EnemyTP.Controls.Add(a, j, i);
                }

            //Печать заголовков: игрок
            for (int j = 1; j < 11; j++)
            {
                Label a = HomeTP.Controls[j] as Label;//Выбираем каждую метку
                a.Font = new System.Drawing.Font("Arial", 12);
                a.Text = Convert.ToString(Title[j - 1]);

            }  
            
            for (int i = 1; i < 11; i++)
            {
                Label a = HomeTP.Controls[i * 11] as Label;//Номер строки умножить на ширину таблицы
                a.Font = new System.Drawing.Font("Arial", 10);
                a.Text = Convert.ToString(i);
               

            }

            //Печать заголовков: противник
            for (int j = 1; j < 11; j++)
            {
                Label a = EnemyTP.Controls[j] as Label;
                a.Font = new System.Drawing.Font("Arial", 12);
                a.Text = Convert.ToString(Title[j - 1]);

            }

            for (int i = 1; i < 11; i++)
            {
                Label a = EnemyTP.Controls[i * 11] as Label;
                a.Font = new System.Drawing.Font("Arial", 10);
                a.Text = Convert.ToString(i);

            }
            //Генерация карт

            //Отображаем карты
            ShowMap(HomeMap, HomeTP);
            ShowMap(EnemyMap, EnemyTP);

        }


    }
}
