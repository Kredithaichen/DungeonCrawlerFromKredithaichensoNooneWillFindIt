using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStatSource
{
    bool SourceIsEqualTo(IStatSource source);
}
