# model-subscribe
This code was written by David Pruitt in 2016 while working at the Texas Biomedical Device Center. It is a simple example project using WPF and C# that demonstrates how to easily subscribe to changes on the model and propagate notifications through the view-model to the view.

Typically, in the MVVM pattern, view-model classes must "subscribe" to changes from the model class. This is usually done by writing a function that acts as an "event listener". The model class will implement the INotifyPropertyChanged interface, and "notify" the view-model of changes that occur on the model. The view-model class then has a function that receives these notifcations, and essentially just passes the notifications onto the UI.

The typical "listener" function in a view-model class will look something like this:

```
void ReactToModelPropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
{
    //Get the name of the property that was changed on the model
    string model_property_changed = e.PropertyName;

    if (model_property_changed.Equals("Some property name on the model goes here"))
    {
        NotifyPropertyChanged("The name of a correspoding property on the view-model")
    }
    else if (model_property_changed.Equals("Another property name"))
    {
        NotifyPropertyChanged("Another corresponding view-model property");
    }
    else if ( ... )
    {
        ...
    }
    else if ( ... )
    {
        ...
    }
}
```

For many view-model classes, especially larger ones, this leads to long if-statements that are laborious to both create and debug. For this reason, I wrote this example project (and maybe even a framework, if I would dare to call it that?) that demonstrates how to simplify the MVVM notification procedures. Using this code, you can now declare your view-model properties like this:

```
[ReactToModelPropertyChanged(new string[] { "CorrespondingModelProperty" })]
public string MyViewModelProperty
{
    get
    {
        return ModelClass.CorrespondingModelProperty;
    }
}
```

And it handles the rest! All you have to do is put those statements on each of your view-model properties. You can even cause your view-model properties to "react" to multiple properties being changed on the model.

Feel free to contact me if you have any questions!
