using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Math;
using System.Collections.ObjectModel;
using System.Diagnostics;
namespace Calculator
{
    class MainPage_ViewModel : INotifyPropertyChanged
    {

        public MainPage_ViewModel()
        {
            mathLib = new ExpressionTreeBuilder();      
            lst = mathLib.RegisteredOperators;
            node = new Math.Nodes.Values.NumberNode("0");
            Result = node.Evaluate().ToString();
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        private IReadOnlyCollection<MathOperatorDescription> lst;
        private Math.ExpressionTreeBuilder mathLib;
        private Math.Nodes.INode node;
        private string val = "";
        private string result;
        public string Values
        {
            get { return val; }
            set { val = value;
                OnPropertyChanged("TXB_Value");
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
            private set { result = value; }
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        
    }
}
