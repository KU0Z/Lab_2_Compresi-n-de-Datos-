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
        public HuffmanNode root { get; set; }
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

        public HuffmanNode CreateTree()
        {
           
            while (data.Count > 1)
            {
                HuffmanNode auxA = data[0];
                data.Remove(data[0]);
                HuffmanNode auxB = data[0];
                data.Remove(data[0]);
                HuffmanNode Parent = new HuffmanNode(auxA, auxB);
                data.Add(Parent); 
                data.Sort();
                this.root = data.FirstOrDefault();
            }
            return root; 
        }

        public void GetCodes(string code, HuffmanNode nodes)
        {
            if(nodes == null)
            {
                return; 
            }
            if(nodes.nodeLeft == null && nodes.nodeRight == null)
            {
                nodes.code = code;
                return;
            }
            GetCodes(code + "0", nodes.nodeLeft);
            GetCodes(code + "1", nodes.nodeRight);
        }
    }
}
