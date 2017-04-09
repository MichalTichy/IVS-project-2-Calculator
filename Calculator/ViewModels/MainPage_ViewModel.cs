using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Math;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Calculator
{
   public class MainPage_ViewModel : INotifyPropertyChanged
    {

        public MainPage_ViewModel()
        {
            tk = new Tokenizer();
            lst = tk.RegisteredOperators;
            node = new Math.Nodes.Values.NumberNode("0");
            Result = node.Evaluate().ToString();
        }
        private Math.ITokenizer tk;
        private ICollection<MathOperatorDescription> test;
        private IReadOnlyCollection<MathOperatorDescription> lst;
        private Math.Nodes.INode node;
        private string val = "0";
        private string result;


        public string Values
        {
            get { return val; }
            set { val = value;
                OnPropertyChanged("Values");
                something();
                }
        }

        public IReadOnlyCollection<MathOperatorDescription> Lst
        {
            get { return lst; }
            private set { }
        }

        public string Result
        {
            get { return result; }
            set { result = value;
                OnPropertyChanged("Result");
                        }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

    

    public void something()
        {
            Result = Values;
            Debug.WriteLine($"result: {result}, Result: {Result}, value: {val}, Value: {Values}");
        }

        
    }
}
