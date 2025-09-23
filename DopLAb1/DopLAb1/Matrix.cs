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