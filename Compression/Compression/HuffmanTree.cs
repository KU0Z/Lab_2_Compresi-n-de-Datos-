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
        Dictionary<byte, HuffmanNode> table = new Dictionary<byte, HuffmanNode>();
        private string encodedText = "";
        private string fileExtension;
        string code;
        public string _fileExtension
        {
            get { return fileExtension; }
            set { fileExtension = value; }
        }
        public HuffmanNode root { get; set; }
        public HuffmanTree(string extension)
        {
            _fileExtension = extension;
        }

        public byte[] EncodeText(byte[] fileData, string fileExtension)
        {
        //    for (int i = 0; i < fileData.Length; i++)
        //    {
                
        //        //string currentCharacter = Convert.ToChar(fileData[i]).ToString();
        //        code = table[fileData[i]].code;
        //        encodedText += code;
        //        //SearchNode(currentCharacter, root);            
        //    }

            foreach(byte bytes in fileData)
            {


                encodedText += table[bytes].code;
            }

            return BinaryToByte(encodedText, fileExtension);                
        }
        public byte[] BinaryToByte(string bits, string fileExtension)
        {
            int numBytes = (int)Math.Ceiling(bits.Length / 8m);
            int finalBytes = bits.Length % 8;
            byte[] byteExtension = Encoding.ASCII.GetBytes(fileExtension);
            //byte[] bytes = new byte[numBytes];
            List<byte> bytes = new List<byte>();
            const int size = 8;
            int counter = 0;

            foreach (KeyValuePair<byte, HuffmanNode> node in table)
            {
                HuffmanNode temp = node.Value;
                int residue = temp.code.Length % 8;
                if (temp.code.Length < 8)
                {
                    bytes.Add(temp.encodedCharacter);
                    bytes.Add(Convert.ToByte(temp.code, 2));
                }
                else
                {
                    for (int i = 0; i < (temp.code.Length / 8); i++)
                    {
                        counter++;
                        bytes.Add(temp.encodedCharacter);
                        bytes.Add(Convert.ToByte(temp.code.Substring(0, size), 2));
                    }
                    if (residue != 0)
                    {
                        counter++;
                        bytes.Add(temp.encodedCharacter);
                        bytes.Add(Convert.ToByte(temp.code.Substring(size - residue, residue), 2));
                    }
                    counter--;
                }
                //bytes.Insert(0, byte outa);
            }
            int jump = (table.Count + counter) * 2;
            int divJump = jump / 255;
            int modJump = jump % 255;
            bytes.Insert(0, Convert.ToByte(modJump));
            bytes.Insert(0, Convert.ToByte(divJump));
            //Extencion
            byte bytesCantidad = Convert.ToByte(byteExtension.Length);
            bytes.Add(bytesCantidad);
            for (int i = 0; i < byteExtension.Length; i++)
            {
                bytes.Add(byteExtension[i]);
            }
            for (int i = 0; i < numBytes -1; i++)
            {
                bytes.Add(Convert.ToByte(bits.Substring((size)*i, size), 2));
            }
            if(finalBytes != 0)
            {
                bytes.Add(Convert.ToByte(bits.Substring((bits.Length - finalBytes), finalBytes),2));
            }

            return bytes.ToArray();
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
                    HuffmanNode temp = new HuffmanNode(currentCharacter, fileData[i]);
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
                table.Add(nodes.encodedCharacter, nodes);
                return;
            }
            GetCodes(code + "0", nodes.nodeLeft);
            GetCodes(code + "1", nodes.nodeRight);
        }
        
    }
}
