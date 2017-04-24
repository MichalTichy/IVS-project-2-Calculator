using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Calculator.Graph;
using Math.ExpressionTreeBuilder;
using Math.Tokenizer;

namespace Calculator.ViewModels
{
   /// <summary>
   /// ViewModel layer for MainPage
   /// </summary>
   public class MainPageViewModel : INotifyPropertyChanged
    {

        private Tree.TreeContainer tre;
        /// <summary>
        /// Constructor for MainPage
        /// </summary>
        /// <param name="tree"></param>
        public MainPageViewModel(Tree.TreeContainer tree)
        {
            
            _tokenizer = new Tokenizer();

            tre = tree;
            //lst = tk.GetPossibleNextMathOperators(ExpressionPartTypes.Number);
            extree = new ExpressionTreeBuilder<Tokenizer>((Tokenizer)_tokenizer);
            Parse(false);
            OutputColor.Color = Colors.Black;

        }
        private MathOperatorDescription _selectedItem;
        private readonly ITokenizer _tokenizer;
        private ICollection<string> _collectionWithExpressionPartTypes;
        private IExpressionTreeBuilder extree;
        private ICollection<MathOperatorDescription> _lst;
        private Math.Nodes.INode _node;
        private string _inputValue = "";
        private string _result = "";
        private bool _isParsable;
        private ICollection<(string, MathOperatorDescription)> _tokenCollection;
        private int _selection;
        private Windows.UI.Xaml.Media.SolidColorBrush _outputColor = new Windows.UI.Xaml.Media.SolidColorBrush();
        private int _oldsel;
        private List<TreeConnection> _treeConnection;
        /// <summary>
        /// Flag signalizing on SelectedItemLostFocus
        /// </summary>
        public bool LostBySelection;

       

        #region Bindings
        /// <summary>
        /// Binding for Textbox text
        /// </summary>
        public string Values
        {
            get => _inputValue; 
            set
            {
                _inputValue = value;
                Parse(true);
                OnPropertyChanged("Values");
            }
        }

        /// <summary>
        /// Binding for ListView
        /// </summary>
        public ICollection<MathOperatorDescription> List
        {
            get => _lst;
            set
            {
                _lst = value;
                OnPropertyChanged("Lst");
            }
        }

        /// <summary>
        /// Binding for TextBlock
        /// </summary>
        public string Result
        {
            get => _result;
            set {
                    _result = value;
                    OnPropertyChanged("Result");
                }
        }

        /// <summary>
        /// Binding for Currently sellected MathOpertorDescription from ListView
        /// </summary>
        public MathOperatorDescription SelectedItem
        { get => _selectedItem;
          set
          {
                _selectedItem = value;
                if (_selectedItem != null)
                {
                    int i = _selectedItem.TextRepresentation.Length;
                    string newValues = _inputValue.Insert(_selection, _selectedItem.TextRepresentation);
                    Selection +=i;
                    Values = newValues;
                    //Values += $" {SelectedItem.TextRepresentation}";
                }
                     OnPropertyChanged("SelectedItem");
          }
        }

        /// <summary>
        /// Possition of Caret
        /// </summary>
        public int Selection
        {
            get => _selection;
            set
            {
                _oldsel = _selection;
                if ((_oldsel == 0 && value == 0) || value != 0)
                {
                    _selection = value;
                }
                OnPropertyChanged("Selection");
            }
        }

        /// <summary>
        /// Color for Input frame
        /// </summary>
        public Windows.UI.Xaml.Media.SolidColorBrush OutputColor
        {
            get => _outputColor;
            set
            {
                _outputColor = value;
                OnPropertyChanged("OutputColor");
            }
        }

        /// <summary>
        /// Flag representing if is given expression parsable
        /// </summary>
        public bool IsParsable
        {
            get => _isParsable;
            set
            {
                _isParsable = value;
                OnPropertyChanged("IsParsable");
            }
        }

        /// <summary>
        /// List of Nodes to be drawn on canvas
        /// </summary>
        public List<TreeConnection> TreeConection
        {
            get => _treeConnection;
            set
            {
                _treeConnection = value;
                OnPropertyChanged("Conn");
            }
        }

        /// <summary>
        /// Event controling consistency between ViewModel and View
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Event controling consistency between ViewModel and View
        /// </summary>
        /// <param name="propertyName"></param>
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region TreeNodeHandling
        Math.Nodes.INode getLeftNode(Math.Nodes.INode n)
        {
            if (!(n is Math.Nodes.Values.NumberNode))
            {
                while (!(n is Math.Nodes.Values.NumberNode) && n != null)
                {
                    if (n is Math.Nodes.Functions.Binary.IBinaryOperationNode)
                    {
                        n = ((Math.Nodes.Functions.Binary.IBinaryOperationNode)n).LeftNode;
                    }
                    else
                    {
                        n = ((Math.Nodes.Functions.Unary.IUnaryOperationNode)n).ChildNode;
                    }
                    
                
                }
            }
            
            return n;
        }

        Math.Nodes.INode GetFirst(Math.Nodes.INode n)
        {
            if (n == null) return null;
            else return getLeftNode(n);
        }

        Math.Nodes.INode GetNext(Math.Nodes.INode n)
        {
           
                if (!(n is Math.Nodes.Values.NumberNode) && (!(n is Math.Nodes.Functions.Unary.IUnaryOperationNode)) && ((Math.Nodes.Functions.Binary.IBinaryOperationNode)n).RightNode != null)
                {
                    return getLeftNode(((Math.Nodes.Functions.Binary.IBinaryOperationNode)n).RightNode);
                }
                else 
                {
                    while ((n.Parent != null) && (n.Parent is Math.Nodes.Functions.Binary.IBinaryOperationNode) && n == ((Math.Nodes.Functions.Binary.IBinaryOperationNode) n.Parent).RightNode)
                    {
                        n = n.Parent;
                    }
                }
                    return n.Parent;
                
        }

        string GetNodeTextRepre(Math.Nodes.INode n)
        {
            IReadOnlyCollection<MathOperatorDescription> mod = _tokenizer.RegisteredOperators;
            foreach (MathOperatorDescription ss in mod)
            {
                if (ss.NodeType == n.GetType())
                {
                    return ss.TextRepresentation;
                }
                
            }
            if (n is Math.Nodes.Values.NumberNode)
            {
                return ((Math.Nodes.Values.NumberNode)n).Evaluate().ToString(CultureInfo.InvariantCulture);
            }
            else
            {
                return "x";
            }
        }
       
        void TreeParser(Math.Nodes.INode n)
        {
            tre.Clear();
            tre.Root = n.GetHashCode().ToString();
            tre.AddNode(GetNodeTextRepre(n), n.GetHashCode().ToString(), (string)null);

            n = GetFirst(n);

            while (n!=null)
            {
               
                if (n.Parent != null)
                {
                    tre.AddNode(GetNodeTextRepre(n), n.GetHashCode().ToString(), (n.Parent).GetHashCode().ToString());
                }
                n = GetNext(n);
            }
        }

#endregion
        private void Parse(bool edit)
        {
            if (_inputValue != "")
            {


                string valueToParse = _inputValue;
                if (edit)
                {
                    valueToParse = _inputValue.Substring(0, _selection);
                }

                try
                {
                    IsParsable = true;

                    _node = extree.ParseExpression(valueToParse);
                    TreeParser(_node);
                    _collectionWithExpressionPartTypes = _tokenizer.SplitExpressionToTokens(valueToParse);
                    _tokenCollection = _tokenizer.AssignOperatorDescriptionToTokens(_collectionWithExpressionPartTypes);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    IsParsable = false;
                }
                finally
                {
                    if (!IsParsable)
                    {
                        List = null;
                    }
                    else
                    {
                        List = _tokenizer.GetPossibleNextMathOperators(Tokenizer.GetPrecedingExpressionPartType(_tokenCollection.Last()));
                        LostBySelection = false;

                    }
                    if (!edit)
                    {
                        OutputColor.Color = (!_isParsable) ? Colors.Red : Colors.Black;
                    }
                }


            }
            else
            {
                Result = "";
                tre.Clear();
            }
        }

       
       
        /// <summary>
        /// Event firing after every newly selected matemathical function
        /// </summary>
        public void SelectionChanged()
        {
            Debug.WriteLine($"xxxx {_selection} .. {Values.Length}");
            Parse(_selection != Values.Length);
        }


        /// <summary>
        /// Event firing after losing focus of textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void InputLostFocus(object sender, RoutedEventArgs e)
        {
            Selection = _oldsel;
        }

        /// <summary>
        /// Event firing after pressed key, has purpose for pressing Enter key
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void KeyPressed(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                Eval();
            }
           

          
        }
        /// <summary>
        /// Evaluating expression given by user
        /// </summary>
        public void Eval()
        {
            if (_isParsable)
            {
                try
                {
                    Result = _node.Evaluate().ToString(CultureInfo.CurrentCulture);
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

            await errDialog.ShowAsync();
        }

    }

  


}
