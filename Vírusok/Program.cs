Random r = new Random();

int n = 20;
int m = 20;
// main container
string[,] unmodifiedContainer = new string[n,m];
// generating main container
for (int i = 0; i < n; i++) for (int j = 0; j < m; j++) unmodifiedContainer[i,j] =  r.Next(100) < 20 ? "*" : "o" ;
// this container is the modified one
string[,] modifiedContainer = new string[n,n];


while (VirusCount(unmodifiedContainer)/(n*m) < 0.8)
{
        Array.Copy(unmodifiedContainer,modifiedContainer,unmodifiedContainer.Length);
    for (int i = 0; i < n; i++)
    {
        for (int j = 0; j < m; j++)
        {
            CheckAndModifySurrondings(i,j,unmodifiedContainer, modifiedContainer);
            System.Threading.Thread.Sleep(500); // 5,000 ms
            DisplayChanges(modifiedContainer,n,m,i,j);
        }
    } 
        Array.Copy(modifiedContainer,unmodifiedContainer,modifiedContainer.Length);
}



void CheckAndModifySurrondings(int cordN,int cordM,string[,] container,string[,] modContainer)
{
    // counter for the bordering viruses
    int counterOfAdjacentViruses = container[cordN,cordM] == "*" ? -1 : 0 ;
    // check if a virus exists on the current position
    // loop for the rows
    for (int i = cordN-1; i <= cordN+1; i++)
    {
        // loop for the columns
        for (int j = cordM-1; j <= cordM+1; j++)
        {
            // this adds to the counter if the specified location contains a virus
            if (i >= 0 && j >= 0 && i < container.GetLength(0) && j < container.GetLength(1) && container[i, j] == "*") counterOfAdjacentViruses++;
        }
    }
    // creates a virus on the specified locations
    if (counterOfAdjacentViruses > 1) modContainer[cordN, cordM] = "*";
    // removes a virus on the specified locations
    if (counterOfAdjacentViruses < 2) modContainer[cordN, cordM] = "o";
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

double VirusCount(string[,] array)
{
    double count = 0;
    for (int i = 0; i < array.GetLength(0); i++)
    {
        for (int j = 0; j < array.GetLength(1); j++)
        {
            if (array[i, j] == "*") count++;
        } 
    }
    return count;
}