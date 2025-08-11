// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

int[] arr = [];
Console.WriteLine(secondLargerNumber(arr));

static int secondLargerNumber(int[] arr)
{
    int temp1=arr[0];
    int temp2=arr[0];

    for(int i=1; i<arr.Length; i++)
    {
        int temp3 = arr[i];

        if(temp1 < temp3)
            temp1 = temp3;
        else if(temp2 < temp3)
            temp2 = temp3;
    }
    return temp1>temp2 ? temp2 : temp1;
}
