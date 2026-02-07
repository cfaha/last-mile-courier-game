using System.Collections.Generic;

public class OwnedItems
{
    private readonly HashSet<string> _items = new HashSet<string>();

    public bool IsOwned(string id) => _items.Contains(id);

    public void Add(string id)
    {
        if (!string.IsNullOrEmpty(id)) _items.Add(id);
    }
}
