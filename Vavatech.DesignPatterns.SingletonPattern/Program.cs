using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Vavatech.DesignPatterns.SingletonPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            byte x = 255;

            x++;

            // checked
            // {
            //     x++;
            // }

            //PermissionService permissionService1 = new PermissionService();

            //PermissionService permissionService2 = new PermissionService();

            PermissionService permissionService1 = PermissionService.Instance;
            PermissionService permissionService2 = PermissionService.Instance;
            PermissionService permissionService3 = PermissionService.Instance;

            if (ReferenceEquals(permissionService1, permissionService3))
            {
                Console.WriteLine("identyczne");
            }

            Logger logger1 = Singleton<Logger>.Instance;
            Logger logger2 = Singleton<Logger>.Instance;

            if (ReferenceEquals(logger1, logger2))
            {
                Console.WriteLine("identyczne");
            }

        }
    }

    class Logger
    {
        private bool isOpened;

        public void WriteLine(string content)
        {
            isOpened = true;

            Console.WriteLine(content);

            isOpened = false;

        }

    }

    // Note: Not thread safe!
    class PermissionService
    {
        // ctor
        protected PermissionService()
        {
        }

        private static PermissionService instance;
        public static PermissionService Instance
        {
            get
            {
                if (instance==null)
                {
                    instance = new PermissionService();
                }
                return instance;
            }
        }
    }


   
    class SafePermissionService
    {
        // ctor
        protected SafePermissionService()
        {
        }

        private static object syncLock = new object();

        private static SafePermissionService instance;
        public static SafePermissionService Instance
        {
            get
            {
                lock (syncLock)
                {
                    if (instance == null)
                    {
                        instance = new SafePermissionService();
                    }
                    return instance;
                }
            }
        }
    }


    class Singleton<T>
        where T : class, new()
    {
        // ctor
        protected Singleton()
        {
        }

        private static object syncLock = new object();

        private static T instance;
        public static T Instance
        {
            get
            {
                lock (syncLock)
                {
                    if (instance == null)
                    {
                        instance = new T();
                    }
                    return instance;
                }
            }
        }
    }
}
