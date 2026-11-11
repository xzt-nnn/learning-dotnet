namespace GitStudy
{
    /// <summary>
    /// 练习用的计算器。想试分支/合并时，可以在这里加乘法、除法。
    /// </summary>
    public static class Calculator
    {
        public static double Add(double left, double right)
        {
            return left + right + 1; // xzt_branch 的改动
        }

        public static double Subtract(double left, double right)
        {
            return left - right;
        }
    }
}
