using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Threading.Tasks;

namespace CSharp6Features
{
  // Value object - non-mutable object
  struct Money
  {
    // use automatic property
    private readonly decimal amount;

    public decimal Amount { get { return amount; } }

    private readonly string currency;

    public string Currency { get { return currency; } }

    public Money(decimal amount, string currency)
    {
      if (currency == null)
      {
        // Get rid of string
        throw new ArgumentNullException("currency");
      }

      this.amount = amount;
      this.currency = currency;
    }


    public override string ToString()
    {
      // Use string interpolation and make it shorter
      return string.Format("{0} ({1})", Amount, Currency);
    }
  }

  class Person : INotifyPropertyChanged
  {
    private string name;

    public string Name
    {
      get { return name; }
      set { name = value; OnPropertyChanged("Name"); }  // Get rid of string
    }      

    public int GetLengthOfName()  // This can be made way shorter
    {
      if (Name != null)
      {
        return Name.Length;
      }
      else
      {
        return 0;
      }
    }

    public Money Balance { get; set; } // Please initialize

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propname)
    {
      if (PropertyChanged != null)    // Make shorter
      {
        PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propname));
      }
    }

    public async Task GetMoreInfoAsync()
    {
      string s = null;
      try
      {
        HttpClient client = new HttpClient();
        var result = await client.GetAsync("http://www.nobodythere.com");
        s = await result.Content.ReadAsStringAsync();
      }
      catch (ArgumentNullException ex) // Only catch where argument = "x"
      {
        // Please add logging (async!)
      }
    }

    protected async Task<string> WriteToLog(string error)
    {
      return await Task.Run(() => "Error");
    }
  }

  class Program
  {
    static void Main(string[] args)
    {
      var p = new Person();
      p.PropertyChanged += (sender, e) => Console.Write(e.PropertyName);

      Dictionary<string, int> x = new Dictionary<string, int> {["peter"] = 5,["jefke"] = 10 };

      Console.Write("> ");
      var input = Console.ReadLine();
      int value = 0;
      if (int.TryParse(input, out value))
      {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Money m1 = new Money(10, "EUR");
        Console.WriteLine(m1);
      }
    }
  }
}
