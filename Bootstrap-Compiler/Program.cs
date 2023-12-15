

if (args.Length != 0 && args[0] != " " && args[0][args[0].Length-4] == 'c'&& args[0][args[0].Length-3] == 'b'&& args[0][args[0].Length-2] == 'l'&& args[0][args[0].Length-2] == 't') //Check whether an input file is there
{                                                                                                                                                                                   //Our Language is called "c blurred" so our extension is .cblt
    string inputcontext = File.ReadAllText(@args[0]/*@"../../../input/input.cblt"*/); //get our code
    List<Token> tokens = Tokenize(inputcontext); //Tokenize it
    AST tree = new AST();
    tree.Parse(tokens);
    Console.WriteLine();
}
else
{
    Console.WriteLine("Missing File!");
    Console.WriteLine("Please Retry");
}
return;

#region Token
static List<Token> Tokenize(string inputcontext) //Methode to define the tokens in our input-code (Lexing)
{
    List<Token> outputtokens = new List<Token>();
    string[] words = inputcontext.Split(' ', '\n','\t'); //Split the code by every non usable character
    foreach (var item in words) //Identify the token of the word
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
            outputtokens.Add(new Token() { Type = TypeofToken._exit });
        }
        else if (isparsed)
        {
            outputtokens.Add(new Token() { Type = TypeofToken.int_val, Value = item });
        }

        else if (item == "+")
        {
            outputtokens.Add(new Token() { Type = TypeofToken.plus});
        }
        else if (item == "-")
        {
            outputtokens.Add(new Token() { Type = TypeofToken.minus});
        }
        else if (item == "*")
        {
            outputtokens.Add(new Token() { Type = TypeofToken.multi});
        }
        else if (item == "/")
        {
            outputtokens.Add(new Token() { Type = TypeofToken.divid});
        }
        else
        {
            throw new Exception("No valid Tokenization available");
        }
        if (semi)
        {
            outputtokens.Add(new Token() { Type = TypeofToken.semicol });
        }
        
    }
    return outputtokens;
}

enum TypeofToken { _exit, int_val, semicol, plus, minus, multi, divid}
class Token
{
    public TypeofToken Type;
    public string Value;
}



#endregion

#region Parser/AST

enum OperationType {O_Add,O_Sub,O_Mul, O_Div, O_Exit,O_Int_Val}

class AST
{
    public ASTNode Root { get; private set; }
    public void Parse(List<Token> tokens)
    {
        List<ASTNode> nodes = new List<ASTNode>();
        foreach (var item in tokens)
        {
            switch (item.Type)
            {
                case TypeofToken._exit: nodes.Add(new ASTNode(OperationType.O_Exit,null,null,null));break;
                case TypeofToken.plus: nodes.Add(new ASTNode(OperationType.O_Add,null,null,null));break;
                case TypeofToken.minus: nodes.Add(new ASTNode(OperationType.O_Sub,null,null,null));break;
                case TypeofToken.divid: nodes.Add(new ASTNode(OperationType.O_Div,null,null,null));break;
                case TypeofToken.multi: nodes.Add(new ASTNode(OperationType.O_Mul,null,null,null));break;
                case TypeofToken.int_val: nodes.Add(new ASTNode(OperationType.O_Int_Val,null,null,item.Value));break;
                default: throw new Exception("No valid Parsing");
            }
        }

        ASTNode r;
        if ((r = FindBinaryExpr(nodes, 0)) != null && nodes.Count > nodes.IndexOf(r) )
        {
            Root = r;
            
        }
        else if(nodes.Count >= 1)
        {
            Root = nodes[0];
        }
        else{}
        
        
    }
    

    ASTNode FindBinaryExpr(List<ASTNode> list, int index)
    {
        if (index < list.Count)
        {
            ASTNode root = null;
            int foundedindex = index;
            for(int i = index; i<list.Count; i++)
            {
                if (list[i].op == OperationType.O_Add)
                {
                    root = list[i];
                    foundedindex = i;
                    i += list.Count;
                }
                else if (list[i].op == OperationType.O_Div)
                {
                    root = list[i];
                    foundedindex = i;
                    i += list.Count;
                }
                else if (list[i].op == OperationType.O_Mul)
                {
                    root = list[i];
                    foundedindex = i;
                    i += list.Count;
                }
                else if (list[i].op == OperationType.O_Sub)
                {
                    root = list[i];
                    foundedindex = i;
                    i += list.Count;
                }
            }
        
            if (list[foundedindex-1].op == OperationType.O_Int_Val)
            {
                root.Left = list[foundedindex-1];
                root.Right = FindBinaryExpr(list, foundedindex + 1);
                if (root.Right == null)
                {
                    if (foundedindex+1<list.Count())
                    {
                        root.Right = list[foundedindex + 1];
                    }
                }
            }

            return root;
        }
        return null;
    }
}

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
    
}

#endregion