using System;

namespace TestPropagationOfPropertyChanges
{
	public class ViewModel : NotifyPropertyChangedObject
	{
		#region Private data members

		//This is public for testing purposes
		public Model _model = new Model();

		#endregion

		#region Constructors

		public ViewModel ()
		{
			_model.PropertyChanged += ReactToModelPropertyChanged;
		}

		#endregion

		#region Properties

		[ListenForModelPropertyChangedAttribute(new string [] {"FirstName", "LastName"})]
		public string FullName 
		{
			get
			{
				return _model.FirstName + _model.LastName;
			}
		}

        public string FirstName
        {
            get
            {
                return _model.FirstName;
            }
            set
            {
                _model.FirstName = value;
            }
        }

        public string LastName
        {
            get
            {
                return _model.LastName;
            }
            set
            {
                _model.LastName = value;
            }
        }

		#endregion

		#region Methods

		void ReactToModelPropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			//Get the name of the property that was changed on the model
			string model_property_changed = e.PropertyName;

			//Get a System.Type object representing the current view-model object
			System.Type t = typeof(ViewModel);

			//Retrieve all property info for the view-model
			var property_info = t.GetProperties();

			//Iterate through each property
			foreach (var property in property_info) 
			{
				//Get the custom attributes defined for this property
				var attributes = property.GetCustomAttributes (false);
				foreach (var attribute in attributes) 
				{
					//If the property is listening for changes on the model
					var a = attribute as ListenForModelPropertyChangedAttribute;
					if (a != null) 
					{
						//If the property that was changed on the model matches the name
						//that this view-model property is listening for...
						if (a.ModelPropertyNames.Contains(model_property_changed))
						{
							//Notify the UI that the view-model property has been changed
							NotifyPropertyChanged (property.Name);
						}
					}
				}
			}

		}

		#endregion
	}
}

