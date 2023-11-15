using Microsoft.Maui.Handlers;
#if IOS
using UIKit;
#endif


namespace abcde.Mobile.CustomControls
{
    public class CustomScrollview : ScrollView
    {
        public CustomScrollview()
        {
#if IOS
      ScrollViewHandler.Mapper.AppendToMapping(nameof(IScrollView.ContentSize), OnScrollViewContentSizePropertyChanged);
#endif
        }

#if IOS
  private void OnScrollViewContentSizePropertyChanged(IScrollViewHandler _, IScrollView __)
  {
      if (Handler?.PlatformView is not UIView platformUiView)
          return;
      
      if (platformUiView.Subviews.FirstOrDefault() is not { } contentView)
          return;
      
      contentView.SizeToFit();
  }
#endif
    }
}
