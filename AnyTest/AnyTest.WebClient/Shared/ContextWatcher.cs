using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace AnyTest.WebClient.Shared
{
    public class ContextWatcher : ComponentBase
    {
        [CascadingParameter]
        protected EditContext EditContext { get; set; }

        public bool Validate() => EditContext?.Validate() ?? false;
    }
}
