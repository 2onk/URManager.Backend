using URManager.Backend.ViewModel;

namespace URManager.Backend.Model
{
    public abstract class TabItems : ViewModelBase
    {
        public TabItems(object name, object icon, bool isClosable)
        {
            Name = name;
            Icon = icon;
            IsClosable = isClosable;
        }
        public object Name { get;}
        public object Icon { get;}
        public bool IsClosable { get;}
    }
}
