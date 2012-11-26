using System;
using System.Windows;
using Offwind.Infrastructure.Models;

namespace Offwind.Infrastructure
{
    public sealed class ModelChangedIndicator
    {
        private readonly Window _window;

        public ModelChangedIndicator(Window window)
        {
            _window = window;
        }

        public void ModelChangedHandler(BaseViewModel model)
        {
            var txt = _window.Title.TrimEnd('*', ' ');
            _window.Title = model.IsChanged
                ? String.Format("{0}*", txt)
                : String.Format("{0}", txt);
        }
    }
}
