using System.IO;
using System.Threading.Tasks;

namespace SidPortfolio.Helper
{
    public static class FileHelper
    {
        public  static async Task<byte[]> ConvertToByteArray(string filePath)
        {
            byte[] fileData;
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader reader = new BinaryReader(fs))
                {
                    fileData = reader.ReadBytes((int)fs.Length);
                }
            }
            return fileData;
        }
    }
}
