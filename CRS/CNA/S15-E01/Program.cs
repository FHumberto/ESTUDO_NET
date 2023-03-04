namespace S15_E01
{
    internal static class Program
    {
        public static void Main()
        {
            char[] course = new char[3] { 'A', 'B', 'C' };
            HashSet<int>[]? registeredStudents = new HashSet<int>[3];

            for (int i = 0; i < course.Length; i++)
            {
                registeredStudents[i] = new();

                Console.Write($"How many students for course {course[i]}? ");
                int studentsNumber = int.Parse(Console.ReadLine());
                for (int j = 0; j < studentsNumber; j++)
                {
                    registeredStudents[i].Add(int.Parse(Console.ReadLine()));
                }
            }
            HashSet<int> allStudents = new HashSet<int>(registeredStudents[0]);
            allStudents.UnionWith(registeredStudents[1]);
            allStudents.UnionWith(registeredStudents[2]);
            Console.Write($"Total Students: {allStudents.Count()}");
        }
    }
}