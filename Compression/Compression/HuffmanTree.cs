using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading.Tasks;

namespace Compression
{
    class HuffmanTree
    {
        List<HuffmanNode> data = new List<HuffmanNode>();
        private string encodedText = "";
        public HuffmanNode root { get; set; }
        public byte[] EncodeText(byte[] fileData)
        {
            for (int i = 0; i < fileData.Length; i++)
            {
                string currentCharacter = Convert.ToChar(fileData[i]).ToString();
                SearchNode(currentCharacter, root);            
            }
            return BinaryToByte(encodedText);                
        }
        public byte[] BinaryToByte(string bits)
        {
            int numBytes = (int)Math.Ceiling(bits.Length / 8m);
            int finalBytes = bits.Length % 8;
            byte[] bytes = new byte[numBytes];
            int size = 8;
            for (int i = 0; i < numBytes -1; i++)
            {
                string prueba = bits.Substring(520, 2);
                bytes[i] = Convert.ToByte(bits.Substring((size)*i, size), 2);
            }
            if(finalBytes != 0)
            {
                bytes[numBytes -1] = Convert.ToByte(bits.Substring((bits.Length - finalBytes), finalBytes));
            }
            return bytes;
        }
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
        public void SearchNode(string character, HuffmanNode nodes)
        {
            if(nodes != null)
            {
                if (nodes.character == character)
                {
                    encodedText += nodes.code;
                }
                if (nodes.code == "")
                {
                    SearchNode(character, nodes.nodeLeft);
                    SearchNode(character, nodes.nodeRight);
                }
                else
                {
                    if (nodes.nodeLeft != null)
                    {
                        SearchNode(character, nodes.nodeLeft);
                    }
                    if (nodes.nodeRight != null)
                    {
                        SearchNode(character, nodes.nodeRight);
                    }
                }
            }
            
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
