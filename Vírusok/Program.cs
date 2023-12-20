Random r = new Random();

int n = 20;
int m = 20;
// main container
// generating main container
string[,] patternContainer = new string[n,m];
for (int i = 0; i < n; i++) for (int j = 0; j < m; j++) patternContainer[i,j] =  r.Next(100) < 20 ? "*" : "o" ;
// this container is the modified one
string[,] mainContainer = new string[n,n];



for (int i = 0; i < n; i++)
{
    Array.Copy(patternContainer,mainContainer,mainContainer.Length);
    for (int j = 0; j < m; j++)
    {
        DisplayChanges(mainContainer,n,m,i,j);
         System.Threading.Thread.Sleep(500); // 5,000 ms
        CheckAndModifySurrondings(i,j,patternContainer, mainContainer);
    }
}


void CheckAndModifySurrondings(int cordN,int cordM,string[,] container,string[,] modifiedContainer)
{
    // counter for the bordering numbers
    int counterOfAdjacentNumbers = 0;
    // check if a virus exists on the current position
    bool virusExists = container[cordN, cordM] == "*" ? true : false;
    // loop for the rows
    for (int i = cordN-1; i < cordN+1; i++)
    {
        // loop for the columns
        for (int j = cordM-1; j < cordM+1; j++)
        {
            // this adds to the counter if the specified location contains a virus
            if (i >= 0 && j >= 0 && i < container.GetLength(0) && j < container.GetLength(1)  && container[i, j] == "*") counterOfAdjacentNumbers++;
        }
    }
    // creates a virus on the specified locations
    if (counterOfAdjacentNumbers > 1 && !virusExists) modifiedContainer[cordN, cordM] = "*";
    // removes a virus on the specified locations
    if (counterOfAdjacentNumbers < 2 && virusExists) modifiedContainer[cordN, cordM] = "o";
}

void DisplayChanges(string[,] container,int cordN,int cordM,int currentN,int currentM)
{
    Console.Clear();
    for (int i = 0; i < cordN ; i++)
    {
        for (int j = 0; j < cordM; j++)
        {
            if (container[i, j] == "*") Console.ForegroundColor = ConsoleColor.Red;
            else Console.ForegroundColor = ConsoleColor.Green;
            if (i == currentN && j == currentM) Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"{container[i,j]} ");
        }
        Console.WriteLine();
    }
}