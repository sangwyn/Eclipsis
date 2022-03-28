using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    public int i, j;

    public Cell(int _i, int _j)
    {
        i = _i;
        j = _j;
    }

    public static bool operator ==(Cell a, Cell b)
    {
        return (a.i == b.i) && (a.j == b.j);
    }

    public static bool operator !=(Cell a, Cell b)
    {
        return !(a == b);
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        var b2 = (Cell)obj;
        return i == b2.i && j == b2.j;
    }
    
    public override int GetHashCode()
    {
        return i.GetHashCode() + j.GetHashCode();
    }
}
