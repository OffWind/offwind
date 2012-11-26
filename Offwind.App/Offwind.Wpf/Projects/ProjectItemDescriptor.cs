using System;
using System.Collections.Generic;
using System.Windows.Controls;
using Offwind.Infrastructure;
using Offwind.Products.OpenFoam.Models;

namespace Offwind.Projects
{
    public sealed class ProjectItemDescriptor
    {
        public string Code { get; private set; }
        public string DefaultName { get; private set; }
        public string NodeName { get; private set; }
        public bool NodeInvisible { get; private set; }
        public Type Form { get; private set; }
        public Action<IProjectItemView> FormInitializer { get; private set; }
        public Type CommandSave { get; private set; }
        public Type CommandBuild { get; private set; }
        public Type Handler { get; private set; }
        public Action<FoamFileHandler> HandlerInitializer { get; private set; }
        public bool NoScroll { get; private set; }

        public Control CreateContentControl()
        {
            var control = (Control)Activator.CreateInstance(Form);
            var itemView = control as IProjectItemView;
            if (itemView != null)
            {
                if (FormInitializer != null)
                {
                    FormInitializer(itemView);
                }
                var handler = CreateHandler();
                itemView.SetFileHandler(handler);
            }
            return control;
        }

        public FoamFileHandler CreateHandler()
        {
            var handler = (FoamFileHandler)Activator.CreateInstance(Handler);
            if (HandlerInitializer != null)
            {
                HandlerInitializer(handler);
            }
            return handler;
        }

        public void AddTo(List<ProjectItemDescriptor> target)
        {
            target.Add(this);
        }

        public ProjectItemDescriptor SetCode(string val)
        {
            Code = val;
            return this;
        }

        public ProjectItemDescriptor SetDefaultName(string val)
        {
            DefaultName = val;
            if (NodeName == null || NodeName.Trim().Length == 0)
            {
                NodeName = val;
            }
            return this;
        }

        public ProjectItemDescriptor SetNodeName(string val)
        {
            NodeName = val;
            return this;
        }

        public ProjectItemDescriptor SetNodeInvisible(bool val)
        {
            NodeInvisible = val;
            return this;
        }

        public ProjectItemDescriptor SetForm(Type val)
        {
            Form = val;
            return this;
        }

        public ProjectItemDescriptor SetFormInitializer(Action<IProjectItemView> val)
        {
            FormInitializer = val;
            return this;
        }

        public ProjectItemDescriptor SetHandler(Type val)
        {
            Handler = val;
            return this;
        }

        public ProjectItemDescriptor SetHandlerInitializer(Action<FoamFileHandler> val)
        {
            HandlerInitializer = val;
            return this;
        }

        public ProjectItemDescriptor SetNoScroll(bool val)
        {
            NoScroll = val;
            return this;
        }

        public ProjectItemDescriptor AddTo<TKey>(Dictionary<TKey, ProjectItemDescriptor> target, TKey key)
        {
            SetCode(key.ToString());
            target.Add(key, this);
            return this;
        }
    }
}
