using System.Collections;
using System.Runtime.CompilerServices;

namespace abcde.Mobile.CustomControls;

public partial class CustomPicker : ContentView
{
    public CustomPicker()
    {
        InitializeComponent();

        showimage.IsVisible = true;
        showimage.Source = "arrow_down.png";
        listView.IsVisible = false;
        listView.ItemSelected += ListView_ItemSelected;
    }

    public static readonly BindableProperty PasswordProperty =
      BindableProperty.Create(nameof(Password), typeof(bool), typeof(CustomPicker), null);

    // Bindable password type entry
    public bool Password
    {
        get { return (bool)GetValue(PasswordProperty); }
        set { SetValue(PasswordProperty, value); }
    }

    protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);
        MainThread.BeginInvokeOnMainThread(() =>
        {
            if (propertyName == nameof(Password))
            {
                showimage.IsVisible = Password;
            }
        });

    }

    private string placeholder;

    public string Placeholder
    {
        get { return placeholder; }
        set
        {
            if (placeholder != value)
            {
                placeholder = value;
                OnPropertyChanged();
            }
        }
    }

    public static readonly BindableProperty ItemsSourceProperty =
        BindableProperty.Create(
            nameof(ItemsSource),
            typeof(IList),
            typeof(CustomPicker),
            null,
            BindingMode.TwoWay);

    public IList ItemsSource
    {
        get { return (IList)GetValue(ItemsSourceProperty); }
        set { SetValue(ItemsSourceProperty, value); }
    }

    private Color placeholderColor;

    public Color PlaceholderColor
    {
        get { return placeholderColor; }
        set
        {
            if (placeholderColor != value)
            {
                placeholderColor = value;
                OnPropertyChanged();
            }
        }
    }

    // Calculate label displacement    
    private double GetPlaceholderDistance(Label control)
    {
        var distance = 0d;
        if (DeviceInfo.Platform == DevicePlatform.iOS)
            distance = 0;
        else
            distance = 5;

        distance = control.Height + distance;
        return distance;
    }

    //invoked when entry is focused    
    private async void TextBox_Focused(object sender, FocusEventArgs e)
    {
        await TranslateLabelToTitle();
    }

    private async Task TranslateLabelToTitle()
    {
        var placeHolder = this.PlaceHolderLabel;
        var distance = GetPlaceholderDistance(placeHolder);
        await placeHolder.TranslateTo(0, -distance);
    }

    // invoked when entry is unfocused
    private async void TextBox_Unfocused(object sender, FocusEventArgs e)
    {
        await TranslateLabelToPlaceHolder();
    }

    // move label from frame border to placeholder position
    private async Task TranslateLabelToPlaceHolder()
    {
        if (string.IsNullOrEmpty(this.Text))
        {
            await this.PlaceHolderLabel.TranslateTo(0, 0);
        }
    }

    // Unfocus the password field when login page loads    
    private void TextBox_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "IsPassword")
        {
            MainThread.BeginInvokeOnMainThread(() => { TextBox.Unfocus(); });
        }
        else
        {
            // Check if the text is null or consists only of whitespace
            if (!string.IsNullOrWhiteSpace(this.Text))
            {
                TranslateLabelToTitle();
            }
            else
            {
                TranslateLabelToPlaceHolder();
            }
        }
    }

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            if (listView.IsVisible)
            {
                showimage.Source = "arrow_down.png";
                listView.IsVisible = false;
            }
            else
            {
                showimage.Source = "arrow_up.png";
                listView.IsVisible = true;

            }
        });
        await TranslateLabelToPlaceHolder();
    }

    public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(
        nameof(SelectedItem),
        typeof(string),
        typeof(CustomPicker),
        null,
        BindingMode.TwoWay);

    public string SelectedItem
    {
        get { return (string)GetValue(SelectedItemProperty); }
        set { SetValue(SelectedItemProperty, value); }
    }

    public static readonly BindableProperty ItemDisplayBindingItemProperty = BindableProperty.Create(
        nameof(ItemDisplayBinding),
        typeof(string),
        typeof(CustomPicker),
        null,
        BindingMode.TwoWay);

    public string ItemDisplayBinding
    {
        get { return (string)GetValue(ItemDisplayBindingItemProperty); }
        set { SetValue(ItemDisplayBindingItemProperty, value); }
    }

    private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem != null)
        {
            // Set the selected item's value to the Entry's Text property
            Text = e.SelectedItem.ToString();

            // Hide the ListView and update the image source
            listView.IsVisible = false;
            ((ListView)sender).SelectedItem = null;
            showimage.Source = "arrow_down.png";
        }
    }

    public static readonly BindableProperty TextProperty = BindableProperty.Create(
        propertyName: nameof(Text),
        returnType: typeof(string),
        declaringType: typeof(CustomPicker),
        defaultValue: null,
        defaultBindingMode: BindingMode.TwoWay);

    // Bindable Text property
    public string Text
    {
        get { return (string)GetValue(TextProperty); }
        set { SetValue(TextProperty, value); }
    }

    // Event handler when text is entered or changed in entry
    public event EventHandler<Microsoft.Maui.Controls.TextChangedEventArgs> TextChanged;

    // invoked when text is entered
    public virtual void OnTextChanged(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {
        TextChanged?.Invoke(this, e);
    }

}