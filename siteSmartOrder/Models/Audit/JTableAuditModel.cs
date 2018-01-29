using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace siteSmartOrder.Models.Audit
{
    public class JTableAuditModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string BranchCode { get; set; }
        public string Date { get; set; }

        public List<AuditCampaignRecord> ConvertModelToRecords(List<AuditCampaign> models)
        {
            return models.Select(m => new AuditCampaignRecord
            {
                Id = m.Id,
                Name = m.Name,
                StartDate = m.StartDate,
                EndDate = m.EndDate,
                StatusColumn =
                    Convert(m.StartDate) > DateTime.Now.Date
                        ? Constants.StatusUnStartedColumn
                        : DateTime.Now.Date > Convert(m.EndDate)
                            ? Constants.StatusFinalizedColumn
                            : Constants.StatusInProgressColumn,
                PosibleExtend = DateTime.Now.Date <= Convert(m.EndDate),
                ExtendColumn =
                    DateTime.Now.Date > Convert(m.EndDate)
                        ? Constants.ExtendColumnDisabled
                        : Constants.ExtendColumn,
                UsersColumn = Constants.DetailColumn,
            }).ToList();
        }

        public class AuditCampaignRecord
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string StartDate { get; set; }
            public string EndDate { get; set; }

            public string StatusColumn { get; set; }
            public string ExtendColumn { get; set; }
            public string UsersColumn { get; set; }
            public bool PosibleExtend { get; set; }
        }

        public static DateTime Convert(string stringValue)
        {

            var formats = new[] { "ddMMyyyyHHmmss", "dd-MM-yyyy", "dd/MM/yyyy", "dd-MM-yyyy hh:mm:ss tt", "dd/MM/yyyy hh:mm:ss tt", "dd-MM-yyyy HH:mm:ss", "dd/MM/yyyy HH:mm:ss" };
            DateTime dateTime;
            if (DateTime.TryParseExact(stringValue, formats, null, DateTimeStyles.None, out dateTime))
            {
                return dateTime;
            }
            else
            {
                throw new Exception("Invalid Date, Date format must be 'dd/MM/yyyy'.");
            }
        }
    }
}