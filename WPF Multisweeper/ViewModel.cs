//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Input;

//namespace Multisweeper
//{

//    public class ViewModelBase
//    {
//        public ViewModelBase()
//        {
//            _canExecute = true;
//        }
//        private ICommand _clickCommand;
//        public ICommand ClickCommand
//        {
//            get
//            {
//                return _clickCommand ?? (_clickCommand = new CommandHandler(() => MyAction(), _canExecute));
//            }
//        }
//        private bool _canExecute;
//        public void MyAction()
//        {

//        }
//    }
//    public class CommandHandler : ICommand
//    {
//        private Action _action;
//        private bool _canExecute;
//        public CommandHandler(Action action, bool canExecute)
//        {
//            _action = action;
//            _canExecute = canExecute;
//        }

//        public bool CanExecute(object parameter)
//        {
//            return _canExecute;
//        }

//        public event EventHandler CanExecuteChanged;

//        public void Execute(object parameter)
//        {
//            _action();
//        }




        

//    }
//}
