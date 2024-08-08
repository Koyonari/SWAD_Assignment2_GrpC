namespace SWAD_Assignment2_GrpC
{
	public class AvailabilitySchedule
	{
		public int Id { get; set; }
		public DateTime StartDatetime { get; set; }
		public DateTime EndDatetime { get; set; }

		public AvailabilitySchedule(int id, DateTime startDatetime, DateTime endDatetime)
		{
			Id = id;
			StartDatetime = startDatetime;
			EndDatetime = endDatetime;
		}
	}
}