using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandycrushProject
{
    public class Node
    {
        public NodeData data;
        
        //public Node top;
        //public Node down;
        public Node left;
        public Node right;

        public Node(NodeData data)
        {
            this.data = data;
            this.right = null;
            this.left = null;
         }
        public Node()
        {
            this.data = new NodeData();
            //this.top = null;
            //this.down = null;
            this.right = null;
            this.left = null;
        }

        public Node GetUpNode(Node node)
        {
            if (node != null && node.left != null && node.left.left != null && node.left.left.left != null && node.left.left.left.left != null && node.left.left.left.left.left != null)
                return node.left.left.left.left.left;
            return null;
        }
        public Node GetDownNode(Node node)
        {
            if (node !=null && node.right != null && node.right.right != null && node.right.right.right != null && node.right.right.right.right != null && node.right.right.right.right.right != null)
                return node.right.right.right.right.right;
            return null;
        }

        //public Node SetNodeName(Node node)
        //{
        //    var cuurentNode = node;
        //    switch (cuurentNode.data.Score)
        //    {
        //        case 1:
        //            cuurentNode.data.Name = "$";
        //            break;
        //        case 2:
        //            cuurentNode.data.Name = "*";
        //            break;
        //        case 3:
        //            cuurentNode.data.Name = "@";
        //            break;
        //        case 4:
        //            cuurentNode.data.Name = "&";
        //            break;
        //        default:
        //            break;

        //    }
        //    return cuurentNode;
        //}
       /* public object Data
        {
            get { return this.data; }
            set { this.data = value; }
        }
        /*
        public Node Top
        {
            get { return this.top; }
            set { this.top = value; }
        }
        public Node Right
        {
            get { return this.right; }
            set { this.right = value; }
        }
        public Node Down
        {
            get { return this.down; }
            set { this.down = value; }
        }
        public Node Left
        {
            get { return this.left; }
            set { this.left = value; }
        }*/

    }
}

