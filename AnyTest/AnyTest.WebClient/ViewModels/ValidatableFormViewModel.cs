using AnyTest.Model;
using AnyTest.WebClient.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnyTest.WebClient.ViewModels
{
    public class ValidatableFormViewModel<T> : ComponentBase
    {
        protected bool unchanged = true;
        protected ValidatableForm<T> form;

        [Parameter]
        public bool HideButtons { get; set; } = false;

        [Parameter]
        public EventCallback OnSubmit { get; set; }

        [Parameter]
        public EventCallback OnCancel { get; set; }

        [Parameter]
        public bool Wide { get; set; } = false;

        [Parameter]
        public bool Disabled { get; set; } = false;

        public bool Validate() => form.Validate();
    }
}
