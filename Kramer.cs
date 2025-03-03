using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Введите размерность системы (количество уравнений и неизвестных):");
        int n = int.Parse(Console.ReadLine());
        double[,] A = new double[n, n];
        double[] b = new double[n];
        Console.WriteLine("Введите матрицу коэффициентов (по строкам, элементы разделяйте пробелами):");
        for (int i = 0; i < n; i++)
        {
            string[] row = Console.ReadLine().Split(' ');
            for (int j = 0; j < n; j++)
            {
                A[i, j] = double.Parse(row[j]);
            }
        }
        Console.WriteLine("Введите вектор свободных членов (элементы разделяйте пробелами):");
        string[] vector = Console.ReadLine().Split(' ');
        for (int i = 0; i < n; i++)
        {
            b[i] = double.Parse(vector[i]);
        }
        try
        {
            double[] x = CramerRule(A, b);
            Console.WriteLine("Решение системы:");
            for (int i = 0; i < x.Length; i++)
            {
                Console.WriteLine($"x[{i + 1}] = {x[i]}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    static double[] CramerRule(double[,] A, double[] b)
    {
        int n = b.Length;
        double[,] tempA = new double[n, n];
        double[] x = new double[n];

        double detA = Determinant(A);

        if (detA == 0)
        {
            throw new Exception("Определитель матрицы A равен нулю, метод Крамера не применим.");
        }

        for (int i = 0; i < n; i++)
        {
            Array.Copy(A, tempA, A.Length);

            for (int j = 0; j < n; j++)
            {
                tempA[j, i] = b[j];
            }

            double detAi = Determinant(tempA);
            x[i] = detAi / detA;
        }
        return x;
    }

    static double Determinant(double[,] matrix)
    {
        int n = matrix.GetLength(0);
        if (n != matrix.GetLength(1))
        {
            throw new Exception("Матрица должна быть квадратной.");
        }
        if (n == 1)
        {
            return matrix[0, 0];
        }
        if (n == 2)
        {
            return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
        }
        double det = 0;
        for (int i = 0; i < n; i++)
        {
            double[,] subMatrix = new double[n - 1, n - 1];
            for (int j = 1; j < n; j++)
            {
                for (int k = 0, l = 0; k < n; k++)
                {
                    if (k == i)
                    {
                        continue;
                    }
                    subMatrix[j - 1, l] = matrix[j, k];
                    l++;
                }
            }
            det += matrix[0, i] * (i % 2 == 0 ? 1 : -1) * Determinant(subMatrix);
        }
        return det;
    }
}