using abcde.Mobile.Resx;
using Microsoft.AspNetCore.Http;
using System.Runtime.CompilerServices;

namespace abcde.Mobile.CustomControls;

public partial class CustomDateControl : ContentView
{
	public CustomDateControl()
	{
		InitializeComponent();
        DatePicker.DateSelected += DatePicker_DateSelected;
        DatePicker.Date = DateTime.Now;
        TextBox.Text = DateTime.Now.ToString(Helpers.Constants.DateFormat);
        calendarPicker.IsVisible = true;
        calendarPicker.Source = "calender_icon.png";
    }
    
 

    public static readonly BindableProperty TextProperty = BindableProperty.Create(
        propertyName: nameof(Text),
        returnType: typeof(string),
        declaringType: typeof(CustomDateControl),
        defaultValue: DateTime.Now.ToString(),
        defaultBindingMode: BindingMode.TwoWay);

    public static readonly BindableProperty CalendarVisibleProperty =
        BindableProperty.Create(nameof(CalendarVisible), typeof(bool), typeof(CustomDateControl), false);

    public bool CalendarVisible
    {
        get { return (bool)GetValue(CalendarVisibleProperty); }
        set { SetValue(CalendarVisibleProperty, value); }
    }

    public string Text
    {
        get { return (string)GetValue(TextProperty); }
        set 
        {
            DateTime value1 = Convert.ToDateTime(value);
            if(value!=null)
            {
                SetValue(TextProperty, value1.ToString(Helpers.Constants.DateFormat));
            }
            else
            {
                SetValue(TextProperty, value);
            }
        }
    }

    public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(
        propertyName: nameof(Placeholder),
        returnType: typeof(string),
        declaringType: typeof(CustomDateControl),
        defaultValue: null,
        defaultBindingMode: BindingMode.OneWay);

    public string Placeholder
    {
        get { return (string)GetValue(PlaceholderProperty); }
        set { SetValue(PlaceholderProperty, value); }
    }

    public static readonly BindableProperty PlaceholderColorProperty =
        BindableProperty.Create(nameof(PlaceholderColor), typeof(Color), typeof(CustomDateControl), Colors.Blue);

    public Color PlaceholderColor
    {
        get { return (Color)GetValue(PlaceholderColorProperty); }
        set { SetValue(PlaceholderColorProperty, value); }
    }

    public static readonly BindableProperty SelectedDateProperty =
        BindableProperty.Create(nameof(SelectedDate), typeof(DateTime), typeof(CustomDateControl), DateTime.Now);

    public DateTime SelectedDate
    {
        get { return (DateTime)GetValue(SelectedDateProperty); }
        set { SetValue(SelectedDateProperty, value); }
    }

    public static readonly BindableProperty DateProperty =
        BindableProperty.Create(nameof(Date), typeof(DateTime), typeof(CustomDateControl), DateTime.Now);

    public DateTime Date
    {
        get { return (DateTime)GetValue(DateProperty); }
        set { SetValue(DateProperty, value); }
    }

    public static readonly BindableProperty BorderColorProperty =
    BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(CustomDateControl), Colors.Blue);

    // Bindable entry border color
    public Color BorderColor
    {
        get { return (Color)GetValue(BorderColorProperty); }
        set { SetValue(BorderColorProperty, value); }
    }
    
    private async void TextBox_Focused(object sender, FocusEventArgs e)
    {
        await UpdateTextBoxAppearance();
    }

    private async void TextBox_Unfocused(object sender, FocusEventArgs e)
    {
        await TranslateLabelToPlaceHolder();
    }   

    private async Task UpdateTextBoxAppearance()
    {
            var placeHolder = this.PlaceHolderLabel;
            var distance = GetPlaceholderDistance(placeHolder);
            await placeHolder.TranslateTo(0, -distance);
        
    }
    private async Task TranslateLabelToPlaceHolder()
    {
        if (string.IsNullOrEmpty(this.Text))
        {
            await this.PlaceHolderLabel.TranslateTo(0, 0);
        }
    }

    private double GetPlaceholderDistance(Label control)
    {
        var distance = 0d;
        if (DeviceInfo.Platform == DevicePlatform.iOS)
            distance = 7;
        else
            distance = 5;

        distance = control.Height + distance;
        return distance;
    }

    public event EventHandler<TextChangedEventArgs> TextChanged;

    public virtual void OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (sender is null)
        {
            throw new ArgumentNullException(nameof(sender));
        }

        TextChanged?.Invoke(this, e);
    }
    
    private void TextBox_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(TextBox.IsPassword))
        {
            MainThread.BeginInvokeOnMainThread(() => { TextBox.Unfocus(); });
        }
        if (!string.IsNullOrWhiteSpace(this.Text))
        {
            TextBox.Text = this.Text;
            UpdateTextBoxAppearance();
        }
        else
        {
            TranslateLabelToPlaceHolder();
        }
    }
    
    protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);        
        MainThread.BeginInvokeOnMainThread(() =>
        {
            if (propertyName == nameof(CalendarVisible))
            {
                calendarPicker.IsVisible = CalendarVisible;
            }
        });
    }

    private void ImageButton_Clicked(object sender, EventArgs e)
    {
        DatePicker.IsVisible = true;
        TextBox.Text = DateTime.Now.ToString(Helpers.Constants.DateFormat);
        TextBox.IsPassword = false;
    }

    private async void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
    {
        DateTime selectedDate = e.NewDate.Date;
        DateTime currentTime = DateTime.Now;
        if(selectedDate == DateTime.Today)
        {
            await UpdateTextBoxAppearance();
            string formattedDateTime = currentTime.ToString(Helpers.Constants.DateFormat);
            TextBox.Text = formattedDateTime;
            TextBox.IsPassword = false;
        }
        else
        {
            await UpdateTextBoxAppearance();
            DateTime combinedDateTime = new DateTime(
            selectedDate.Year,
            selectedDate.Month,
            selectedDate.Day,
            currentTime.Hour,
            currentTime.Minute,
            currentTime.Second
        );
            string formattedDateTime = combinedDateTime.ToString(Helpers.Constants.DateFormat);
            TextBox.Text = formattedDateTime;
            TextBox.IsPassword = false;
        }
       
        
    }
}