using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Image_resizer
{
    public class PathFile
    {
        private string[] _args = new string[2];
        private string _inputPath;
       
        private string _path;
        private bool _pathsExists;
        public PathFile(string path)
        {
            _path = path;
            _pathsExists = TrySetArgs(path);
        }
        private bool TrySetArgs(string path)
        {
            if (File.Exists(path))
            {
                _args = File.ReadAllLines(path);

                if (_args.Length == 2)
                    if (!string.IsNullOrEmpty(_args[0]) && !string.IsNullOrEmpty(_args[1]))
                    {
                        _inputPath = _args[0];
                        return true;
                    }
                return false;
            }
            else File.Create(path).Dispose();

            return false;
        }
        private void RewriteFile()
        {
            File.WriteAllLines(_path, [_inputPath]);
        }
        public void SetInput(string path)
        {
            _inputPath = path;
            RewriteFile();
        }

        public bool PathsExists => _pathsExists;
        public string InputPath => _inputPath;
    }
}
