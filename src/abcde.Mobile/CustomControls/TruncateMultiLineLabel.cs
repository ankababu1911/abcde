namespace abcde.Mobile.CustomControls
{
    public class TruncateMultiLineLabel : Label
    {
        public int MaxLines { get; set; } = 3;

        public TruncateMultiLineLabel()
        {
            MaxLines = 3;
            LineBreakMode = LineBreakMode.TailTruncation;
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            UpdateMaxLines(height);
        }

        private void UpdateMaxLines(double height)
        {
            if (Device.RuntimePlatform == Device.Android)
            {
                // Adjust the number of lines based on available height
                MaxLines = (int)(height / LineHeight);
            }
        }
    }
}
