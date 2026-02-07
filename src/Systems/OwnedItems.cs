using System.Collections.Generic;

public class OwnedItems
{
    private readonly HashSet<string> _items = new HashSet<string>();

    public bool IsOwned(string id) => _items.Contains(id);

    public void Add(string id)
    {
        if (!string.IsNullOrEmpty(id)) _items.Add(id);
    }

    public void Load(string csv)
    {
        _items.Clear();
        if (string.IsNullOrEmpty(csv)) return;
        var parts = csv.Split(',');
        foreach (var p in parts)
        {
            if (!string.IsNullOrEmpty(p)) _items.Add(p);
        }
    }

    public string ToCsv()
    {
        return string.Join(",", _items);
    }
}
