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

        if (item == "+")
        {
            outputtokens.Add(new Token() { Type = TypeofToken.plus});
        }
        if (item == "-")
        {
            outputtokens.Add(new Token() { Type = TypeofToken.minus});
        }
        if (item == "*")
        {
            outputtokens.Add(new Token() { Type = TypeofToken.multi});
        }
        if (item == "/")
        {
            outputtokens.Add(new Token() { Type = TypeofToken.divid});
        }
        if (semi)
        {
            outputtokens.Add(new Token() { Type = TypeofToken.semicol });
        }
    }
    return outputtokens;
}

static ASTNode MKAST(List<Token> tokens)
{
    return null;
}


enum TypeofToken { _return, int_val, semicol, plus, minus, multi, divid}
class Token
{
    public TypeofToken Type;
    public string Value;
}

enum OperationType {O_Add,O_Sub,O_Mul, O_Div}
class ASTNode
{
    public OperationType op;
    public ASTNode Left;
    public ASTNode Right;
    public string int_val;
    public ASTNode(OperationType op, ASTNode Left, ASTNode Right, string int_val)
    {
        this.op = op;
        this.Left = Left;
        this.Right = Right;
        this.int_val = int_val;
    }
    static OperationType TookentoASTOp(Token input)
    {
        switch (input.Type)
        {
            case TypeofToken.plus: return OperationType.O_Add; 
            case TypeofToken.minus: return OperationType.O_Add; 
            case TypeofToken.multi: return OperationType.O_Add; 
            case TypeofToken.divid: return OperationType.O_Add; 
            default: throw new Exception("No valid Operation");
        }
    }
}





