using System.IO;

namespace Offwind.Infrastructure.SaveCommands
{
    public abstract class SaveCommand<TModel> : ICommand
    {
        private TModel _model;
        private string _caseDir;

        protected TModel Model { get { return _model; } }
        protected string CaseDir { get { return _caseDir; } }

        public void SetModel(TModel model)
        {
            _model = model;
        }

        public void SetDirectory(string caseDir)
        {
            _caseDir = caseDir;
        }

        public abstract void Execute();

        protected string InitBaseDir(BaseDirType type)
        {
            string t;
            switch (type)
            {
                case BaseDirType.Constant:
                    t = "constant";
                    break;
                case BaseDirType.System:
                    t = "system";
                    break;
                default:
                    t = "";
                    break;
            }

            var dir = Path.Combine(CaseDir, t);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            return dir;
        }
    }
}
