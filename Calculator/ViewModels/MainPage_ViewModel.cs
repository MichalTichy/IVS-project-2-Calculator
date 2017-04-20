using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Math;
using Math.Nodes.Functions;
using Math.Nodes.Values;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml.Controls;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Windows.UI.Core;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml.Media;
using Windows.Foundation;
using GraphLayout;

namespace Calculator
{
   public class MainPage_ViewModel : INotifyPropertyChanged
    {


        private TreeContainer.TreeContainer tre;
        public MainPage_ViewModel(TreeContainer.TreeContainer tree)
        {
            tk = new Tokenizer();
            lst = tk.GetPossibleNextMathOperators(ExpressionPartTypes.Number);
            extree = new ExpressionTreeBuilder<Math.Tokenizer>((Tokenizer)tk);
            OutputColor.Color = Colors.Black;
            tre = tree;
        }
        private MathOperatorDescription selectedItem;
        private Math.ITokenizer tk;
        private ICollection<string> collectionWithExpressionPartTypes;
        private IExpressionTreeBuilder extree;
        private ICollection<MathOperatorDescription> lst;
        private Math.Nodes.INode node;
        private string val = "0";
        private string result = "";
        private bool isParsable =false;
        private ICollection<(string, MathOperatorDescription)> ts2;
        private int selection;
        private Windows.UI.Xaml.Media.SolidColorBrush outputColor = new Windows.UI.Xaml.Media.SolidColorBrush();
        public int oldsel;
        public string oldselected;
        private List<TreeConnection> conn;

    public string Values
        {
            get => val; 
            set
            {
                val = value;
                parse(true);
                OnPropertyChanged("Values");
            }
        }

        public ICollection<MathOperatorDescription> Lst
        {
            get => lst;
            set
            {
                lst = value;
                OnPropertyChanged("Lst");
            }
        }

        public string Result
        {
            get { return result; }
            set { result = value;
                OnPropertyChanged("Result");
                        }
        }

        public MathOperatorDescription SelectedItem
        { get => selectedItem;
          set
          {
                Debug.WriteLine("tets");
                    selectedItem = value;
                if (selectedItem != null)
                {
                    int i = selectedItem.TextRepresentation.Length;
                    Values = Values.Insert(selection, selectedItem.TextRepresentation);
                    Selection += i;
                    //Values += $" {SelectedItem.TextRepresentation}";
                }
                    
                    OnPropertyChanged("SelectedItem");
          }
        }

        public int Selection
        {
            get => selection;
            set
            {
                oldsel = selection;
                if (value != 0)
                {
                    selection = value;
                    OnPropertyChanged("Selection");
                }
                
            }
        }

        public Windows.UI.Xaml.Media.SolidColorBrush OutputColor
        {
            get => outputColor;
            set
            {
                outputColor = value;
                OnPropertyChanged("OutputColor");
            }
        }

        public bool IsParsable
        {
            get => isParsable;
            set
            {
                isParsable = value;
                OnPropertyChanged("IsParsable");
            }
        }

        public List<TreeConnection> Conn
        {
            get => Conn;
            set
            {
                Conn = value;
                OnPropertyChanged("Conn");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        Math.Nodes.INode getLeftNode(Math.Nodes.INode n)
        {
            if (!(n is Math.Nodes.Values.NumberNode))
            {
                while (!(n is Math.Nodes.Values.NumberNode) &&((Math.Nodes.Functions.Binary.IBinaryOperationNode)n).LeftNode != null)
                {
                  
                    
                        n = ((Math.Nodes.Functions.Binary.IBinaryOperationNode)n).LeftNode;
                
                }
            }
            
            return n;
        }

        Math.Nodes.INode getFirst(Math.Nodes.INode n)
        {
            if (n == null) return null;
            else return getLeftNode(n);
        }

        Math.Nodes.INode getNext(Math.Nodes.INode n)
        {
           
                if (!(n is Math.Nodes.Values.NumberNode) &&((Math.Nodes.Functions.Binary.IBinaryOperationNode)n).RightNode != null)
                {
                    return getLeftNode(((Math.Nodes.Functions.Binary.IBinaryOperationNode)n).RightNode);
                }
                else
                {
                    while (n.Parent != null && n == ((Math.Nodes.Functions.Binary.IBinaryOperationNode)n.Parent).RightNode)
                    {
                        n = n.Parent;
                    }
                    return n.Parent;
                }
        }

        void foo(Math.Nodes.INode n)
        {
            Dictionary<int, Math.Nodes.INode> dt = new Dictionary<int, Math.Nodes.INode>();

            string ss = "";
            n = getFirst(n);
            tre.Clear();
            tre.Root = n.GetHashCode().ToString();
            tre.AddNode(n, n.GetHashCode().ToString(),(string)null);
            while (n!=null)
            {
                Debug.WriteLine(n.ToString());
                if (n is Math.Nodes.Values.NumberNode)
                {
                    Debug.WriteLine($"Leaf {((Math.Nodes.Values.NumberNode)n).Evaluate().ToString()}");
                    ss += ((Math.Nodes.Values.NumberNode)n).Evaluate().ToString();
                }
                else
                {
                    Debug.WriteLine($"op {((Math.Nodes.Functions.Binary.IBinaryOperationNode)n).ToString()}");
                    ss += n.ToString();
                    
                }
                if (n.Parent != null)
                {
                    tre.AddNode(n, n.GetHashCode().ToString(), (n.Parent).GetHashCode().ToString());
                }
                n = getNext(n);
                
            }
            Debug.WriteLine(ss);
        }


        private void parse(bool edit)
        {
            string valueToParse = val;
            if (edit)
            {
                valueToParse = val.Substring(0, selection);
            }  

            try
            {
                IsParsable = true;
                
                node = extree.ParseExpression(valueToParse);
                

                collectionWithExpressionPartTypes = tk.SplitExpressionToTokens(valueToParse);
                ts2 = tk.AssignOperatorDescriptionToTokens(collectionWithExpressionPartTypes);
            }
            catch(Exception)
            {
                IsParsable = false;
            }
            finally
            {                
                if (!IsParsable)
                {
                    Lst = null;
                }
                else
                {
                    Lst = tk.GetPossibleNextMathOperators(Tokenizer.GetPrecedingExpressionPartType(ts2.Last<(string, MathOperatorDescription)>()));
                    foo(node);
                }
                if (!edit)
                {
                    if (!IsParsable)
                    {
                        OutputColor.Color = Colors.Red;
                    }
                    else
                    {
                        OutputColor.Color = Colors.Black;
                    }
                    
                }
            }

          

        }

       
       
        public void SelectionChanged()
        {
            Debug.WriteLine($"xxxx {selection} .. {Values.Length}");
            if (selection != Values.Length)
            {
                parse(true);
            }
            else parse(false);


        }

        public void KeyPressed(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                Eval();
            }
           

          
        }
        public void Eval()
        {
            int totalyNonUsefull;
            if (isParsable && (int.TryParse(collectionWithExpressionPartTypes.Last<string>(), out totalyNonUsefull)))
            {
                try
                {
                    Result = node.Evaluate().ToString();
                }
                catch (Exception ex)
                {


                    DisplayErrDialog(ex.Message);
                }
            }
        }

        private async void DisplayErrDialog(string cont)
        {
            
            ContentDialog errDialog = new ContentDialog
            {
                Title = "Chyba",
                Content = cont, PrimaryButtonText="ok"
            };

            ContentDialogResult result = await errDialog.ShowAsync();
            
        }

    }

  


}
