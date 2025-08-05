using Domain.Entities;

namespace Infrastracture.Services.MeetingScheduler
{
    public class MeetingSchedulerAlgorithm
    {
        public static DateTime? FindEarliestAvailableSlot(
            List<List<Meeting>> userMeetings,
            TimeSpan duration,
            DateTime earliestStart,
            DateTime latestEnd)
        {
            var businessStart = earliestStart.Date.AddHours(9);
            var businessEnd = earliestStart.Date.AddHours(17);

            if (earliestStart < businessStart)
            {
                earliestStart = businessStart;
            }

            if (latestEnd > businessEnd)
            {
                latestEnd = businessEnd;
            }


            var timeline = new List<(DateTime Start, DateTime End)>();

            foreach (var meetings in userMeetings)
            {
                foreach (var m in meetings)
                    timeline.Add((m.StartTime, m.EndTime));
            }

            timeline = timeline
                .Where(t => t.End > earliestStart && t.Start < latestEnd)
                .OrderBy(t => t.Start)
                .ToList();

            var merged = new List<(DateTime Start, DateTime End)>();
            foreach (var interval in timeline)
            {
                if (!merged.Any() || interval.Start > merged.Last().End)
                    merged.Add(interval);
                else
                    merged[merged.Count - 1] = (merged.Last().Start,
                                                interval.End > merged.Last().End ? interval.End : merged.Last().End);
            }

            DateTime cursor = earliestStart;

            foreach (var (start, end) in merged)
            {
                if (cursor + duration <= start)
                    return cursor;

                if (end > cursor)
                    cursor = end;
            }


            if (cursor + duration <= latestEnd)
            {
                return cursor;
            }
            else
            {
                return null;
            }

        }

    }

}
