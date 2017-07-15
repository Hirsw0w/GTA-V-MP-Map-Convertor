using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTAVMapConverter
{
    public static class conSettings
    {
        public static bool objectsToArray = false;
        public static bool vehiclesToArray = false;
        public static bool pedsToArray = false;
        public static bool pickupsToArray = false;
        public static bool markersToArray = false;

        public static string objectString = "API.createObject";
        public static int objectRotType = 0; // 0 - Rotation | 1 - Quantinum
        public static int objectsDimension = 0;
        public static string objectsArrayName = "Objects";
        public static int objectsArrayStartCount = 0;

        public static string vehicleString = "API.createVehicle";
        public static int vehiclesDimension = 0;
        public static string vehiclesArrayName = "Vehicles";
        public static int vehiclesArrayStartCount = 0;

        public static string pedString = "API.createPed";
        public static int pedsDimension = 0;
        public static string pedsArrayName = "Peds";
        public static int pedsArrayStartCount = 0;

        public static string pickupString = "API.createPickup";
        public static int pickupsDimension = 0;
        public static string pickupsArrayName = "Pickups";
        public static int pickupsArrayStartCount = 0;

        public static string markerString = "API.createMarker";
        public static int markersDimension = 0;
        public static string markersArrayName = "Markers";
        public static int markersArrayStartCount = 0;
    }
}
