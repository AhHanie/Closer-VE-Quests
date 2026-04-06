using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace Closer_VE_Quests
{
    public class ModSettings : Verse.ModSettings
    {
        public const int DefaultAncientSiloMinDistance = 7;
        public const int DefaultAncientSiloMaxDistance = 27;
        public const int DefaultAncientIcbmLaunchSiteMinDistance = 7;
        public const int DefaultAncientIcbmLaunchSiteMaxDistance = 27;
        public const int DefaultGeneratorSiteMinDistance = 7;
        public const int DefaultGeneratorSiteMaxDistance = 27;
        public const int DefaultCryptoforgeSiteMinDistance = 20;
        public const int DefaultCryptoforgeSiteMaxDistance = 100;
        public const int AbsoluteMinDistance = 1;
        public const int AbsoluteMaxDistance = 200;

        public static int ancientSiloMinDistance = DefaultAncientSiloMinDistance;
        public static int ancientSiloMaxDistance = DefaultAncientSiloMaxDistance;
        public static int ancientIcbmLaunchSiteMinDistance = DefaultAncientIcbmLaunchSiteMinDistance;
        public static int ancientIcbmLaunchSiteMaxDistance = DefaultAncientIcbmLaunchSiteMaxDistance;
        public static int generatorSiteMinDistance = DefaultGeneratorSiteMinDistance;
        public static int generatorSiteMaxDistance = DefaultGeneratorSiteMaxDistance;
        public static int cryptoforgeSiteMinDistance = DefaultCryptoforgeSiteMinDistance;
        public static int cryptoforgeSiteMaxDistance = DefaultCryptoforgeSiteMaxDistance;

        public override void ExposeData()
        {
            Scribe_Values.Look(ref ancientSiloMinDistance, "ancientSiloMinDistance", DefaultAncientSiloMinDistance);
            Scribe_Values.Look(ref ancientSiloMaxDistance, "ancientSiloMaxDistance", DefaultAncientSiloMaxDistance);
            Scribe_Values.Look(ref ancientIcbmLaunchSiteMinDistance, "ancientIcbmLaunchSiteMinDistance", DefaultAncientIcbmLaunchSiteMinDistance);
            Scribe_Values.Look(ref ancientIcbmLaunchSiteMaxDistance, "ancientIcbmLaunchSiteMaxDistance", DefaultAncientIcbmLaunchSiteMaxDistance);
            Scribe_Values.Look(ref generatorSiteMinDistance, "generatorSiteMinDistance", DefaultGeneratorSiteMinDistance);
            Scribe_Values.Look(ref generatorSiteMaxDistance, "generatorSiteMaxDistance", DefaultGeneratorSiteMaxDistance);
            Scribe_Values.Look(ref cryptoforgeSiteMinDistance, "cryptoforgeSiteMinDistance", DefaultCryptoforgeSiteMinDistance);
            Scribe_Values.Look(ref cryptoforgeSiteMaxDistance, "cryptoforgeSiteMaxDistance", DefaultCryptoforgeSiteMaxDistance);
        }

        public static void Validate()
        {
            ancientSiloMinDistance = Mathf.Clamp(ancientSiloMinDistance, AbsoluteMinDistance, AbsoluteMaxDistance);
            ancientSiloMaxDistance = Mathf.Clamp(ancientSiloMaxDistance, AbsoluteMinDistance, AbsoluteMaxDistance);
            ancientIcbmLaunchSiteMinDistance = Mathf.Clamp(ancientIcbmLaunchSiteMinDistance, AbsoluteMinDistance, AbsoluteMaxDistance);
            ancientIcbmLaunchSiteMaxDistance = Mathf.Clamp(ancientIcbmLaunchSiteMaxDistance, AbsoluteMinDistance, AbsoluteMaxDistance);
            generatorSiteMinDistance = Mathf.Clamp(generatorSiteMinDistance, AbsoluteMinDistance, AbsoluteMaxDistance);
            generatorSiteMaxDistance = Mathf.Clamp(generatorSiteMaxDistance, AbsoluteMinDistance, AbsoluteMaxDistance);
            cryptoforgeSiteMinDistance = Mathf.Clamp(cryptoforgeSiteMinDistance, AbsoluteMinDistance, AbsoluteMaxDistance);
            cryptoforgeSiteMaxDistance = Mathf.Clamp(cryptoforgeSiteMaxDistance, AbsoluteMinDistance, AbsoluteMaxDistance);

            if (ancientSiloMinDistance > ancientSiloMaxDistance)
            {
                ancientSiloMaxDistance = ancientSiloMinDistance;
            }

            if (ancientIcbmLaunchSiteMinDistance > ancientIcbmLaunchSiteMaxDistance)
            {
                ancientIcbmLaunchSiteMaxDistance = ancientIcbmLaunchSiteMinDistance;
            }

            if (generatorSiteMinDistance > generatorSiteMaxDistance)
            {
                generatorSiteMaxDistance = generatorSiteMinDistance;
            }

            if (cryptoforgeSiteMinDistance > cryptoforgeSiteMaxDistance)
            {
                cryptoforgeSiteMaxDistance = cryptoforgeSiteMinDistance;
            }
        }
    }
}
