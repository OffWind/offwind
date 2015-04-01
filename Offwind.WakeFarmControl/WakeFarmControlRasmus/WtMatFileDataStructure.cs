using System;
using ILNumerics;

namespace WakeFarmControlR
{
    class WtMatFileDataStructure
    {
        public class C_
        {
            public ILArray<double> table;
            public ILArray<double> beta;
            public ILArray<double> tsr;
        }

        public class Gen
        {
            public double effeciency;
        }

        public class Rotor
        {
            public double radius;
            public double inertia;
            public double ratedspeed;
        }

        public class Ctrl
        {
            public class Pitch
            {
                public double Pgain;
                public double Igain;
                public double ratelim;
                public double ulim;
                public double llim;
            }

            public class Torq
            {
                public double ratelim;
            }

            public Pitch pitch = new Pitch();
            public Torq torq = new Torq();
            public double p_rated;
        }

        public C_ cp = new C_();
        public C_ ct = new C_();
        public Gen gen = new Gen();
        public Rotor rotor = new Rotor();
        public Ctrl ctrl = new Ctrl();

        public static implicit operator WtMatFileDataStructure (ILMatFile ILMatFile)
        {
            return new WtMatFileDataStructure(ILMatFile);
        }

        public WtMatFileDataStructure(ILMatFile ilMatFile)
        {
            this.cp.table                   = ilMatFile.GetArray<double>("wt_cp_table");
            this.cp.beta                    = ilMatFile.GetArray<double>("wt_cp_beta");
            this.cp.tsr                     = ilMatFile.GetArray<double>("wt_cp_tsr");
            this.ct.table                   = ilMatFile.GetArray<double>("wt_ct_table");
            this.ct.beta                    = ilMatFile.GetArray<double>("wt_ct_beta");
            this.ct.tsr                     = ilMatFile.GetArray<double>("wt_ct_tsr");
            this.gen.effeciency             = (double)(ilMatFile.GetArray<double>("wt_gen_effeciency"));
            this.rotor.radius               = (double)(ilMatFile.GetArray<double>("wt_rotor_radius"));
            this.rotor.inertia              = (double)(ilMatFile.GetArray<double>("wt_rotor_inertia"));
            this.rotor.ratedspeed           = (double)(ilMatFile.GetArray<double>("wt_rotor_ratedspeed"));
            this.ctrl.pitch.Pgain           = (double)(ilMatFile.GetArray<double>("wt_ctrl_pitch_Pgain"));
            this.ctrl.pitch.Igain           = (double)(ilMatFile.GetArray<double>("wt_ctrl_pitch_Igain"));
            this.ctrl.pitch.ratelim         = (double)(ilMatFile.GetArray<double>("wt_ctrl_pitch_ratelim"));
            this.ctrl.pitch.ulim            = (double)(ilMatFile.GetArray<double>("wt_ctrl_pitch_ulim"));
            this.ctrl.pitch.llim            = (double)(ilMatFile.GetArray<double>("wt_ctrl_pitch_llim"));
            this.ctrl.torq.ratelim          = (double)(ilMatFile.GetArray<double>("wt_ctrl_torq_ratelim"));
            this.ctrl.p_rated               = (double)(ilMatFile.GetArray<double>("wt_ctrl_p_rated"));
        }

    }
}
