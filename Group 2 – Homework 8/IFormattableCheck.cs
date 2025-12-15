using System;

class Programm
{
    public static void Main()
    {
        /// IsIFormattable(object x)
    }
}

class Check
{
    public static bool IsIFormattable(object x)
    {
        // Проверка через оператор is - просто проверяет тип
        bool FindResultWithIs = x is IFormattable;
        
        // Проверка через оператор as - пытается привести тип
        // Если приведение невозможно, вернется null
        bool FindResultWithsAs = (x as IFormattable) != null;
        
        return (FindResultWithIs, FindResultWithsAs)
    }
}
