using System;
using System.Text;
using System.Collections;

namespace method_Hooke_Jeeves
{
    class Program
    {

        public static double Roz(double x1, double x2)          //Функция Розенброка
        {
            return (1 - x1) * (1 - x1) + 100 * (x2 - x1 * x1) * (x2 - x1 * x1);
        }
        

        static void Main(string[] args)
        {

            double x1;      //начальное значение
            double x2;     //начальное значение
            double e = 0.0001;        //допустимая ошибка поиска
            double h = 1;      //шаг
            double alfa = 0.1;      //коэффициент дробления шага
            Console.WriteLine("Введите начальные значения x1 и x2: ");
            x1 = Convert.ToDouble(Console.ReadLine());
            x2 = Convert.ToDouble(Console.ReadLine());

            //вычисление значения функции в базисной точке
            double xb1 = x1;        // xb1 и xb2 означают координаты базисной точки
            double xb2 = x2;
            double rb = Roz(xb1, xb2);


            //создание основного цикла поиска
            while(h > e)        //проверяет условие останова итерационного поиска
            {
                Console.WriteLine("Шаг: " + h);

                Console.WriteLine("Исследующий поиск ");
                //исследующий поиск с использованием временных переменных
                x1 = xb1;
                x2 = xb2;
                double r = rb;
                //по координатам
                double newx1;   //пробные координаты новой точки
                double newx2;

                newx1 = x1 + h;
                newx2 = x2 + h;
                double r1 = Roz(newx1, newx2);
                if (r1 < r)
                {
                    x1 = newx1;
                    x2 = newx2;
                    r = r1;
                }
                else
                {
                    newx1 = x1 - h;
                    newx2 = x2 - h;
                    r1 = Roz(newx1, newx2);
                    if (r1 < r)
                    {
                        x1 = newx1;
                        x2 = newx2;
                        r = r1;
                    }
                }
                Console.WriteLine("Исследующий поиск окончен ");

                //если исследующий поиск является удачным, то выполняется поиск по образцу внутри условия if, проверяющего успешность исследующего поиска
                //сравнение значений текущей и базисной точки
                if (r < rb)
                {
                    while (true)
                    {
                        //берём новую базисную точку и значение в ней, сохранив предыдущие значения
                        double xpb1 = xb1;
                        double xpb2 = xb2;
                        xb1 = x1;
                        xb2 = x2;
                        rb = r;

                        Console.WriteLine("Новая базовая точка: {0} {1} ", xb1, xb2);

                        //делаем шаг поиска по образцу
                        double xp1;
                        double xp2;
                        xp1 = xpb1 + 2 * (xb1 - xpb1);
                        xp2 = xpb2 + 2 * (xb2 - xpb2);

                        //исследующий поиск относительно новой базисной точки
                        x1 = xp1;
                        x2 = xp2;
                        r = Roz(x1, x2);

                        newx1 = x1 + h;
                        newx2 = x2 + h;
                        r1 = Roz(newx1, newx2);
                        if (r1 < r)
                        {
                            x1 = newx1;
                            x2 = newx2;
                            r = r1;
                        }
                        else
                        {
                            newx1 = x1 - h;
                            newx2 = x2 - h;
                            r1 = Roz(newx1, newx2);
                            if (r1 < r)
                            {
                                x1 = newx1;
                                x2 = newx2;
                                r = r1;
                            }
                        }

                        if (r >= rb)
                            break;
                    }
                }       // в противном случае выполняется дробление шага поиска 
                else h *= alfa;
                //затем заново выполняется исследующий поиск и поиск по образцу
            }


            //выводим результаты
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("Результаты: ");
            Console.WriteLine("Координаты точки X = {0}  {1} ", xb1, xb2);
            Console.WriteLine("Значение функции F = {0} ", rb);

            Console.ReadKey();
        }
    }
}
