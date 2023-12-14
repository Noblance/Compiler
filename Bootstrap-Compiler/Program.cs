if (args.Length != 0 && args[0] != " ") //Check whether an input file is there
{
    string inputcontext = File.ReadAllText(@args[0]); //get our code
    List<Token> tokens = Tokenize(inputcontext);
}
else
{
    Console.WriteLine("Missing File!");
    Console.WriteLine("Please Retry");
}
return;



static List<Token> Tokenize(string inputcontext) //Methode to define the tokens in our input-code (Lexing)
{
    List<Token> outputtokens = new List<Token>();
    string[] words = inputcontext.Split(' ');
    foreach (var item in words)
    {
        bool semi = false;
        int temp;
        bool isparsed = int.TryParse(item, out temp);
        if (item.Contains(';'))
        {
            semi = true;
            item.Replace(';', ' ');
        }
        if (item == "return")
        {
            outputtokens.Add(new Token() { Type = TypeofToken._return });
        }
        if (isparsed)
        {
            outputtokens.Add(new Token() { Type = TypeofToken.int_val, Value = item });
        }

        if (semi)
        {
            outputtokens.Add(new Token() { Type = TypeofToken.semicol });
        }
    }
    return outputtokens;
}


enum TypeofToken { _return, int_val, semicol}
class Token
{
    public TypeofToken Type;
    public string Value;
}    



