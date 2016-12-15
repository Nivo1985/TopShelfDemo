using System;
using System.IO;
using System.Threading;
using Topshelf.Logging;

namespace TopShelfDemo
{
    public class ServiceLogic
    {
        private FileSystemWatcher _fileSystemWatcher;
        private static readonly LogWriter _logger = HostLogger.Get<ServiceLogic>();

        public bool Start()
        {
            _fileSystemWatcher = new FileSystemWatcher(@"d:\TopShelfDemo", "*.jpg");
            _fileSystemWatcher.NotifyFilter = NotifyFilters.Attributes |
                    NotifyFilters.CreationTime |
                    NotifyFilters.FileName |
                    NotifyFilters.LastAccess |
                    NotifyFilters.LastWrite |
                    NotifyFilters.Size |
                    NotifyFilters.Security;

            _fileSystemWatcher.Created += (sender, args) =>
                {
                    while (true)
                    {
                        Thread.Sleep(2500);
                        if (UtilityMethods.GetExclusiveAccess(args.FullPath))
                        {
                            var fileInfo = new FileInfo(args.FullPath);

                            if (args.Name.ToLower().Contains("virus"))
                            {
                                fileInfo.Delete();
                                _logger.InfoFormat("{0} deleted", args.FullPath);
                            }
                            else
                            {
                                fileInfo.Rename(Path.GetFileNameWithoutExtension(args.FullPath) + "_ValidFile" + fileInfo.Extension);
                                _logger.InfoFormat("{0} veryfied", args.FullPath);
                            }

                            break;
                        }
                    }


                };

            _fileSystemWatcher.EnableRaisingEvents = true;

            return true;
        }

        public bool Stop()
        {
            _fileSystemWatcher.Dispose();
            return true;
        }
    }
}