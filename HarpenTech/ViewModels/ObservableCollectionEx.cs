using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace HarpenTechApp.ViewModels;

/// <summary>
/// A custom ObservableCollection class with additional functionality.
/// </summary>
/// <typeparam name="T">The type of elements in the collection.</typeparam>
public class ObservableCollectionEx<T> : ObservableCollection<T>
{
    // Default constructor
    public ObservableCollectionEx() : base()
    {
    }

    // Constructor that initializes the collection with an existing IEnumerable
    public ObservableCollectionEx(IEnumerable<T> collection) : base(collection)
    {
    }

    /// <summary>
    /// Constructor that initializes the collection with an existing List
    /// </summary>
    /// <param name="list"></param>
    public ObservableCollectionEx(List<T> list) : base(list)
    {
    }

    /// <summary>
    /// Reloads the data in the collection based on a new set of items
    /// </summary>
    /// <param name="items">The new set of items to replace the existing collection.</param>
    public void ReloadData(IEnumerable<T> items)
    {
        ReloadData(
            innerList =>
            {
                // Add each item from the new set to the collection
                foreach (var item in items)
                {
                    innerList.Add(item);
                }
            });
    }

    /// <summary>
    /// Reloads the data in the collection based on an action performed on the inner list
    /// </summary>
    /// <param name="innerListAction">The action to be performed on the inner list.</param>
    public void ReloadData(Action<IList<T>> innerListAction)
    {
        // Clear the existing items in the collection
        Items.Clear();

        // Perform the specified action on the inner list
        innerListAction(Items);

        // Notify property and collection changes
        OnPropertyChanged(new PropertyChangedEventArgs(nameof(Count)));
        OnPropertyChanged(new PropertyChangedEventArgs("Items[]"));
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }

    /// <summary>
    /// Reloads the data asynchronously based on an async action performed on the inner list
    /// </summary>
    /// <param name="innerListAction">The async action to be performed on the inner list.</param>
    /// <returns></returns>
    public async Task ReloadDataAsync(Func<IList<T>, Task> innerListAction)
    {
        // Clear the existing items in the collection
        Items.Clear();

        // Perform the specified async action on the inner list
        await innerListAction(Items);

        // Notify property and collection changes
        OnPropertyChanged(new PropertyChangedEventArgs(nameof(Count)));
        OnPropertyChanged(new PropertyChangedEventArgs("Items[]"));
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }
}

