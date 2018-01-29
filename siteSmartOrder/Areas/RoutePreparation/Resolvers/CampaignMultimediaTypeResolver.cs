using System.Collections.Generic;
using System.Linq;
using siteSmartOrder.Areas.RoutePreparation.Enums;
using siteSmartOrder.Areas.RoutePreparation.Models;
using siteSmartOrder.Infrastructure.Extensions;

namespace siteSmartOrder.Areas.RoutePreparation.Resolvers
{
    public static class CampaignMultimediaTypeResolver
    {
        public static int ResolverType(this List<CampaignMultimedia> campaignMultimediaTypes)
        {
            var campaignMultimediaTypeIds = campaignMultimediaTypes.Select(x => x.MultimediaType);
            if (campaignMultimediaTypeIds.Any(multimediaType => multimediaType.IsEqualTo((int)CampaignMultimediaType.Video)))
                return (int) CampaignMultimediaType.Video;

            return (int) CampaignMultimediaType.Imagen;
        }
    }
}