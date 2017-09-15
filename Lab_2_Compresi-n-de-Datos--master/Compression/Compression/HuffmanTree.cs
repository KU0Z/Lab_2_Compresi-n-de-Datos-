using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compression
{
    class HuffmanTree
    {
        List<HuffmanNode> data = new List<HuffmanNode>(); 
        public void GetFrequencies(byte[] fileData)
        {
            for (int i = 0; i < fileData.Length; i++)
            {
                string currentCharacter = Convert.ToChar(fileData[i]).ToString();
                if (data.Exists(x => x.character == currentCharacter))
                {
                    data[data.FindIndex(y => y.character == currentCharacter)].frequency++;
                }
                else
                {
                    HuffmanNode temp = new HuffmanNode(currentCharacter);
                    data.Add(temp);
                }
            }
            data.Sort(); 
        }
    }
}
