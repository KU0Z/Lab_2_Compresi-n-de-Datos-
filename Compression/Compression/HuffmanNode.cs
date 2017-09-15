using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compression
{
    class HuffmanNode : IComparable<HuffmanNode>
    {
        public string character;
        public int frequency;
        public HuffmanNode nodeRight;
        public HuffmanNode nodeLeft;
        public HuffmanNode parent;
        public string code; 

        public HuffmanNode(string _character)
        {
            character = _character;
            frequency = 1;
            nodeLeft = null;
            nodeRight = null;
            parent = null;
            code = "";
        }
        //Combine two nodes
        public HuffmanNode(HuffmanNode nodeA, HuffmanNode nodeB)
        {
            frequency = nodeA.frequency + nodeB.frequency;
            nodeA.parent = this;
            nodeB.parent = this;

            if (nodeA.frequency <= nodeB.frequency)
            {
                character = nodeA.character + nodeB.character;
                nodeLeft = nodeA;
                nodeRight = nodeB;
                code = "";
            }
            else
            {
                character = nodeB.character + nodeA.character;
                nodeLeft = nodeB;
                nodeRight = nodeA;
                code = "";
            }
        }

        public int CompareTo(HuffmanNode otherNode) 
        {
            return this.frequency.CompareTo(otherNode.frequency);
        }

    }
}
