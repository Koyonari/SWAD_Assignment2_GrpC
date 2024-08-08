
namespace SWAD_Assignment2_GrpC
{
    public class VehicleInspection
    {
        public int Id { get; set; }
        public int VehicleRegistrationNumber { get; set; }
        public DateTime InspectionDate { get; set; }
        public string InspectionType { get; set; }
        public string InspectionResult { get; set; }
        public string InspectorName { get; set; }
        public string InspectionLocation { get; set; }

        public VehicleInspection(int id, int vehicleRegistrationNumber, DateTime inspectionDate,
                                 string inspectionType, string inspectionResult,
                                 string inspectorName, string inspectionLocation)
        {
            Id = id;
            VehicleRegistrationNumber = vehicleRegistrationNumber;
            InspectionDate = inspectionDate;
            InspectionType = inspectionType;
            InspectionResult = inspectionResult;
            InspectorName = inspectorName;
            InspectionLocation = inspectionLocation;
        }
    }
}