namespace AdvancedScada.Management.BLManager
{
    public static class Extensions
    {
        public static int IsNumeric(this string strg)
        {
            int result = 0;
            char[] array = strg.ToCharArray();
            string numberStrg = string.Empty;
            foreach (char c in array)
            {
                if (char.IsDigit(c))
                {
                    numberStrg += c;
                }
            }

            if (!string.IsNullOrEmpty(numberStrg) && !string.IsNullOrWhiteSpace(numberStrg))
            {
                result = int.Parse(numberStrg);
            }

            return result;
        }
    }
}