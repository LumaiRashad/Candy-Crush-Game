using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandycrushProject
{

    public class LinkedCandies
    {
        private int size;
        public Node TopL, RDown;
        public int TotalScore = 0;

        public LinkedCandies()
        {
            size = 25;
            TopL = RDown = null;
        }
        public void CandiesGrid()
        {
            DLinked candiesLinkedList = new DLinked();
            Random rnd = new Random();
            for (int i = 0; i < size; i++)
            {
                Node newNode = new Node();
                newNode.data.Score = rnd.Next(1, 5);
                newNode = SetNodeName(newNode);

                //int nod = rnd.Next(1, 5);
                //obj.add(nod);
                candiesLinkedList.add(newNode);
                if (i == 0)
                {
                    TopL = candiesLinkedList.head;
                }
                if (i == 24)
                {
                    RDown = candiesLinkedList.tail;
                }
            }

            CheckGrid(candiesLinkedList);
            candiesLinkedList.Print();
            Play(candiesLinkedList);

        }
        private void CheckGrid(DLinked CandiesGrid)
        {
            Node currentNode = CandiesGrid.head;
            for (int i = 0; i < 25; i++)
            {
                if (MatchNodeWith2Right(currentNode) == true)
                    GenerateNewRandom(currentNode.right.right);

                if (MatchNodeWith2Left(currentNode) == true)
                    GenerateNewRandom(currentNode);//change current node not the Up nodes to make sure all previous nodes are not the same

                if (MatchNodeWith2Up(currentNode) == true)
                    GenerateNewRandom(currentNode);//change current node not the Up nodes to make sure all previous nodes are not the same


                if (MatchNodeWith2Down(currentNode) == true)
                {
                    var firstDown = currentNode.GetDownNode(currentNode);
                    var secondDown = currentNode.GetDownNode(firstDown);
                    GenerateNewRandom(secondDown);// change last down node to make sure all previous nodes are not changed
                }

                currentNode = currentNode.right;
            }

            //TO DO: check if the grid has no available moves , then game over

        }

        public void Play(DLinked candiesLinkedList)
        {
            //1- Get Index From User with validation from 0 to 24 ,numbers only
            string indexInput = ReadInput("Enter the number of index ,you want to move ( from 0 t0 24)" , ConsoleColor.Magenta);
            int index = ValidatIndex(indexInput);

            string directionInput = ReadInput("What is the Direction you want to move the candy to ?!", ConsoleColor.Magenta);
            string direction = ValidateDirection(index, directionInput, candiesLinkedList);
            
            MoveCandy(index, direction, candiesLinkedList);

            
        }



        public Node SwapNodes(Node CurrentNode,Node nodeToSwap)
         {
             if (nodeToSwap == null) return null;

             Node tempNode = new Node();
             tempNode.data = nodeToSwap.data;

             nodeToSwap.data = CurrentNode.data;
             CurrentNode.data = tempNode.data;

             return nodeToSwap;
         }
         
 

        private void MoveCandy(int index, string direction, DLinked candiesLinkedList)
        {
            //2- Get the node having this index 
            Node currentNode = GetNodeWithIndex(candiesLinkedList, index);

            Node myUpNode = currentNode.GetUpNode(currentNode);
            Node myDownNode = currentNode.GetDownNode(currentNode);
            Node myRightNode = currentNode.right;
            Node myLeftNode = currentNode.left;
            Node nodeToSwap = new Node();

            Node currentNodeAfterSwap = new Node();
            switch (direction)
            {
                case "right":
                    nodeToSwap = myRightNode;
                    MoveCandyRight(currentNode, nodeToSwap, candiesLinkedList, index, direction);
                    break;
                case "left":
                    nodeToSwap = myLeftNode;
                    MoveCandyLeft(currentNode, nodeToSwap, candiesLinkedList, index, direction);
                    break;
                case "up":
                    nodeToSwap = myUpNode;
                    MoveCandyUp(currentNode, nodeToSwap, candiesLinkedList, index, direction);
                    break;
                case "down":
                    nodeToSwap = myDownNode;
                    MoveCandyDown(currentNode, nodeToSwap, candiesLinkedList, index, direction);
                    break;
                default:
                    break;

            }


            //Console.WriteLine("node to swap after swap " + cu.data.Name);
            Console.WriteLine("Total Score = " + TotalScore);
            Play(candiesLinkedList);

            //Un Match
            

        }

        private void MoveCandyRight(Node currentNode, Node nodeToSwap, DLinked candiesLinkedList, int index, string direction)
        {
           Node currentNodeAfterSwap = new Node();
            // Match 5
            if (MatchTwoUpTwoDown(currentNode, nodeToSwap))
            {
                currentNodeAfterSwap = SwapNodes(currentNode, nodeToSwap);
                candiesLinkedList.Print();
                TotalScore += 5 * currentNodeAfterSwap.data.Score;
                CrushFiveCandiesVertical(currentNodeAfterSwap, candiesLinkedList);
            }
            // Match 4
            else if (MatchTwoUpOneDown(currentNode, nodeToSwap))
            {
                currentNodeAfterSwap = SwapNodes(currentNode, nodeToSwap);
                candiesLinkedList.Print();
                TotalScore += 4 * currentNodeAfterSwap.data.Score;
                Node currentUp = currentNodeAfterSwap.GetUpNode(currentNodeAfterSwap);
                Node startNode = currentUp.GetUpNode(currentUp);
                CrushFourCandiesVertical(startNode, candiesLinkedList);
            }
            else if (MatchTwoDownOneUp(currentNode, nodeToSwap))
            {
                currentNodeAfterSwap = SwapNodes(currentNode, nodeToSwap);
                candiesLinkedList.Print();
                TotalScore += 4 * currentNodeAfterSwap.data.Score;
                Node startNode = currentNodeAfterSwap.GetUpNode(currentNodeAfterSwap);
                CrushFourCandiesVertical(startNode, candiesLinkedList);
            }
            // Match 3
            else if (MatchTwoRight(currentNode, nodeToSwap))
            {
                currentNodeAfterSwap = SwapNodes(currentNode, nodeToSwap);
                candiesLinkedList.Print();
                TotalScore += 3 * currentNodeAfterSwap.data.Score;
                CrushCandyWithTwoRightCandies(currentNodeAfterSwap, candiesLinkedList);

            }

            else if (MatchTwoUp(currentNode, nodeToSwap))
            {
                currentNodeAfterSwap = SwapNodes(currentNode, nodeToSwap);
                candiesLinkedList.Print();
                TotalScore += 3 * currentNodeAfterSwap.data.Score;
                CrushCandyWithTwoUpCandies(currentNodeAfterSwap, candiesLinkedList);

            }
            else if (MatchTwoDown(currentNode, nodeToSwap))
            {
                currentNodeAfterSwap = SwapNodes(currentNode, nodeToSwap);
                candiesLinkedList.Print();
                TotalScore += 3 * currentNodeAfterSwap.data.Score;

                CrushCandyWithTwoDownCandies(currentNodeAfterSwap, candiesLinkedList);
            }
            else if (MatchOneUpOneDown(currentNode, nodeToSwap))
            {
                currentNodeAfterSwap = SwapNodes(currentNode, nodeToSwap);
                candiesLinkedList.Print();
                TotalScore += 3 * currentNodeAfterSwap.data.Score;
                Node currentUp = currentNodeAfterSwap.GetUpNode(currentNodeAfterSwap);
                CrushCandyWithTwoDownCandies(currentUp, candiesLinkedList);
                //swap 3 verticla 
                //generate
            }
          
            else
            {
                DisplayUnmatchedDirection(index, direction, candiesLinkedList);
            }
            
        }
        private void MoveCandyLeft(Node currentNode, Node nodeToSwap, DLinked candiesLinkedList, int index, string direction)
        {
            Node currentNodeAfterSwap = new Node();
            
            if (MatchTwoUpTwoDown(currentNode, nodeToSwap))
            {
                currentNodeAfterSwap = SwapNodes(currentNode, nodeToSwap);
                candiesLinkedList.Print();
                TotalScore += 5 * currentNodeAfterSwap.data.Score;
                CrushFiveCandiesVertical(currentNodeAfterSwap, candiesLinkedList);
            }
            else if (MatchTwoLeft(currentNode, nodeToSwap))
            {
                currentNodeAfterSwap = SwapNodes(currentNode, nodeToSwap);
                candiesLinkedList.Print();
                TotalScore += 3 * currentNodeAfterSwap.data.Score;
                CrushCandyWithTwoLeftCandies(currentNodeAfterSwap, candiesLinkedList);

            }
            else if (MatchTwoUpOneDown(currentNode, nodeToSwap))
            {
                currentNodeAfterSwap = SwapNodes(currentNode, nodeToSwap);
                candiesLinkedList.Print();
                TotalScore += 4 * currentNodeAfterSwap.data.Score;
                Node currentUp = currentNodeAfterSwap.GetUpNode(currentNodeAfterSwap);
                Node startNode = currentUp.GetUpNode(currentUp);
                CrushFourCandiesVertical(startNode, candiesLinkedList);
            }
            else if (MatchTwoDownOneUp(currentNode, nodeToSwap))
            {
                currentNodeAfterSwap = SwapNodes(currentNode, nodeToSwap);
                candiesLinkedList.Print();
                TotalScore += 4 * currentNodeAfterSwap.data.Score;
                Node startNode = currentNodeAfterSwap.GetUpNode(currentNodeAfterSwap);
                CrushFourCandiesVertical(startNode, candiesLinkedList);
            }
            else if (MatchTwoUp(currentNode, nodeToSwap))
            {
                currentNodeAfterSwap = SwapNodes(currentNode, nodeToSwap);
                candiesLinkedList.Print();
                TotalScore += 3 * currentNodeAfterSwap.data.Score;
                CrushCandyWithTwoUpCandies(currentNodeAfterSwap, candiesLinkedList);

            }
            else if (MatchTwoDown(currentNode, nodeToSwap))
            {
                currentNodeAfterSwap = SwapNodes(currentNode, nodeToSwap);
                candiesLinkedList.Print();
                TotalScore += 3 * currentNodeAfterSwap.data.Score;
                CrushCandyWithTwoDownCandies(currentNodeAfterSwap, candiesLinkedList);

            }
            else if (MatchOneUpOneDown(currentNode, nodeToSwap))
            {
                currentNodeAfterSwap = SwapNodes(currentNode, nodeToSwap);
                TotalScore += 3 * currentNodeAfterSwap.data.Score;
                Node currentUp = currentNodeAfterSwap.GetUpNode(currentNodeAfterSwap);
                CrushCandyWithTwoDownCandies(currentUp, candiesLinkedList);
            }

            else
            {
                DisplayUnmatchedDirection(index, direction, candiesLinkedList);
            }
           
        }
        private void MoveCandyUp(Node currentNode, Node nodeToSwap, DLinked candiesLinkedList, int index, string direction)
        {
            Node currentNodeAfterSwap = new Node();
            if (MatchTwoRightTwoLeft(currentNode, nodeToSwap))
            {
                currentNodeAfterSwap = SwapNodes(currentNode, nodeToSwap);
                candiesLinkedList.Print();
                TotalScore += 5 * currentNodeAfterSwap.data.Score;
                CrushFiveCandiesHorizontal(currentNodeAfterSwap, candiesLinkedList);
            }
            else if (MatchTwoRightOneLeft(currentNode, nodeToSwap))
            {
                currentNodeAfterSwap = SwapNodes(currentNode, nodeToSwap);
            }
            else if (MatchTwoLeftOneRight(currentNode, nodeToSwap))
            {
                currentNodeAfterSwap = SwapNodes(currentNode, nodeToSwap);
            }
            else if (MatchTwoUp(currentNode, nodeToSwap))
            {
                currentNodeAfterSwap = SwapNodes(currentNode, nodeToSwap);
                candiesLinkedList.Print();
                TotalScore += 3 * currentNodeAfterSwap.data.Score;
                CrushCandyWithTwoUpCandies(currentNodeAfterSwap, candiesLinkedList);
            }
            else if (MatchTwoRight(currentNode, nodeToSwap))
            {
                currentNodeAfterSwap = SwapNodes(currentNode, nodeToSwap);
                candiesLinkedList.Print();
                TotalScore += 3 * currentNodeAfterSwap.data.Score;
                CrushCandyWithTwoRightCandies(currentNodeAfterSwap, candiesLinkedList);
            }
            else if (MatchTwoLeft(currentNode, nodeToSwap))
            {
                currentNodeAfterSwap = SwapNodes(currentNode, nodeToSwap);
                candiesLinkedList.Print();
                TotalScore += 3 * currentNodeAfterSwap.data.Score;
                CrushCandyWithTwoLeftCandies(currentNodeAfterSwap, candiesLinkedList);
            }
            else if (MatchOneRightOneLeft(currentNode, nodeToSwap))
            {
                currentNodeAfterSwap = SwapNodes(currentNode, nodeToSwap);
            }          
            else
            {
                DisplayUnmatchedDirection(index, direction, candiesLinkedList);
            }
        }
        private void MoveCandyDown(Node currentNode, Node nodeToSwap, DLinked candiesLinkedList, int index, string direction)
        {
            Node currentNodeAfterSwap = new Node();
            if (MatchTwoRightTwoLeft(currentNode, nodeToSwap))
            {
                currentNodeAfterSwap = SwapNodes(currentNode, nodeToSwap);
                candiesLinkedList.Print();
                TotalScore += 5 * currentNodeAfterSwap.data.Score;
                CrushFiveCandiesHorizontal(currentNodeAfterSwap, candiesLinkedList);
            }
            else if (MatchTwoRightOneLeft(currentNode, nodeToSwap))
            {
                currentNodeAfterSwap = SwapNodes(currentNode, nodeToSwap);
            }
            else if (MatchTwoLeftOneRight(currentNode, nodeToSwap))
            {
                currentNodeAfterSwap = SwapNodes(currentNode, nodeToSwap);
            }
            else if (MatchTwoDown(currentNode, nodeToSwap))
            {
                currentNodeAfterSwap = SwapNodes(currentNode, nodeToSwap);
                candiesLinkedList.Print();
                TotalScore += 3 * currentNode.data.Score;
                CrushCandyWithTwoDownCandies(currentNodeAfterSwap, candiesLinkedList);
            }
            else if (MatchTwoRight(currentNode, nodeToSwap))
            {
                currentNodeAfterSwap = SwapNodes(currentNode, nodeToSwap);
                candiesLinkedList.Print();
                TotalScore += 3 * currentNode.data.Score;
                CrushCandyWithTwoRightCandies(currentNodeAfterSwap, candiesLinkedList);
            }
            else if (MatchTwoLeft(currentNode, nodeToSwap))
            {
                currentNodeAfterSwap = SwapNodes(currentNode, nodeToSwap);
                candiesLinkedList.Print();
                TotalScore += 3 * currentNode.data.Score;
                CrushCandyWithTwoLeftCandies(currentNodeAfterSwap, candiesLinkedList);
            }
            else if (MatchOneRightOneLeft(currentNode, nodeToSwap))
            {
                currentNodeAfterSwap = SwapNodes(currentNode, nodeToSwap);
            }
            else
            {
                DisplayUnmatchedDirection(index, direction, candiesLinkedList);
            }
        }

        private void CrushCandyWithTwoRightCandies(Node currentNodeAfterSwap, DLinked candiesLinkedList)
        {
            Node newCurrentUp = new Node();

            while (newCurrentUp != null)
            {
                //swap current Up
                newCurrentUp = currentNodeAfterSwap.GetUpNode(currentNodeAfterSwap);
                if (newCurrentUp == null)
                {
                    currentNodeAfterSwap.data.Name = ".";
                    currentNodeAfterSwap.right.data.Name = ".";
                    currentNodeAfterSwap.right.right.data.Name = ".";

                    break;
                }
                else
                {
                    currentNodeAfterSwap = SwapNodes(currentNodeAfterSwap, newCurrentUp);

                    //swap current.right up
                    Node currentNodeAfterSwapRight = currentNodeAfterSwap.right;
                    Node newCurrentRightDown = currentNodeAfterSwapRight.GetDownNode(currentNodeAfterSwapRight);
                    currentNodeAfterSwapRight = SwapNodes(currentNodeAfterSwapRight, newCurrentRightDown);


                    //swap current right right up
                    Node currentNodeAfterSwapRightRight = currentNodeAfterSwapRight.right;
                    Node newCurrentRightRightUp = currentNodeAfterSwapRightRight.GetUpNode(currentNodeAfterSwapRightRight);
                    currentNodeAfterSwapRightRight = SwapNodes(currentNodeAfterSwapRightRight, newCurrentRightRightUp);

                }

            }
            Console.WriteLine("finish ");
            candiesLinkedList.Print();
            Console.WriteLine("Generating New Nodes ...: ");
            //generate Random
            GenerateNewRandom(currentNodeAfterSwap);
            GenerateNewRandom(currentNodeAfterSwap.right);
            GenerateNewRandom(currentNodeAfterSwap.right.right);
            CheckGrid(candiesLinkedList);

            candiesLinkedList.Print();
        }
        private void CrushCandyWithTwoLeftCandies(Node currentNodeAfterSwap, DLinked candiesLinkedList)
        {
            Node newCurrentUp = new Node();

            while (newCurrentUp != null)
            {
                //swap current Up
                newCurrentUp = currentNodeAfterSwap.GetUpNode(currentNodeAfterSwap);
                if (newCurrentUp == null)
                {
                    currentNodeAfterSwap.data.Name = ".";
                    currentNodeAfterSwap.left.data.Name = ".";
                    currentNodeAfterSwap.left.left.data.Name = ".";

                    break;
                }
                else
                {
                    currentNodeAfterSwap = SwapNodes(currentNodeAfterSwap, newCurrentUp);

                    //swap current.right up
                    Node currentNodeAfterSwapLeft = currentNodeAfterSwap.left;
                    Node newCurrentRightDown = currentNodeAfterSwapLeft.GetDownNode(currentNodeAfterSwapLeft);
                    currentNodeAfterSwapLeft = SwapNodes(currentNodeAfterSwapLeft, newCurrentRightDown);


                    //swap current right right up
                    Node currentNodeAfterSwapLeftLeft = currentNodeAfterSwapLeft.left;
                    Node newCurrentRightRightUp = currentNodeAfterSwapLeftLeft.GetUpNode(currentNodeAfterSwapLeftLeft);
                    currentNodeAfterSwapLeftLeft = SwapNodes(currentNodeAfterSwapLeftLeft, newCurrentRightRightUp);

                }

            }
            Console.WriteLine("finish ");
            candiesLinkedList.Print();
            Console.WriteLine("Generating New Nodes ...: ");
            //generate Random
            GenerateNewRandom(currentNodeAfterSwap);
            GenerateNewRandom(currentNodeAfterSwap.left);
            GenerateNewRandom(currentNodeAfterSwap.left.left);
            CheckGrid(candiesLinkedList);

            candiesLinkedList.Print();
        }
        private void CrushCandyWithTwoUpCandies(Node currentNodeAfterSwap, DLinked candiesLinkedList)
        {
            Node newCurrentUp = new Node();
            Node newCurrentUpUp = new Node();
            Node TopNode = new Node();
            while (TopNode != null)
            {
                //swap current Up
                newCurrentUp = currentNodeAfterSwap.GetUpNode(currentNodeAfterSwap);
                newCurrentUpUp = newCurrentUp.GetUpNode(newCurrentUp);

                TopNode = newCurrentUpUp.GetUpNode(newCurrentUpUp);

                if (TopNode == null)
                {
                    currentNodeAfterSwap.data.Name = ".";
                    newCurrentUp.data.Name = ".";
                    newCurrentUpUp.data.Name = ".";
                    break;
                }
                else
                {
                    currentNodeAfterSwap = SwapNodes(currentNodeAfterSwap, TopNode);
                    Node currentNodeAfterSwapUp = currentNodeAfterSwap.GetUpNode(currentNodeAfterSwap);
                    if (currentNodeAfterSwapUp == null)
                    {
                        currentNodeAfterSwap.data.Name = ".";
                        newCurrentUp.data.Name = ".";
                        newCurrentUpUp.data.Name = ".";
                        break;
                    }
                    else
                    {
                        Node currentNodeAfterSwapDown = currentNodeAfterSwap.GetDownNode(currentNodeAfterSwap);
                        Node currentNodeAfterSwapDownDown = currentNodeAfterSwapDown.GetDownNode(currentNodeAfterSwapDown);
                        currentNodeAfterSwapUp = SwapNodes(currentNodeAfterSwapUp, currentNodeAfterSwapDownDown);
                    }

                }

            }
            Console.WriteLine("finish ");
            candiesLinkedList.Print();
            Console.WriteLine("Generating New Nodes ...: ");
            //generate Random
            GenerateNewRandom(currentNodeAfterSwap);
            GenerateNewRandom(newCurrentUp);
            GenerateNewRandom(newCurrentUpUp);
            CheckGrid(candiesLinkedList);

            candiesLinkedList.Print();
        }
        private void CrushCandyWithTwoDownCandies(Node currentNodeAfterSwap, DLinked candiesLinkedList)
        {
            Node newCurrentDown = new Node();
            Node newCurrentDownDown = new Node();
            Node TopNode = new Node();
            while (TopNode != null)
            {
                
                newCurrentDown = currentNodeAfterSwap.GetDownNode(currentNodeAfterSwap);
                newCurrentDownDown = currentNodeAfterSwap.GetDownNode(newCurrentDown);
                TopNode = currentNodeAfterSwap.GetUpNode(currentNodeAfterSwap);
                if (TopNode == null)
                {
                    currentNodeAfterSwap.data.Name = ".";
                    newCurrentDown.data.Name = ".";
                    newCurrentDownDown.data.Name = ".";
                    break;
                }
                else
                {
                    currentNodeAfterSwap = SwapNodes(newCurrentDownDown, TopNode);
                    Node currentNodeAfterSwapUp = currentNodeAfterSwap.GetUpNode(currentNodeAfterSwap);
                    if (currentNodeAfterSwapUp == null)
                    {
                        currentNodeAfterSwap.data.Name = ".";
                        newCurrentDown = currentNodeAfterSwap.GetDownNode(currentNodeAfterSwap);
                        newCurrentDown.data.Name = ".";
                        newCurrentDownDown = newCurrentDown.GetDownNode(newCurrentDown);
                        newCurrentDownDown.data.Name = ".";
                        break;
                    }
                    else
                    {
                        currentNodeAfterSwap = SwapNodes(newCurrentDown, currentNodeAfterSwapUp);
                    }

                }

            }
            Console.WriteLine("Crushing ... ");
            candiesLinkedList.Print();
            Console.WriteLine("Generating New Nodes ...: ");
            //generate Random
            GenerateNewRandom(currentNodeAfterSwap);
            GenerateNewRandom(newCurrentDown);
            GenerateNewRandom(newCurrentDownDown);
            CheckGrid(candiesLinkedList);

            candiesLinkedList.Print();
        }
        private void CrushFourCandiesVertical(Node startNode, DLinked candiesLinkedList)
        {
            Node upNode = startNode.GetUpNode(startNode);
            Node startNodeDown = startNode.GetDownNode(startNode);
            Node startNodeDownDown = startNodeDown.GetDownNode(startNodeDown);
            Node startNodeDownDownDown = startNodeDownDown.GetDownNode(startNodeDownDown);
            if (upNode == null)
            {
                startNode.data.Name = ".";
                startNodeDown.data.Name = ".";
                startNodeDownDown.data.Name = ".";
                startNodeDownDownDown.data.Name = ".";
            }
            else
            {
                startNodeDownDownDown = SwapNodes(startNodeDownDownDown, upNode);
                startNode.data.Name = ".";
                startNodeDown.data.Name = ".";
                startNodeDownDown.data.Name = ".";
                startNodeDownDownDown.data.Name = ".";
            }

            Console.WriteLine("finish ");
            candiesLinkedList.Print();
            Console.WriteLine("Generating New Nodes ...: ");
            //generate Random
            GenerateNewRandom(startNode);
            GenerateNewRandom(startNodeDown);
            GenerateNewRandom(startNodeDownDown);
            GenerateNewRandom(startNodeDownDownDown);

            CheckGrid(candiesLinkedList);
            candiesLinkedList.Print();
        }
        private void CrushFiveCandiesVertical(Node currentNode, DLinked candiesLinkedList)
        {
           
            Node currentNodeUp = currentNode.GetUpNode(currentNode);
            Node currentNodeUpUp = currentNodeUp.GetUpNode(currentNodeUp);
            Node currentNodeDown = currentNode.GetDownNode(currentNode);
            Node currentNodeDownDown = currentNodeDown.GetDownNode(currentNodeDown);

            currentNode.data.Name = ".";
            currentNodeUp.data.Name = ".";
            currentNodeUpUp.data.Name = ".";
            currentNodeDown.data.Name = ".";
            currentNodeDownDown.data.Name = ".";

            Console.WriteLine("finish ");
            candiesLinkedList.Print();
            Console.WriteLine("Generating New Nodes ...: ");
            //generate Random
            GenerateNewRandom(currentNode);
            GenerateNewRandom(currentNodeUp);
            GenerateNewRandom(currentNodeUpUp);
            GenerateNewRandom(currentNodeDown);
            GenerateNewRandom(currentNodeDownDown);

            CheckGrid(candiesLinkedList);
            candiesLinkedList.Print();
        }

        private void CrushFiveCandiesHorizontal(Node currentNode, DLinked candiesLinkedList)
        {
            Node currentNodeRight = currentNode.right;
            Node currentNodeRightRight = currentNodeRight.right;
            Node currentNodeLeft = currentNode.left;
            Node currentNodeLeftLeft = currentNodeLeft.left;

            currentNode.data.Name = ".";
            currentNodeRight.data.Name = ".";
            currentNodeRightRight.data.Name = ".";
            currentNodeLeft.data.Name = ".";
            currentNodeLeftLeft.data.Name = ".";

            Console.WriteLine("finish ");
            candiesLinkedList.Print();
            Console.WriteLine("Generating New Nodes ...: ");
            //generate Random
            GenerateNewRandom(currentNode);
            GenerateNewRandom(currentNodeRight);
            GenerateNewRandom(currentNodeRightRight);
            GenerateNewRandom(currentNodeLeft);
            GenerateNewRandom(currentNodeLeftLeft);

            CheckGrid(candiesLinkedList);
            candiesLinkedList.Print();
        }

        private void DisplayUnmatchedDirection(int index, string direction ,DLinked candiesGrid)
        {
            DisplayMessage("Unmatched Candies, please select another direction, Or press N for New Index", ConsoleColor.Red);
            string directionInput = Console.ReadLine();
            if (directionInput.ToLower() == "n")
                Play(candiesGrid);
            else
                direction = ValidateDirection(index, directionInput, candiesGrid);

            MoveCandy(index, direction, candiesGrid);
        }
        // Match current Node with neighbours while drawing the Grid
        private bool MatchNodeWith2Right(Node currentNode)
        {
            if (currentNode != null && currentNode.right != null && currentNode.right.right != null &&
                currentNode.data.Score == currentNode.right.data.Score && currentNode.data.Score == currentNode.right.right.data.Score)
            {
                return true;
            }
            return false;
        }
        private bool MatchNodeWith2Left(Node currentNode)
        {
            if (currentNode != null && currentNode.left != null && currentNode.left.left != null &&
                currentNode.data.Score == currentNode.left.data.Score && currentNode.data.Score == currentNode.left.left.data.Score)
            {

                return true;
            }
            return false;
        }
        private bool MatchNodeWith2Up(Node currentNode)
        {
            var firstUp = currentNode.GetUpNode(currentNode);
            var secondUp = currentNode.GetUpNode(firstUp);
            if (currentNode != null && firstUp != null && secondUp != null &&
                currentNode.data.Score == firstUp.data.Score && currentNode.data.Score == secondUp.data.Score)
            {

                return true;
            }
            return false;
        }
        private bool MatchNodeWith2Down(Node currentNode)
        {
            var firstDown = currentNode.GetDownNode(currentNode);
            var secondDown = currentNode.GetDownNode(firstDown);

            if (currentNode != null && firstDown != null && secondDown != null &&
                currentNode.data.Score == firstDown.data.Score && currentNode.data.Score == secondDown.data.Score)
                return true;

            return false;
        }
 


        private void GenerateNewRandom(Node nodeToBeChanged)
        {
            Random rnd = new Random();
            int oldScore = nodeToBeChanged.data.Score;
            int randomScore = rnd.Next(1, 5);
            while (oldScore == randomScore)
            {
                randomScore = rnd.Next(1, 5);
            }

            nodeToBeChanged.data.Score = randomScore;
            nodeToBeChanged = SetNodeName(nodeToBeChanged);
        }
        private Node SetNodeName(Node node)
        {
            var cuurentNode = node;
            switch (cuurentNode.data.Score)
            {
                case 1:
                    cuurentNode.data.Name = "$";
                    break;
                case 2:
                    cuurentNode.data.Name = "*";
                    break;
                case 3:
                    cuurentNode.data.Name = "@";
                    break;
                case 4:
                    cuurentNode.data.Name = "&";
                    break;
                //case 5:
                //    Console.ForegroundColor = ConsoleColor.Blue;
                //    break;
                default:
                    break;

            }
            return cuurentNode;
        }
      
        private int ValidatIndex(string input)
        {
            int index;
            bool isNumber = int.TryParse(input,out index);
            while(!isNumber || (isNumber && ! (index >=0 && index <= 24)) ){
                input = ReadInput("Wrong Value, please enter Number( from 0 t0 24)", ConsoleColor.Red);
                isNumber = int.TryParse(input,out index);
            }

            return index;

        }
        private string ValidateDirection(int index, string direction,DLinked candiesLinkedList)
        {
            direction = direction.ToLower();
            while( !(direction == "up" || direction == "down" || direction == "right" || direction == "left")){
                direction = ReadInput("Wrong Direction !! please enter Again :", ConsoleColor.Red);
                direction = direction.ToLower();
            }

            string validDirection = ValidateDirectionWithIndex(index, direction);
            validDirection = ValidateSameNode(candiesLinkedList, index, validDirection);

            return validDirection;
        }
        private  string ValidateDirectionWithIndex(int index, string direction){

            if (direction == "left" && ( index == 0 || index == 5 || index == 10 || index == 15 || index == 20 ))
            {

                direction = ReadInput("Cannot move index " + index + " to left !! please enter another direction :", ConsoleColor.Red);
                direction = direction.ToLower();
                direction = ValidateDirectionWithIndex(index, direction);
            }
            if (direction == "right" && (index == 4 || index == 9 || index == 14 || index == 19 || index == 24))
            {

                direction = ReadInput("Cannot move index " + index + " to right !! please enter another direction :", ConsoleColor.Red);
                direction = direction.ToLower();
                direction = ValidateDirectionWithIndex(index, direction);
            }
            if (direction == "up" && (index == 0|| index == 1 || index == 2 || index == 3 || index == 4))
            {

                direction = ReadInput("Cannot move index " + index + " to up !! please enter another direction :", ConsoleColor.Red);
                direction = direction.ToLower();
                direction = ValidateDirectionWithIndex(index, direction);
            }
            if (direction == "down" && (index == 20 || index == 21 || index == 22 || index == 23 || index == 24))
            {

                direction = ReadInput("Cannot move index " + index + " to down !! please enter another direction :", ConsoleColor.Red);
                direction = direction.ToLower();
                direction = ValidateDirectionWithIndex(index, direction);
            }
            return direction;
        }
        private string ValidateSameNode(DLinked candiesLinkedList, int index,string direction)
        {
            direction = direction.ToLower();
            Node currentNode = GetNodeWithIndex(candiesLinkedList, index);
            //string validDirection = direction;
            switch (direction)
            {
                case "up":
                    Node upNode = currentNode.GetUpNode(currentNode);
                    if (upNode != null && currentNode.data.Score == upNode.data.Score)
                    {
                        string userInput = ReadInput("Wrong Movement up!!, Same Candies please select another Direction",ConsoleColor.Red);
                        direction = ValidateDirection(index, userInput, candiesLinkedList);
                    }
                    break;
                case "down":
                    Node downNode = currentNode.GetDownNode(currentNode);
                    if (downNode != null && currentNode.data.Score == downNode.data.Score)
                    {
                        string userInput = ReadInput("Wrong Movement down!!, Same Candies please select another Direction",ConsoleColor.Red);
                        direction = ValidateDirection(index, userInput, candiesLinkedList);
                    }
                    break;
                case "right":
                    if (currentNode.right != null && currentNode.data.Score == currentNode.right.data.Score)
                    {
                        string userInput = ReadInput("Wrong Movement right!!, Same Candies please select another Direction", ConsoleColor.Red);
                        direction = ValidateDirection(index, userInput, candiesLinkedList);
                    }
                    break;
                case "left":
                    if (currentNode.left != null &&  currentNode.data.Score == currentNode.left.data.Score)
                    {
                        string userInput = ReadInput("Wrong Movement left!!, Same Candies please select another Direction", ConsoleColor.Red);
                        direction = ValidateDirection(index, userInput, candiesLinkedList);
                    }
                    break;
                default:
                    break;
            }
            return direction;
        }

        private string ReadInput(string message , ConsoleColor messageColor)
        {
            string userInput = "";
            DisplayMessage(message, messageColor);
            userInput = Console.ReadLine();
            return userInput;
        }
        private void DisplayMessage(string message, ConsoleColor messageColor)
        {
            Console.ForegroundColor = messageColor;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
          
        }
        public Node GetNodeWithIndex(DLinked CandiesGrid, int index)
        {
            var currentNode = CandiesGrid.head;
            for(int i= 1 ; i<= index ; i++)
            {
                currentNode = currentNode.right;
            }
            return currentNode;
        }
        public void SwapCurrentNodeRight(Node currentNode)
        {
            //save old state in 2 variables
            var myRightNode = currentNode.right;
            var oldCurrentNode = currentNode;
            //compare
            if (myRightNode != null)
            { 
                currentNode.right = currentNode;
                //case all column
                bool currentMatchUp = MatchNodeWith2Right(currentNode);
                bool currentMatchDown = MatchNodeWith2Down(currentNode);
                bool currentMatchRight = MatchNodeWith2Right(currentNode);

                if(currentMatchUp  == true && currentMatchDown == true)
                {
                    //TO DO : crush current and the whole column , calculate the score 
                    //Generate random from the column and recheck the column if not the same
                }
                if(currentMatchUp)
                {
                    //TO DO : crush current with 2 ups and generate random , calculate the score 
                }
                if (currentMatchDown)
                {
                    //TO DO : crush current with 2 downs and generate random , calculate the score 
                }
                if (currentMatchRight)
                {
                    //TO DO : crush current with 2 rights and generate random , calculate the score 
                }
                //TO DO: if it match 1 up , 1 down 
                //TO DO: if it match 1 up , 1 down and 2 right
                //TO DO: if it matches 1 up and 2 down "crush 4" 
                //TO DO: if it matches 2 up and 1 down "crush 4" 
                



                //else if not equal return back all positions and don't crush anyting then display message that you cannot move
                currentNode = oldCurrentNode;
                currentNode.right = myRightNode;
            }

        }

        //Match Vertical Methods
        private bool MatchOneUpOneDown(Node currentNode, Node NodeToSwap)
        {
            if (NodeToSwap == null)
                return false;
            Node NodeToSwapUp = NodeToSwap.GetUpNode(NodeToSwap);
            Node NodeToSwapDown = NodeToSwap.GetDownNode(NodeToSwap);
            if (NodeToSwapUp != null && NodeToSwapDown != null &&
                currentNode.data.Score == NodeToSwapUp.data.Score && currentNode.data.Score == NodeToSwapDown.data.Score)
                return true;
            return false;
        }
        private bool MatchTwoUp(Node currentNode, Node NodeToSwap)
        {
            if (NodeToSwap == null) return false;
            Node NodeToSwapUp = NodeToSwap.GetUpNode(NodeToSwap);

            if (NodeToSwapUp == null) return false;
            Node NodeToSwapUpUp = NodeToSwapUp.GetUpNode(NodeToSwapUp);

            if (NodeToSwapUp != null && NodeToSwapUpUp != null &&
                currentNode.data.Score == NodeToSwapUp.data.Score && currentNode.data.Score == NodeToSwapUpUp.data.Score)
                return true;
            return false;
        }
        private bool MatchTwoDown(Node currentNode, Node NodeToSwap)
        {
            if (NodeToSwap == null) return false;

            Node NodeToSwapDown = NodeToSwap.GetDownNode(NodeToSwap);
            if (NodeToSwapDown == null) return false;

            Node NodeToSwapDownDown = NodeToSwapDown.GetDownNode(NodeToSwapDown);
            if (NodeToSwapDown != null && NodeToSwapDownDown != null &&
                currentNode.data.Score == NodeToSwapDown.data.Score && currentNode.data.Score == NodeToSwapDownDown.data.Score)
                return true;
            return false;
        }
        private bool MatchTwoUpOneDown(Node currentNode, Node NodeToSwap)
        {
            if (NodeToSwap == null) return false;

            bool match2Up = MatchTwoUp(currentNode, NodeToSwap);
            Node NodeToSwapDown = NodeToSwap.GetDownNode(NodeToSwap);
            if ( NodeToSwapDown != null && match2Up && NodeToSwapDown.data.Score == currentNode.data.Score)
                return true;
            return false;
        }
        private bool MatchTwoDownOneUp(Node currentNode, Node NodeToSwap)
        {
            bool match2Down= MatchTwoDown(currentNode, NodeToSwap);
            Node NodeToSwapUp = NodeToSwap.GetUpNode(NodeToSwap);
            if (NodeToSwapUp != null && match2Down && NodeToSwapUp.data.Score == currentNode.data.Score)
                return true;
            return false;
        }
        private bool MatchTwoUpTwoDown(Node currentNode, Node NodeToSwap)
        {
            bool match2Up = MatchTwoUp(currentNode, NodeToSwap);
            bool match2Down = MatchTwoDown(currentNode, NodeToSwap);
            if (match2Up && match2Down)
                return true;
            return false;
        }

        //Match horizontal Methods 
        private bool MatchOneRightOneLeft(Node currentNode, Node NodeToSwap)
        {
            if (NodeToSwap != null && NodeToSwap.right != null && NodeToSwap.left != null &&
                currentNode.data.Score == NodeToSwap.right.data.Score && currentNode.data.Score == NodeToSwap.left.data.Score)
                return true;
            return false;
        }
        private bool MatchTwoRight(Node currentNode, Node NodeToSwap)
        {
            //Node myRightNode = currentNode.right;
            //if (myRightNode != null && myRightNode.right != null && myRightNode.right.right != null &&
            //    currentNode.data.Score == myRightNode.right.data.Score && currentNode.data.Score == myRightNode.right.right.data.Score)
            //    return true;
            if (NodeToSwap != null && NodeToSwap.right != null && NodeToSwap.right.right != null &&
                currentNode.data.Score == NodeToSwap.right.data.Score && currentNode.data.Score == NodeToSwap.right.right.data.Score)
                return true;
            return false;
        }
        private bool MatchTwoLeft(Node currentNode, Node NodeToSwap)
        {
            //Node myLeftNode = currentNode.left;
            //if (myLeftNode != null && myLeftNode.left != null && myLeftNode.left.left != null &&
            //    currentNode.data.Score == myLeftNode.left.data.Score && currentNode.data.Score == myLeftNode.left.left.data.Score)
            //    return true; 

            if (NodeToSwap != null && NodeToSwap.left != null && NodeToSwap.left.left != null &&
                currentNode.data.Score == NodeToSwap.left.data.Score && currentNode.data.Score == NodeToSwap.left.left.data.Score)
                return true; 
            return false;
        }
        private bool MatchTwoRightOneLeft(Node currentNode, Node NodeToSwap)
        {
            bool match2right = MatchTwoRight(currentNode, NodeToSwap);
            if (NodeToSwap.left !=null && match2right && currentNode.data.Score == NodeToSwap.left.data.Score)
                return true;
            return false;
        }
        private bool MatchTwoLeftOneRight(Node currentNode, Node NodeToSwap)
        {
            bool match2left = MatchTwoLeft(currentNode, NodeToSwap);
            if (NodeToSwap != null &&  NodeToSwap.right != null && match2left && currentNode.data.Score == NodeToSwap.right.data.Score)
                return true;
            return false;
        }
        private bool MatchTwoRightTwoLeft(Node currentNode, Node NodeToSwap)
        {
            bool match2right = MatchTwoRight(currentNode, NodeToSwap);
            bool match2left = MatchTwoLeft(currentNode, NodeToSwap);
            if (match2right && match2left)
                return true;
            return false;
        }

       
        //Match "L shape" Methods "Vertical & Horzontal"
      // match 2up with 2right || match 2up with 2left
      private bool MatchUpHorizotal(Node currentNode, Node NodeToSwap,String direction )
        {
            direction = direction.ToLower();
            bool match2right = MatchTwoRight(currentNode, NodeToSwap);
            bool match2left = MatchTwoLeft(currentNode, NodeToSwap);
            bool match2up= MatchTwoUp(currentNode, NodeToSwap);

              
            if (direction == "right"||direction=="up")
              {
                  if (match2up && match2right)
                      return true;
              }
            else if (direction=="left"||direction=="up")
            {
                if (match2up && match2left)
                    return true;
            }
            return false;
        }
      // match 2down with 2right || match 2down with 2left
      private bool MatchDownHorizontal(Node currentNode, Node NodeToSwap,String direction)
        {
            direction = direction.ToLower();
          bool match2right = MatchTwoRight(currentNode, NodeToSwap);
          bool match2left = MatchTwoLeft(currentNode, NodeToSwap);
          bool match2down= MatchTwoDown(currentNode, NodeToSwap);
              
            if (direction == "right"||direction=="down")
              {
                  if (match2down&& match2right)
                      return true;
              }
            else if (direction=="left"||direction=="down")
            {
                if (match2down && match2left)
                    return true;
            }
            return false;
  
        }
    }
}

