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
        protected  bool unchanged = true;
        protected ValidatableForm<T> form;

        [Parameter]
        public virtual bool HideButtons { get; set; } = false;

        [Parameter]
        public virtual EventCallback OnSubmit { get; set; }

        [Parameter]
        public virtual EventCallback OnCancel { get; set; }

        [Parameter]
        public virtual bool Wide { get; set; } = false;

        [Parameter]
        public virtual bool Disabled { get; set; } = false;

        public virtual bool Validate() => form.Validate();
    }
}
