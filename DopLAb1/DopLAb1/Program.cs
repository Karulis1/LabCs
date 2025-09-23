using System;

public class Matrix
{
    public int[,] data;
    public int Rows;
    public int Columns;

    public Matrix(int rows, int columns)
    {
        Rows = rows;
        Columns = columns;
        data = new int[rows, columns];
    }

    public void FillRandom(int min = 0, int max = 10)
    {
        Random rand = new Random();
        for (int i = 0; i < Rows; i++)
            for (int j = 0; j < Columns; j++)
                data[i, j] = rand.Next(min, max);
    }

    public void Fill()
    {
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                Console.Write($"Элемент [{i},{j}]: ");
                data[i, j] = int.Parse(Console.ReadLine());
            }
        }
    }

    public void Print()
    {
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
                Console.Write(data[i, j] + "\t");
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    public static Matrix Add(Matrix a, Matrix b)
    {
        Matrix result = new Matrix(a.Rows, a.Columns);
        for (int i = 0; i < a.Rows; i++)
            for (int j = 0; j < a.Columns; j++)
                result.data[i, j] = a.data[i, j] + b.data[i, j];
        return result;
    }

    public static Matrix Multiply(Matrix a, Matrix b)
    {
        Matrix result = new Matrix(a.Rows, b.Columns);
        for (int i = 0; i < a.Rows; i++)
            for (int j = 0; j < b.Columns; j++)
                for (int k = 0; k < a.Columns; k++)
                    result.data[i, j] += a.data[i, k] * b.data[k, j];
        return result;
    }

    public static Matrix Multiply(Matrix a, int number)
    {
        Matrix result = new Matrix(a.Rows, a.Columns);
        for (int i = 0; i < a.Rows; i++)
            for (int j = 0; j < a.Columns; j++)
                result.data[i, j] = a.data[i, j] * number;
        return result;
    }

    public Matrix Transponir()
    {
        Matrix result = new Matrix(Columns, Rows);
        for (int i = 0; i < Rows; i++)
            for (int j = 0; j < Columns; j++)
                result.data[j, i] = data[i, j];
        return result;
    }
}

class Program
{
    static void Main()
    {
        Console.Write("Введите количество строк первой матрицы: ");
        int rows1 = int.Parse(Console.ReadLine());
        Console.Write("Введите количество столбцов первой матрицы: ");
        int cols1 = int.Parse(Console.ReadLine());

        Console.Write("Введите количество строк второй матрицы: ");
        int rows2 = int.Parse(Console.ReadLine());
        Console.Write("Введите количество столбцов второй матрицы: ");
        int cols2 = int.Parse(Console.ReadLine());

        Console.WriteLine();

        Matrix m1 = new Matrix(rows1, cols1);
        Matrix m2 = new Matrix(rows2, cols2);

        Console.WriteLine("Заполнить матрицу 1: 1 - случайно");
        Console.WriteLine("                     2 - вручную");
        int choice = int.Parse(Console.ReadLine());

        if (choice == 1)
        {
            m1.FillRandom();
        }
        else
        {
            Console.WriteLine("Заполнение матрицы 1:");
            m1.Fill();
        }
        Console.WriteLine();

        Console.WriteLine("Заполнить матрицу 2: 1 - случайно");
        Console.WriteLine("                     2 - вручную");

        choice = int.Parse(Console.ReadLine());

        if (choice == 1)
        {
            m2.FillRandom();
        }
        else
        {
            Console.WriteLine("Заполнение матрицы 2:");
            m2.Fill();
        }
        Console.WriteLine();

        Console.WriteLine("Матрица 1:");
        m1.Print();

        Console.WriteLine("Матрица 2:");
        m2.Print();

        if (m1.Rows == m2.Rows && m1.Columns == m2.Columns)
        {
            Matrix sum = Matrix.Add(m1, m2);
            Console.WriteLine("1. сложение матриц:");
            sum.Print();
        }
        else
        {
            Console.WriteLine("1. сложение невозможно");
            Console.WriteLine($"   ({m1.Rows}x{m1.Columns}) не равно ({m2.Rows}x{m2.Columns})");
            Console.WriteLine();
        }

        if (m1.Columns == m2.Rows)
        {
            Matrix product = Matrix.Multiply(m1, m2);
            Console.WriteLine("2. умножение матриц:");
            Console.WriteLine($"   ({m1.Rows}x{m1.Columns}) * ({m2.Rows}x{m2.Columns}) = ({m1.Rows}x{m2.Columns})");
            product.Print();
        }
        else
        {
            Console.WriteLine("2. умножение матриц невозможно");
            Console.WriteLine();
        }

        Console.WriteLine("3. умножение на число:");

        Console.WriteLine("Матрица 1 * 3:");
        Matrix scaled1 = Matrix.Multiply(m1, 3);
        scaled1.Print();

        Console.WriteLine("Матрица 2 * 3:");
        Matrix scaled2 = Matrix.Multiply(m2, 3);
        scaled2.Print();

        Console.WriteLine("4. транспонирование:");

        Matrix transposed1 = m1.Transponir();
        Console.WriteLine("Транспонированная матрица 1:");
        Console.WriteLine($"   ({m1.Rows}x{m1.Columns}) - ({transposed1.Rows}x{transposed1.Columns})");
        transposed1.Print();

        Matrix transposed2 = m2.Transponir();
        Console.WriteLine("Транспонированная матрица 2:");
        Console.WriteLine($"   ({m2.Rows}x{m2.Columns}) - ({transposed2.Rows}x{transposed2.Columns})");
        transposed2.Print();

    }
}