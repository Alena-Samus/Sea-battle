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

        //Генерация карт
        public void GenerateMap(char[,] Map)
        {//Маркер того, что корабль ставить можно
            bool Possible;

            //В начале делаем клетки пустыми
            for (int i = 0; i < 11; i++)
                for (int j = 0; j < 11; j++)
                    Map[i, j] = Chars[0];



            //Считаем количество поставленных кораблей
            int Ships = 0;

            //Определяем текущую длину корабля. Начинаем с самого длинного
            int Shiplength = 4;
            do
            {
                //Выбираем ориентацию корабля: 0 - horizontal, 1 - vertical
                int Orientation = rnd.Next(2);
                //Выбираем координаты кораблей, предотвращая их выход за пределы игрового поля
                //Выбираем верхнюю левую границу
                int x = rnd.Next(1, 11 - Orientation * (Shiplength - 1));
                int y = rnd.Next(1, 11 - (1 - Orientation) * (Shiplength - 1));

                //Проверяем возможность поставить корабль
                Possible = true; //Предполагаем, что корабль поставить можно
                for (int i = 0; i < Shiplength; i++) //Идем по клеткам, где предоложительно можно поставить корабль
                    if (Map[x + i * Orientation, y + i * (1 - Orientation)] != Chars[0])//Если там не спокойная вода
                        Possible = false;//корабль поставить нельзя

                //если есть безопасное место для корабля
                if (Possible == true)

                {//Заполняем "зоны близости" кораблей на карте
                    for (int i = 0; i < Shiplength; i++)
                    {//Матрица 3х3 из сбитых клеток
                        for (int xx = Math.Max(x + i * Orientation - 1, 1);
                            xx <= Math.Min(x + i * Orientation + 1, 10); xx++)
                            for (int yy = Math.Max(y + i * (1 - Orientation) - 1, 1);
                                yy <= Math.Min(y + i * (1 - Orientation) + 1, 10); yy++)
                                Map[xx, yy] = Chars[1];
                    }


                    //Рисуем корабли
                    for (int i = 0; i < Shiplength; i++)
                        Map[x + i * Orientation, y + i * (1 - Orientation)] = Chars[2];

                    //Увеличиваем количество кораблей

                    Ships++;

                    //Уменьшаем длину кораблей
                    //Должно быть: 1 - четырехпалубный; 2 - трехпалубных; 3 - двухпалубных; 4 - однопалубных
                    if (Ships == 1 || Ships == 3 || Ships == 6)
                        Shiplength--;

                }
            } while (Ships < 10);

            //После того как корабли расставили, убираем ограничители
            for (int i = 0; i < 11; i++)
                for (int j = 0; j < 11; j++)
                {
                    if (Map[i, j] == Chars[1])
                        Map[i, j] = Chars[0];
                }

        }

        //Метод для отображения карты на форме
        public void ShowMap(char[,] Map, TableLayoutPanel TP)
        {
            for (int i = 1; i < 11; i++)
                for (int j = 1; j < 11; j++)
                {
                    Label a = TP.Controls[i * 11 + j] as Label;
                    a.Text = Convert.ToString(Map[i, j]);
                }

        }

        //Метод для выстрелов по карте противника
        private void label1_Click(object sender, EventArgs e)
        {
            //выясняем по какой клетке кликнул игрок
            Label a = sender as Label;
            int i;
            for (i = 0; i < 121; i++)
            {
                Label b = EnemyTP.Controls[i] as Label;
                if (a == b)
                    break;

                //если на карте, по которой стреляли, оказалась пустота, рисуем волну
                if (EnemyMap[i % 11, i % 11] == Chars[0])
                {
                    EnemyMap[i % 11, i % 11] = Chars[1];
                    a.Text = Convert.ToString(Chars[1]);
                }

                //передаем ход противнику
                // EnemyMove();

                //если на карте противника по координатам несбитый корабль, по рисуем сбитую клетку
                if (EnemyMap[i % 11, i % 11] == Chars[2])
                {
                    EnemyMap[i % 11, i % 11] = Chars[3];
                    a.Text = Convert.ToString(Chars[3]);
                }


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
                for (int j = 0; j < 11; j++)
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
                    if (i > 0 && j > 0)
                        a.Click += label1_Click;//назначаем в качесве обработчика метод для выстрелов по карте противника
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
                a.Font = new System.Drawing.Font("Arial", 8);
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
                a.Font = new System.Drawing.Font("Arial", 8);
                a.Text = Convert.ToString(i);

            }
            //Генерируем карты
            GenerateMap(HomeMap);
            GenerateMap(EnemyMap);

            //Отображаем карты
            ShowMap(HomeMap, HomeTP);

            ShowMap(EnemyMap, EnemyTP);// карту противника не показываем

        }


    }
}

