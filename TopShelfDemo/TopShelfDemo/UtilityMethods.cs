using System.IO;

namespace TopShelfDemo
{
    public static class UtilityMethods
    {
        public static bool GetExclusiveAccess(string filePath)
        {
            try
            {
                using (FileStream file = new FileStream(filePath, FileMode.Append, FileAccess.Write))
                {
                    file.Close();
                    return true;
                }
            }
            catch (IOException)
            {
                return false;
            }
        }
    }
}