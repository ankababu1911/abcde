using System.Windows.Input;

namespace abcde.Mobile.CustomControls;

public partial class CustomButton : ContentView
{
	public CustomButton()
	{
		InitializeComponent();
	}
    // BindableProperty implementation
    public static readonly BindableProperty CommandProperty =
        BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(CustomButton), null);

    public ICommand Command
    {
        get { return (ICommand)GetValue(CommandProperty); }
        set { SetValue(CommandProperty, value); }
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        if (Command != null && Command.CanExecute(null))
        {
            Command.Execute(null);
        }
    }

    public static readonly BindableProperty TextProperty = BindableProperty.Create(
       propertyName: nameof(Text),
       returnType: typeof(string),
       declaringType: typeof(OutlinedEntryControl),
       defaultValue: null,
       defaultBindingMode: BindingMode.TwoWay);

    // Bindable Text property
    public string Text
    {
        get { return (string)GetValue(TextProperty); }
        set { SetValue(TextProperty, value); }
    }
}