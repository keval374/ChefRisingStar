using System.Diagnostics;

namespace ChefRisingStar.Models
{
    [DebuggerDisplay("{GetDebuggerDisplay}")]
    public class SelectableFilter : BaseNotifyModel
    {
        private string _text;
        public bool _isSelected;

        public bool IsSelected
        {
            get { return _isSelected; }
            set { SetProperty(ref _isSelected, value); }
        }

        
        public string Text
        {
            get { return _text; }
            set { SetProperty(ref _text, value); }
        }

        public SelectableFilter(string text)
        {
            _text = text;
        }

        private string GetDebuggerDisplay()
        {
            return $"{Text} - {IsSelected}";
        }
    }
}
