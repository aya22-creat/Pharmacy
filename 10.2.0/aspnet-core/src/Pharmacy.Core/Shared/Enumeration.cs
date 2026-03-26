using System.Collections.Generic;
using System.Linq;
using System;
using System.Reflection;

namespace Pharmacy.Shared;
public abstract class Enumeration : IComparable
{
    public int Id { get; }
    public string Name { get; }

    protected Enumeration(int id, string name)
    {
        Id = id;
        Name = name;

    }
     // get all class & static field instances of the enumeration (read value ) &  transforms orderStatus   return list 
    
        public static IEnumerable<T> GetAll<T>() where T : Enumeration =>
    typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static| BindingFlags.DeclaredOnly)
    .Select(f => (T)f.GetValue(null)).Cast<T>();

    public override string ToString() => Name;
    public override int GetHashCode() => Id.GetHashCode();
    public int CompareTo(object obj) => Id.CompareTo(((Enumeration)obj).Id);

    public override bool Equals(object obj)
    {
        if (obj is not Enumeration other)
            return false;
        return GetType() == other.GetType() && Id == other.Id;
    }
     public static bool operator ==(Enumeration a, Enumeration b)
        {
            if (a is null && b is null) return true;
            if (a is null || b is null) return false;
            return a.Equals(b);
        }

        public static bool operator !=(Enumeration a, Enumeration b) => !(a == b);
    }
    
