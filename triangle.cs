using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    abstract class Shape
    {
        public abstract string GetTypeString(); 
    }

    class Shape2D : Shape
    {
        double width, height; 
        int frac = 2; 

        public Shape2D(double width = 0, double height = 0, int frac = 2)
        {
            Width = width;
            Height = height;
            this.frac = frac;
        }

        public double Width
        {
            get
            {
                return width;
            }
            set
            {
                width = value < 0 ? -value : value;
            }
        }

        public double Height
        {
            get
            {
                return height;
            }
            set
            {
                height = value < 0 ? -value : value;
            }
        }

        public int Frac
        {
            get
            {
                return frac;
            }
            set
            {
                width = frac < 1 ? 0 : (frac > 14 ? 14 : value);
            }
        }

        public double RoundTo(double d)
        {
            return Math.Round(d, Frac);
        }


        public override string GetTypeString()
        {
            return "рамка фигуры";
        }

        public virtual double Area()
        {
            return RoundTo(Width * Height);
        }
    }

    class Triangle : Shape2D
    {
        protected string Style; 
        double A, B, C;         

        public Triangle(double A,double B,double C)
        {
            if (A + B > C & A + C > B & B + C > A)
            {
                double[] Temp = new double[] { A, B, C };
                Array.Sort(Temp);
                this.C = Width = Temp[2];
                this.B = Temp[1];
                this.A = Temp[0];
                Height = GetHeight(Temp[1]);
                var Temp2 = new List<double>() { Temp[0] * Temp[0], Temp[1] * Temp[1], Temp[2] * Temp[2] };
                this.Style = Temp2[2] < Temp2[0] + Temp2[1] ?
                (Temp2[0] == Temp2[1] ? "равносторонний" : "остроугольный") :
                (Temp2[2] > Temp2[0] + Temp2[1] ? "тупоугольный" : "прямоугольный");
            }

            else
            {
                this.A = this.B = this.C = Width = Height = 0;
                this.Style = "Треугольник не существует";
            }
        }

        private double GetHeight(double a)
        {
            double P = (A + B + C) / 2;
            return RoundTo(2 * Math.Sqrt(P * (P - A) * (P - B) * (P - C)) / A);
        }

        public override double Area()
        {
            double P = (A + B + C) / 2;
            return RoundTo(Math.Sqrt(P * (P - A) * (P - B) * (P - C)));
        }

        public override string GetTypeString()
        {
            return Style;
        }
    }

    public static class Measures
    {
        public static double ToRadians(this double angleInDegree)
        {
            return (angleInDegree * Math.PI) / 180.0;
        }
    }

    sealed class Triangle2С : Triangle
    {
        public Triangle2С(double A, double B, double angleC) :
        base(A, B,
        Math.Sqrt(Math.Pow(A, 2) + Math.Pow(B, 2) - 2 * A * B * Math.Cos(Measures.ToRadians(angleC))))
        {

        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите стороны треуголника");
            
            double A = double.Parse(Console.ReadLine());
            double B = double.Parse(Console.ReadLine());
            double C = double.Parse(Console.ReadLine());
            
            Triangle t3 = new Triangle(A, B, C);
            Console.WriteLine("Тип треуголника: {0}", t3.GetTypeString());
            Console.WriteLine("Площадь равна: " + t3.Area() + System.Environment.NewLine);
 
            Console.WriteLine("Введите 2 треуголника, а затем угол между ними");

            double D = double.Parse(Console.ReadLine());
            double E = double.Parse(Console.ReadLine());
            double angle = double.Parse(Console.ReadLine());

            Triangle2С t2ang = new Triangle2С(D, E, angle); 
            Console.WriteLine("Тип треугольника: {0}", t2ang.GetTypeString());
            Console.WriteLine("Площадь равна: " + t2ang.Area());
            Console.WriteLine();


            Console.ReadKey();
        }
    }

}