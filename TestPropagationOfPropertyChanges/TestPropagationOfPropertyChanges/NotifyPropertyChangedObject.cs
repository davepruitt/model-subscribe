using System;
using System.ComponentModel;

namespace TestPropagationOfPropertyChanges
{
	public abstract class NotifyPropertyChangedObject : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged implementation

		public event PropertyChangedEventHandler PropertyChanged;

		public void NotifyPropertyChanged (string propertyName)
		{
			if (PropertyChanged != null) 
			{
				PropertyChanged (this, new PropertyChangedEventArgs (propertyName));
			}
		}

		#endregion
	}
}

