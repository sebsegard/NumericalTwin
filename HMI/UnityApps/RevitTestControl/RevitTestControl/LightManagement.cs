using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using System.Threading;
using System.Windows.Media.Imaging;
using MySql.Data.MySqlClient;

namespace UnityApps
{
    public static class LightManagement
    {
        public static void Init()
        {
            LightControl.API.PhilipsRestApi.Init();
        }

        public static void SwichOn(int pAreaId, int pLuminaireId)
        {
            LightControl.API.PhilipsRestApi.Init();
            LightControl.API.PhilipsRestApi.SetLuminerLevel(pAreaId, pLuminaireId, 100);
        }

        public static void SwichOff(int pAreaId, int pLuminaireId)
        {
            LightControl.API.PhilipsRestApi.Init();
            LightControl.API.PhilipsRestApi.SetLuminerLevel(pAreaId, pLuminaireId, 0);
        }

        public static void SetLevel(int pAreaId, int pLuminaireId, int pLevel)
        {
            LightControl.API.PhilipsRestApi.Init();
            LightControl.API.PhilipsRestApi.SetLuminerLevel(pAreaId, pLuminaireId, pLevel);
        }

    }
}


