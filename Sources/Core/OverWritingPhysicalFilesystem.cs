using NuGet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazooka.Core
{
    public class OverWritingPhysicalFilesystem : PhysicalFileSystem
    {
        public OverWritingPhysicalFilesystem(string path) : base(path) { }

        /// <summary>
        ///     Adds a file in the filesystem. Overrides the default to override any existing file
        ///     due to a problem when installing and some files weren't deleted
        /// </summary>
        /// <param name="path"></param>
        /// <param name="writeToStream"></param>
        public override void AddFile(string path, Action<System.IO.Stream> writeToStream)
        {
            EnsureDirectory(Path.GetDirectoryName(path));

            string fullPath = GetFullPath(path);

            using (Stream outputStream = File.Open(fullPath,FileMode.Create))
            {
                writeToStream(outputStream);
            }
        }

        public override void AddFile(string path, Stream stream)
        {
            if (File.Exists(Path.Combine(this.Root,path)))
            {
                File.Delete(Path.Combine(this.Root, path));
            }

            base.AddFile(Path.Combine(this.Root, path), stream);
        }

        public override void AddFiles(IEnumerable<IPackageFile> files, string rootDir)
        {
            foreach (var file in files)
            {
                this.AddFile(file.Path, file.GetStream());
            }
        }

        public override Stream CreateFile(string path)
        {
            string fullPath = GetFullPath(path);

            // before creating the file, ensure the parent directory exists first.
            string directory = Path.GetDirectoryName(fullPath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            return File.Create(fullPath);
        }
    }
}
