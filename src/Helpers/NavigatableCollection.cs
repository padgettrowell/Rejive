using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Rejive
{
    public class NavigatableCollection<T> : ObservableCollection<T>
    {
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
            GetDefaultView().MoveCurrentTo(item);
        }

        public void MoveFirst()
        {
            GetDefaultView().MoveCurrentToFirst();
        }

        public void MovePrevious()
        {
            GetDefaultView().MoveCurrentToPrevious();
        }

        public void MoveNext()
        {
            GetDefaultView().MoveCurrentToNext();
        }

        public void MoveLast()
        {
            GetDefaultView().MoveCurrentToLast();
        }
    }
}
