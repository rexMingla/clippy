using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RexMingla.ClipboardManager
{
    public delegate void OnClipboardChangeEventHandler(ClipboardContent content);

    public interface IClipboardNotifier
    {
        void Start();
        void Stop();

        event OnClipboardChangeEventHandler OnClipboardChange;
    }
}
