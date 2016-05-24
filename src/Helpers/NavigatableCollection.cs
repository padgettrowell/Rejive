using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Rejive
{
    public class NavigatableCollection<T> : ObservableCollection<T>
    {
        public delegate void CurrentItemChangedHandler(T previous, T current);
        public event CurrentItemChangedHandler CurrentItemChanged;


        public NavigatableCollection() { }

        public NavigatableCollection(List<T> list) : base(list) { }

        public NavigatableCollection(IEnumerable<T> collection) : base(collection) { }

        private System.ComponentModel.ICollectionView GetDefaultView()
        {
            return System.Windows.Data.CollectionViewSource.GetDefaultView(this);
        }

        public int CurrentPosition
        {
            get
            {
                return GetDefaultView().CurrentPosition;
            }
        }

        public T CurrentItem
        {
            get
            {
                return (T)GetDefaultView().CurrentItem;
            }
        }

        public void MoveTo(T item)
        {
            var c = CurrentItem;
            GetDefaultView().MoveCurrentTo(item);
            OnCurrentItemChanged(c, CurrentItem);
        }

        public void MoveFirst()
        {
            var c = CurrentItem;
            GetDefaultView().MoveCurrentToFirst();
            OnCurrentItemChanged(c, CurrentItem);
        }

        public void MovePrevious()
        {
            var c = CurrentItem;
            GetDefaultView().MoveCurrentToPrevious();
            OnCurrentItemChanged(c, CurrentItem);
        }

        public void MoveNext()
        {
            var c = CurrentItem;
            GetDefaultView().MoveCurrentToNext();
            OnCurrentItemChanged(c, CurrentItem);
        }

        public void MoveLast()
        {
            var c = CurrentItem;
            GetDefaultView().MoveCurrentToLast();
            OnCurrentItemChanged(c, CurrentItem);
        }

        private void OnCurrentItemChanged(T changedFrom, T changedTo)
        {
            if (CurrentItemChanged != null)
                CurrentItemChanged(changedFrom, changedTo);
        }
    }
}
