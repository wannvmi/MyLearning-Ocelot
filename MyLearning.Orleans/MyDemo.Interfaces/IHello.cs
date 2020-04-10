using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyDemo.Interfaces
{
    /// <summary>
    /// Orleans grain communication interface IHello
    /// </summary>
    public interface IHello : Orleans.IGrainWithIntegerKey
    {
        Task<string> SayHello(string greeting);
    }
}
