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