using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace SmartBuildingUnityApps
{
    public static class LightControl
    {
        public static void SwichOn(int pAreaId, int pLuminaireId)
        {
            UnityApps.LightManagement.SwichOn(pAreaId, pLuminaireId);
        }

        public static void SwichOff(int pAreaId, int pLuminaireId)
        {
            UnityApps.LightManagement.SwichOff(pAreaId, pLuminaireId);
        }

        public static void SetLevel(int pAreaId, int pLuminaireId, int pLevel)
        {
            UnityApps.LightManagement.SetLevel(pAreaId, pLuminaireId, pLevel);
        }
       
    }
}
