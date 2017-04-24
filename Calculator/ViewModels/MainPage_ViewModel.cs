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
using System.Collections.ObjectModel;
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
            dt.Interval = new TimeSpan(0, 0, 0, 0, 80);
            dt.Tick += Dt_Tick;
            dt.Start();
            FillFunctionLists();
        }

        private void FillFunctionLists()
        {
            IReadOnlyCollection<MathOperatorDescription> tmp = _tokenizer.RegisteredOperators;
            _bLst = new Collection<MathOperatorDescription>();
            _gLst = new Collection<MathOperatorDescription>();
            _sLst = new Collection<MathOperatorDescription>();
            string lastItem = "";
            foreach (var ss in tmp)
            {
                
                switch (ss.OperationCategory)
                {
                    case OperationCategory.Basic:
                    {
                        if (lastItem != ss.TextRepresentation)
                        {
                            _bLst.Add(ss);
                        }
                    }
                        break;
                    case OperationCategory.Goniometric:
                    {
                        if (lastItem != ss.TextRepresentation)
                        {
                            _gLst.Add(ss);
                        }
                        
                    }
                        break;
                    case OperationCategory.Special:
                    {
                        if (lastItem != ss.TextRepresentation)
                        {
                            _sLst.Add(ss);
                        }
                        
                    }
                        break;
                }
                lastItem = ss.TextRepresentation;
            }
            OnPropertyChanged("BasicList");
            OnPropertyChanged("GoniometricList");
            OnPropertyChanged("SpecialList");
        }
        private void Dt_Tick(object sender, object e)
        {
            foreach (var ss in tre.Children)
            {
                if (ss is Windows.UI.Xaml.Shapes.Line)
                {
                    tre.Children.Remove(ss);
                }
            }
            if (_node != null)
            {
                TreeParser(_node);
            }
            
            tre.ReDraw();
        }

        private MathOperatorDescription _selectedItem;
        private readonly ITokenizer _tokenizer;
        private ICollection<string> _collectionWithExpressionPartTypes;
        private IExpressionTreeBuilder extree;
        private ICollection<MathOperatorDescription> _rLst;
        private ICollection<MathOperatorDescription> _bLst;
        private ICollection<MathOperatorDescription> _gLst;
        private ICollection<MathOperatorDescription> _sLst;
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
        public ICollection<MathOperatorDescription> RecomendedList
        {
            get => _rLst;
            set
            {
                _rLst = value;
                OnPropertyChanged("RecomendedList");
            }
        }

        public ICollection<MathOperatorDescription> BasicList
        {
            get => _bLst;
            set
            {
                _bLst = value;
                OnPropertyChanged("BasicList");
            }
        }

        public ICollection<MathOperatorDescription> GoniometricList
        {
            get => _gLst;
            set
            {
                _gLst = value;
                OnPropertyChanged("GoniometricList");
            }
        }

        public ICollection<MathOperatorDescription> SpecialList
        {
            get => _sLst;
            set
            {
                _sLst = value;
                OnPropertyChanged("SpecialList");
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
                OnPropertyChanged("TreeConnection");
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
                        RecomendedList = null;
                    }
                    else
                    {
                        RecomendedList = _tokenizer.GetPossibleNextMathOperators(Tokenizer.GetPrecedingExpressionPartType(_tokenCollection.Last()));
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
                    Result = _node.Evaluate().ToString();
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

        private DispatcherTimer dt = new DispatcherTimer();
        

    }

  


}
