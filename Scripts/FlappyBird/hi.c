#include <stdio.h>
#include <stdlib.h>

int main()
{
    int Length = 5;
    int pipe;
    
    int Zero = 0, One = 0, Two = 0;
    for (int i = 0; i < 100000; i++)
    {
        int random = rand() % (Length * 10); // Select from range [0, Length * 10 * 2 - 1]

        if (random < (Length * 10) * 50 / 100) // 50% probability
        {
            pipe = 0;
            Zero++;
        }
        else if (random < (Length * 10) * 80 / 100) // 30% probability (next 30%)
        {
            pipe = 1;
            One++;
        }
        else // Remaining 20%
        {
            pipe = 2;
            Two++;
        }
    }

    printf("Probability of Zero is : %f %%\n", (float)Zero/1000);
    printf("Probability of One is : %f %%\n", (float)One/1000);
    printf("Probability of Two is : %f %%\n", (float)Two/1000);

    return 0;
}
