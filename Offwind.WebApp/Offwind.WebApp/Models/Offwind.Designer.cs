﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data.EntityClient;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Runtime.Serialization;

[assembly: EdmSchemaAttribute()]

namespace Offwind.WebApp.Models
{
    #region Contexts
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class OffwindEntities : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new OffwindEntities object using the connection string found in the 'OffwindEntities' section of the application configuration file.
        /// </summary>
        public OffwindEntities() : base("name=OffwindEntities", "OffwindEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new OffwindEntities object.
        /// </summary>
        public OffwindEntities(string connectionString) : base(connectionString, "OffwindEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new OffwindEntities object.
        /// </summary>
        public OffwindEntities(EntityConnection connection) : base(connection, "OffwindEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        #endregion
    
        #region Partial Methods
    
        partial void OnContextCreated();
    
        #endregion
    
        #region ObjectSet Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<DRunningCase> DRunningCases
        {
            get
            {
                if ((_DRunningCases == null))
                {
                    _DRunningCases = base.CreateObjectSet<DRunningCase>("DRunningCases");
                }
                return _DRunningCases;
            }
        }
        private ObjectSet<DRunningCase> _DRunningCases;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<DWorkCase> DWorkCases
        {
            get
            {
                if ((_DWorkCases == null))
                {
                    _DWorkCases = base.CreateObjectSet<DWorkCase>("DWorkCases");
                }
                return _DWorkCases;
            }
        }
        private ObjectSet<DWorkCase> _DWorkCases;

        #endregion
        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the DRunningCases EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToDRunningCases(DRunningCase dRunningCase)
        {
            base.AddObject("DRunningCases", dRunningCase);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the DWorkCases EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToDWorkCases(DWorkCase dWorkCase)
        {
            base.AddObject("DWorkCases", dWorkCase);
        }

        #endregion
    }
    

    #endregion
    
    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="Offwind.DbModels", Name="DRunningCase")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class DRunningCase : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new DRunningCase object.
        /// </summary>
        /// <param name="id">Initial value of the Id property.</param>
        /// <param name="started">Initial value of the Started property.</param>
        /// <param name="finished">Initial value of the Finished property.</param>
        /// <param name="owner">Initial value of the Owner property.</param>
        /// <param name="name">Initial value of the Name property.</param>
        /// <param name="state">Initial value of the State property.</param>
        /// <param name="result">Initial value of the Result property.</param>
        public static DRunningCase CreateDRunningCase(global::System.Guid id, global::System.DateTime started, global::System.DateTime finished, global::System.String owner, global::System.String name, global::System.String state, global::System.String result)
        {
            DRunningCase dRunningCase = new DRunningCase();
            dRunningCase.Id = id;
            dRunningCase.Started = started;
            dRunningCase.Finished = finished;
            dRunningCase.Owner = owner;
            dRunningCase.Name = name;
            dRunningCase.State = state;
            dRunningCase.Result = result;
            return dRunningCase;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Guid Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if (_Id != value)
                {
                    OnIdChanging(value);
                    ReportPropertyChanging("Id");
                    _Id = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("Id");
                    OnIdChanged();
                }
            }
        }
        private global::System.Guid _Id;
        partial void OnIdChanging(global::System.Guid value);
        partial void OnIdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.DateTime Started
        {
            get
            {
                return _Started;
            }
            set
            {
                OnStartedChanging(value);
                ReportPropertyChanging("Started");
                _Started = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Started");
                OnStartedChanged();
            }
        }
        private global::System.DateTime _Started;
        partial void OnStartedChanging(global::System.DateTime value);
        partial void OnStartedChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.DateTime Finished
        {
            get
            {
                return _Finished;
            }
            set
            {
                OnFinishedChanging(value);
                ReportPropertyChanging("Finished");
                _Finished = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Finished");
                OnFinishedChanged();
            }
        }
        private global::System.DateTime _Finished;
        partial void OnFinishedChanging(global::System.DateTime value);
        partial void OnFinishedChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Owner
        {
            get
            {
                return _Owner;
            }
            set
            {
                OnOwnerChanging(value);
                ReportPropertyChanging("Owner");
                _Owner = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Owner");
                OnOwnerChanged();
            }
        }
        private global::System.String _Owner;
        partial void OnOwnerChanging(global::System.String value);
        partial void OnOwnerChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Name
        {
            get
            {
                return _Name;
            }
            set
            {
                OnNameChanging(value);
                ReportPropertyChanging("Name");
                _Name = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Name");
                OnNameChanged();
            }
        }
        private global::System.String _Name;
        partial void OnNameChanging(global::System.String value);
        partial void OnNameChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String State
        {
            get
            {
                return _State;
            }
            set
            {
                OnStateChanging(value);
                ReportPropertyChanging("State");
                _State = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("State");
                OnStateChanged();
            }
        }
        private global::System.String _State;
        partial void OnStateChanging(global::System.String value);
        partial void OnStateChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Result
        {
            get
            {
                return _Result;
            }
            set
            {
                OnResultChanging(value);
                ReportPropertyChanging("Result");
                _Result = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Result");
                OnResultChanged();
            }
        }
        private global::System.String _Result;
        partial void OnResultChanging(global::System.String value);
        partial void OnResultChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Model
        {
            get
            {
                return _Model;
            }
            set
            {
                OnModelChanging(value);
                ReportPropertyChanging("Model");
                _Model = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Model");
                OnModelChanged();
            }
        }
        private global::System.String _Model;
        partial void OnModelChanging(global::System.String value);
        partial void OnModelChanged();

        #endregion
    
    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="Offwind.DbModels", Name="DWorkCase")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class DWorkCase : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new DWorkCase object.
        /// </summary>
        /// <param name="id">Initial value of the Id property.</param>
        /// <param name="created">Initial value of the Created property.</param>
        /// <param name="owner">Initial value of the Owner property.</param>
        /// <param name="name">Initial value of the Name property.</param>
        public static DWorkCase CreateDWorkCase(global::System.Guid id, global::System.DateTime created, global::System.String owner, global::System.String name)
        {
            DWorkCase dWorkCase = new DWorkCase();
            dWorkCase.Id = id;
            dWorkCase.Created = created;
            dWorkCase.Owner = owner;
            dWorkCase.Name = name;
            return dWorkCase;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Guid Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if (_Id != value)
                {
                    OnIdChanging(value);
                    ReportPropertyChanging("Id");
                    _Id = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("Id");
                    OnIdChanged();
                }
            }
        }
        private global::System.Guid _Id;
        partial void OnIdChanging(global::System.Guid value);
        partial void OnIdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.DateTime Created
        {
            get
            {
                return _Created;
            }
            set
            {
                OnCreatedChanging(value);
                ReportPropertyChanging("Created");
                _Created = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Created");
                OnCreatedChanged();
            }
        }
        private global::System.DateTime _Created;
        partial void OnCreatedChanging(global::System.DateTime value);
        partial void OnCreatedChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Owner
        {
            get
            {
                return _Owner;
            }
            set
            {
                OnOwnerChanging(value);
                ReportPropertyChanging("Owner");
                _Owner = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Owner");
                OnOwnerChanged();
            }
        }
        private global::System.String _Owner;
        partial void OnOwnerChanging(global::System.String value);
        partial void OnOwnerChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Name
        {
            get
            {
                return _Name;
            }
            set
            {
                OnNameChanging(value);
                ReportPropertyChanging("Name");
                _Name = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Name");
                OnNameChanged();
            }
        }
        private global::System.String _Name;
        partial void OnNameChanging(global::System.String value);
        partial void OnNameChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Model
        {
            get
            {
                return _Model;
            }
            set
            {
                OnModelChanging(value);
                ReportPropertyChanging("Model");
                _Model = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Model");
                OnModelChanged();
            }
        }
        private global::System.String _Model;
        partial void OnModelChanging(global::System.String value);
        partial void OnModelChanged();

        #endregion
    
    }

    #endregion
    
}
