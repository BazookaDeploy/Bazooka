namespace Bazooka.Core
{
    using NuGet;
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    ///     Specialization of nuget file system that overrides any file
    /// </summary>
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

        /// <summary>
        ///     Add a file
        /// </summary>
        /// <param name="path">Path to addd  the file</param>
        /// <param name="stream">Stream for the file</param>
        public override void AddFile(string path, Stream stream)
        {
            if (File.Exists(Path.Combine(this.Root,path)))
            {
                File.Delete(Path.Combine(this.Root, path));
            }

            base.AddFile(Path.Combine(this.Root, path), stream);
        }

        /// <summary>
        ///     Adds files to a directory
        /// </summary>
        /// <param name="files">Files to add</param>
        /// <param name="rootDir">Root directory</param>
        public override void AddFiles(IEnumerable<IPackageFile> files, string rootDir)
        {
            foreach (var file in files)
            {
                this.AddFile(file.Path, file.GetStream());
            }
        }

        /// <summary>
        ///     Creates a file 
        /// </summary>
        /// <param name="path">Path where to create the file</param>
        /// <returns>Stream of created file</returns>
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
