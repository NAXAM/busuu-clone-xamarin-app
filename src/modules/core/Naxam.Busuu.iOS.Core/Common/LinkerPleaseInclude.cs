using System;
using System.Windows.Input;
using UIKit;

namespace Naxam.Busuu.iOS.Core.Common
{
    public class LinkerPleaseInclude
    {
		public void Include(UIBarButtonItem button)
		{
			button.Clicked += (sender, e) => { button.Title = button.Title + ""; };
		}

		public void Include(ICommand command)
		{
			command.CanExecuteChanged += (s, e) => { if (command.CanExecute(null)) command.Execute(null); };
		}
	}
}
