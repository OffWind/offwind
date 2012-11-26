using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using Offwind.Infrastructure.Models;
using Offwind.Products.OpenFoam.Models;
using Offwind.Sowfa.Constant.TurbineProperties;

namespace Offwind.Products.Sowfa.UI.TurbinesSetup
{

    public sealed class VVertice : BaseViewModel
    {
        public decimal X
        {
            get { return GetProperty<decimal>("X"); }
            set { SetProperty("X", value); }
        }

        public decimal Y
        {
            get { return GetProperty<decimal>("Y"); }
            set { SetProperty("Y", value); }
        }

        public decimal Z
        {
            get { return GetProperty<decimal>("Z"); }
            set { SetProperty("Z", value); }
        }

        public VVertice(Vertice p)
        {
            if (p != null)
            {
                X = p.X;
                Y = p.Y;
                Z = p.Z;
                return;
            }
            X = Y = Z = 0;
        }
    }


    public sealed class VAirfoilBlade : BaseViewModel
    {
        public string AirfoilName
        {
            get { return GetProperty<string>("AirfoilName"); }
            set { SetProperty("AirfoilName", value); }
        }
        public ObservableCollection<VVertice> Blade;
    }

    public sealed class VTurbineType : BaseViewModel
    {
        public string PropName
        {
            get { return GetProperty<string>("PropName"); }
            set { SetProperty("PropName", value); }
        }

        public Visibility ShowInstance
        {
            get { return GetProperty<Visibility>("ShowInstance"); }
            set { SetPropertyEnum("ShowInstance", value); }
        }

        public string TipPicture
        {
            get { return GetProperty<string>("TipPicture"); }
            set { SetProperty("TipPicture", value); }
        }

        public void Update_TipPicture( )
        {
            TipPicture = (NumBl == 2) ? "/Images/blade_2.png" : "/Images/blade_3.png";
        }

        #region ViewBody
        public int NumBl
        {
            get { return GetProperty<int>("NumBl"); }
            set { SetProperty("NumBl", value); }
        }

        public decimal TipRad
        {
            get { return GetProperty<decimal>("TipRad"); }
            set { SetProperty("TipRad", value); }
        }

        public decimal HubRad
        {
            get { return GetProperty<decimal>("HubRad"); }
            set { SetProperty("HubRad", value); }
        }

        public decimal UndSling
        {
            get { return GetProperty<decimal>("UndSling"); }
            set { SetProperty("UndSling", value); }
        }

        public decimal OverHang
        {
            get { return GetProperty<decimal>("OverHang"); }
            set { SetProperty("OverHang", value); }
        }

        public decimal TowerHt
        {
            get { return GetProperty<decimal>("TowerHt"); }
            set { SetProperty("TowerHt", value); }
        }

        public decimal Twr2Shft
        {
            get { return GetProperty<decimal>("Twr2Shft"); }
            set { SetProperty("Twr2Shft", value); }
        }

        public decimal ShftTilt
        {
            get { return GetProperty<decimal>("ShftTilt"); }
            set { SetProperty("ShftTilt", value); }
        }

        public VVertice PreCone { set; get; }

        public decimal GBRatio
        {
            get { return GetProperty<decimal>("GBRatio"); }
            set { SetProperty("GBRatio", value); }
        }

        public decimal GenIner
        {
            get { return GetProperty<decimal>("GenIner"); }
            set { SetProperty("GenIner", value); }
        }

        public decimal HubIner
        {
            get { return GetProperty<decimal>("HubIner"); }
            set { SetProperty("HubIner", value); }
        }

        public decimal BladeIner
        {
            get { return GetProperty<decimal>("BladeIner"); }
            set { SetProperty("BladeIner", value); }
        }

        public ControllerType TorqueControllerType
        {
            get { return GetProperty<ControllerType>("TorqueControllerType"); }
            set { SetPropertyEnum("TorqueControllerType", value); }
        }

        public ControllerType YawControllerType
        {
            get { return GetProperty<ControllerType>("YawControllerType"); }
            set { SetPropertyEnum("YawControllerType", value); }
        }

        public ControllerType PitchControllerType
        {
            get { return GetProperty<ControllerType>("PitchControllerType"); }
            set { SetPropertyEnum("PitchControllerType", value); }
        }

        public decimal CutInGenSpeed
        {
            get { return GetProperty<decimal>("CutInGenSpeed"); }
            set { SetProperty("CutInGenSpeed", value); }
        }

        public decimal RatedGenSpeed
        {
            get { return GetProperty<decimal>("RatedGenSpeed"); }
            set { SetProperty("RatedGenSpeed", value); }
        }

        public decimal Region2StartGenSpeed
        {
            get { return GetProperty<decimal>("Region2StartGenSpeed"); }
            set { SetProperty("Region2StartGenSpeed", value); }
        }

        #region TorqueControllerParams

        public decimal Region2EndGenSpeed
        {
            get { return GetProperty<decimal>("Region2EndGenSpeed"); }
            set { SetProperty("Region2EndGenSpeed", value); }
        }

        public decimal CutInGenTorque
        {
            get { return GetProperty<decimal>("CutInGenTorque"); }
            set { SetProperty("CutInGenTorque", value); }
        }

        public decimal RatedGenTorque
        {
            get { return GetProperty<decimal>("RatedGenTorque"); }
            set { SetProperty("RatedGenTorque", value); }
        }

        public decimal RateLimitGenTorque
        {
            get { return GetProperty<decimal>("RateLimitGenTorque"); }
            set { SetProperty("RateLimitGenTorque", value); }
        }

        public decimal KGen
        {
            get { return GetProperty<decimal>("KGen"); }
            set { SetProperty("KGen", value); }
        }

        public decimal TorqueControllerRelax
        {
            get { return GetProperty<decimal>("TorqueControllerRelax"); }
            set { SetProperty("TorqueControllerRelax", value); }
        }

        #endregion

        #region PitchControllerParams
        public decimal PitchControlStartPitch
        {
            get { return GetProperty<decimal>("PitchControlStartPitch"); }
            set { SetProperty("PitchControlStartPitch", value); }
        }

        public decimal PitchControlEndPitch
        {
            get { return GetProperty<decimal>("PitchControlEndPitch"); }
            set { SetProperty("PitchControlEndPitch", value); }
        }

        public decimal PitchControlStartSpeed
        {
            get { return GetProperty<decimal>("PitchControlStartSpeed"); }
            set { SetProperty("PitchControlStartSpeed", value); }
        }

        public decimal PitchControlEndSpeed
        {
            get { return GetProperty<decimal>("PitchControlEndSpeed"); }
            set { SetProperty("PitchControlEndSpeed", value); }
        }

        public decimal RateLimitPitch
        {
            get { return GetProperty<decimal>("RateLimitPitch"); }
            set { SetProperty("RateLimitPitch", value); }
        }

        #endregion

        public ObservableCollection<VAirfoilBlade> airfoilBlade { set; get; }

        #endregion

        private readonly TurbineProperiesHandler _fileHandler;

        public VTurbineType( string typeInstance, string path, bool useDefaultData )
        {
            if (typeInstance == null) return;
            _fileHandler = new TurbineProperiesHandler(typeInstance, useDefaultData);
            var d = (TurbinePropertiesData)_fileHandler.Read(_fileHandler.GetPath(path));

            PropName = typeInstance;
            ShowInstance = Visibility.Visible;
            BladeIner = d.BladeIner;
            HubIner = d.HubIner;
            HubRad = d.HubRad;
            NumBl = d.NumBl;
            Update_TipPicture();
            TipRad = d.TipRad;
            UndSling = d.UndSling;
            OverHang = d.OverHang;
            TowerHt = d.TowerHt;
            Twr2Shft = d.Twr2Shft;
            ShftTilt = d.ShftTilt;
            PreCone = new VVertice(d.PreCone);
            GBRatio = d.GBRatio;
            GenIner = d.GenIner;
            TorqueControllerType = d.TorqueControllerType;
            YawControllerType = d.YawControllerType;
            PitchControllerType = d.PitchControllerType;

                #region Fill TorqueControllerParams
            if (d.torqueControllerParams != null)
            {
                CutInGenSpeed = d.torqueControllerParams.CutInGenSpeed;
                RatedGenSpeed = d.torqueControllerParams.RatedGenSpeed;
                Region2StartGenSpeed = d.torqueControllerParams.Region2StartGenSpeed;
                Region2EndGenSpeed = d.torqueControllerParams.Region2EndGenSpeed;
                CutInGenTorque = d.torqueControllerParams.CutInGenTorque;
                RatedGenTorque = d.torqueControllerParams.RatedGenTorque;
                RateLimitGenTorque = d.torqueControllerParams.RateLimitGenTorque;
                KGen = d.torqueControllerParams.KGen;
                TorqueControllerRelax = d.torqueControllerParams.TorqueControllerRelax;
            }

            #endregion
                #region Fill PitchControllerParams
            if (d.pitchControllerParams != null)
            {
                PitchControlStartPitch = d.pitchControllerParams.PitchControlStartPitch;
                PitchControlEndPitch = d.pitchControllerParams.PitchControlEndPitch;
                PitchControlStartSpeed = d.pitchControllerParams.PitchControlStartSpeed;
                PitchControlEndSpeed = d.pitchControllerParams.PitchControlEndSpeed;
                RateLimitPitch = d.pitchControllerParams.RateLimitPitch;
            }
            #endregion

            airfoilBlade = new ObservableCollection<VAirfoilBlade>();

            foreach (var x in d.airfoilBlade)
            {
                var copyOfBlade = new ObservableCollection<VVertice>();
                foreach (var y in x.Blade)
                {
                    copyOfBlade.Add(new VVertice(y));
                }
                airfoilBlade.Add(new VAirfoilBlade()
                {
                    AirfoilName = x.AirfoilName,
                    Blade = copyOfBlade
                });
            }
        }
        public void Save(string path)
        {
            var p = new TurbinePropertiesData()
                        {
                            BladeIner = BladeIner,
                            GBRatio = GBRatio,
                            GenIner = GenIner,
                            HubIner = HubIner,
                            HubRad = HubRad,
                            NumBl = NumBl,
                            OverHang = OverHang,
                            ShftTilt = ShftTilt,
                            TipRad = TipRad,
                            TowerHt = TowerHt,
                            Twr2Shft = Twr2Shft,
                            UndSling = UndSling,
                            PitchControllerType = PitchControllerType,
                            TorqueControllerType = TorqueControllerType,
                            YawControllerType = YawControllerType,
                            PreCone = new Vertice()
                                          {
                                              X = PreCone.X,
                                              Y = PreCone.Y,
                                              Z = PreCone.Z
                                          },
                            pitchControllerParams = new PitchControllerParams()
                                                        {
                                                            PitchControlEndPitch = PitchControlEndPitch,
                                                            PitchControlEndSpeed = PitchControlEndSpeed,
                                                            PitchControlStartPitch = PitchControlStartPitch,
                                                            PitchControlStartSpeed = PitchControlStartSpeed,
                                                            RateLimitPitch = RateLimitPitch
                                                        },
                            torqueControllerParams = new TorqueControllerParams()
                                                         {
                                                             CutInGenSpeed = CutInGenSpeed,
                                                             CutInGenTorque = CutInGenTorque,
                                                             KGen = KGen,
                                                             RateLimitGenTorque = RateLimitGenTorque,
                                                             RatedGenSpeed = RatedGenSpeed,
                                                             RatedGenTorque = RatedGenTorque,
                                                             Region2EndGenSpeed = Region2EndGenSpeed,
                                                             Region2StartGenSpeed = Region2StartGenSpeed,
                                                             TorqueControllerRelax = TorqueControllerRelax
                                                         },
                            airfoilBlade = new List<AirfoilBlade>()
                        };
            foreach (var airFoil in airfoilBlade)
            {
                var x = new AirfoilBlade()
                            {
                                AirfoilName = airFoil.AirfoilName,
                                Blade = new List<Vertice>()
                            };
                foreach( var blade in airFoil.Blade)
                {
                    x.Blade.Add( new Vertice(blade.X, blade.Y, blade.Z));
                }
                p.airfoilBlade.Add(x);
            }
            _fileHandler.FileName = PropName;
            _fileHandler.Write(_fileHandler.GetPath(path), p);
        }
    }

    public sealed class VTurbineProperties : BaseViewModel
    {
        public ObservableCollection<VTurbineType> TurbineTypes { set; get; }
    }
}
