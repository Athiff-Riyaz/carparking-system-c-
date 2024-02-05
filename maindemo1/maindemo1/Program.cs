internal class Program
{
    private static void Main(string[] args)
    {
        int[] marks = new int[4];
        marks = { 23, 45, 67, 89, 56, 13 };
        for (int i=0; i < marks.Length; i++)
        {
            Console.WriteLine("Input a number:");
            marks[i] = Convert.ToInt32(Console.ReadLine());
        }
        //find max using max method
        int maxMark = marks.Max();
        Console.WriteLine("The maximum mark is: " + maxMark);

        //find max using for loop
        int max = marks[0];

        for (int i = 1; i < marks.Length; i++)
        {
            if (marks[i] > max)
            {
                max = marks[i];
            }
        }

        Console.WriteLine("The maximum value is: " + max);



    }
}