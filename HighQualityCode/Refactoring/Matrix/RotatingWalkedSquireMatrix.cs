namespace Matrix
{
    using System;
    using System.Text;

    public class RotatingWalkedSquireMatrix
    {
        private const int DirectionsCount = 8;   

        private int[,] matrix;

        private int width;

        private int[] directionX = { 1, 1, 1, 0, -1, -1, -1, 0 };

        private int[] directionY = { 1, 0, -1, -1, -1, 0, 1, 1 };

        public RotatingWalkedSquireMatrix(int width)
        {
            this.Width = width;
            this.matrix = new int[width, width];
            this.FillRotatingWalkInMatrix();
        }

        public int Width
        {
            get
            {
                return this.width;
            }

            private set
            {
                this.width = value;
            }
        }

        public int this[int row, int column]
        {
            get
            {
                if (row < 0 || row >= this.Width)
                {
                    throw new IndexOutOfRangeException("Defined row is out of range.");
                }

                if (column < 0 || column >= this.Width)
                {
                    throw new IndexOutOfRangeException("Defined column is out of range.");
                }

                return this.matrix[row, column];
            }           
        }

        public int this[Position position]
        {
            get
            {
                if (position.X < 0 || position.X >= this.Width)
                {
                    throw new IndexOutOfRangeException("Defined row is out of range.");
                }

                if (position.Y < 0 || position.Y >= this.Width)
                {
                    throw new IndexOutOfRangeException("Defined column is out of range.");
                }

                return this.matrix[position.X, position.Y];
            }

            private set
            {
                if (position.X < 0 || position.X >= this.Width)
                {
                    throw new IndexOutOfRangeException("Defined row is out of range.");
                }

                if (position.Y < 0 || position.Y >= this.Width)
                {
                    throw new IndexOutOfRangeException("Defined column is out of range.");
                }

                this.matrix[position.X, position.Y] = value;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int x = 0; x < this.Width; x++)
            {
                for (int y = 0; y < this.Width; y++)
                {
                    Position currentPosition = new Position(x, y);
                    sb.AppendFormat("{0,3}  ", this[currentPosition]);
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }

        private void FillRotatingWalkInMatrix()
        {   
            int nextNumber = 1;
            Direction direction = Direction.DownRight;
            Position currentPosition = new Position(0, 0);

            while (true)
            { 
                this[currentPosition] = nextNumber;
                nextNumber++;

                Position nextFreeNeighbourPosition = this.GetNextFreeNeighbourPosition(currentPosition, ref direction);
                if (nextFreeNeighbourPosition == null) 
                {                   
                    direction = Direction.DownRight;
                    Position smalestEmptyCellPosition = this.FindSmalestEmptyCell();
                    if (smalestEmptyCellPosition == null)                 
                    {
                        ///no empty cell is left in the matrix
                        break;
                    }

                    this[smalestEmptyCellPosition] = nextNumber;
                    currentPosition = smalestEmptyCellPosition;
                } 
                else
                { 
                    currentPosition = nextFreeNeighbourPosition;
                }                
            }
            
            return;            
        }

        private Position GetNeibhourPosition(Position position, Direction direction)
        {
            return new Position(
                position.X + this.directionX[(int)direction],
                position.Y + this.directionY[(int)direction]);
        }

        private Direction GetNextDirection(Direction direction)
        {
            if (direction == Direction.Right)
            {
                return Direction.DownRight;
            }

            Direction nextDirection = direction + 1;
            return nextDirection;
        }

        private Position GetNextFreeNeighbourPosition(Position startPosition, ref Direction direction)
        {            
            Direction currentDirection = direction;

            for (int i = 0; i < DirectionsCount; i++)
            {                
                Position nextPosition = this.GetNeibhourPosition(startPosition, currentDirection);

                if ((nextPosition.X >= this.Width || nextPosition.X < 0) ||
                    (nextPosition.Y >= this.Width || nextPosition.Y < 0))
                {
                    currentDirection = this.GetNextDirection(currentDirection);
                    continue;
                }               

                if (this[nextPosition] == 0)
                {               
                    direction = currentDirection;
                    return nextPosition;
                }

                currentDirection = this.GetNextDirection(currentDirection);               
            }

            return null;
        }

        private Position FindSmalestEmptyCell()
        {
            ///An empty cell at the smallest possible row and as close as possible to the start of this row
            
            for (int x = 0; x < this.Width; x++)
            {
                for (int y = 0; y < this.Width; y++)
                {
                    Position currentPosition = new Position(x, y);
                    if (this[currentPosition] == 0) 
                    {
                        return currentPosition; 
                    }
                }
            }

            return null;
        }              
    }
}
