using System;
using System.Collections.Generic; // Must Header files for csharp

namespace HospitalManagementSystem // To organize code in  frame
{
    interface HospitalOperations
    {
        void ViewDoctorSchedule();
        void AddPatient();
        void ViewPatients();
        void AddPatientSchedule();
        void ViewPatientSchedule();
    }

    abstract class HospitalBase
    {
        public abstract void AddDepartment(Department department);
    }

    class CentralHospital : HospitalBase
    {
        private static List<Department> departmentList = new List<Department>();

        public override void AddDepartment(Department department)
        {
            departmentList.Add(department);
        }

        public static List<Department> GetDepartmentList()
        {
            return departmentList;
        }
    }

    class Department : HospitalOperations
    {
        private Dictionary<int, List<string>> doctorSchedules;
        private Dictionary<int, List<string>> patientSchedules;
        private List<(int doctorId, string doctorName, string specialization)> doctorList;
        private List<(int patientId, string patientName, int age)> patientList;
        private int departmentId;

        public int DepartmentId { get { return departmentId; } }//getter setter for departmentId

        public Department(int departmentId)
        {
            this.departmentId = departmentId;
            doctorSchedules = new Dictionary<int, List<string>>();///
            patientSchedules = new Dictionary<int, List<string>>();
            doctorList = new List<(int, string, string)>();
            patientList = new List<(int, string, int)>();
            new CentralHospital().AddDepartment(this);
        }

        public void AddDoctor(int doctorId, string doctorName, string specialization)
        {
            doctorList.Add((doctorId, doctorName, specialization));
            doctorSchedules[doctorId] = new List<string>();
        }

        public void AddSchedule(int doctorId, string time)
        {
            if (!doctorSchedules.ContainsKey(doctorId))
            {
                Console.WriteLine("\tDoctor ID does not exist.");
                return;
            }

            doctorSchedules[doctorId].Add(time);
        }

        public void ViewDoctors()
        {
            Console.WriteLine("\nDoctors in this department:");
            foreach (var doctor in doctorList)
            {
                Console.WriteLine($"\tDoctor ID: {doctor.doctorId}, Name: {doctor.doctorName}, Specialization: {doctor.specialization}");
            }
        }

        public void ViewDoctorSchedule()
        {
            Console.WriteLine("\nDoctor Schedules:");
            foreach (var doctor in doctorList)
            {
                Console.WriteLine($"\tDoctor ID: {doctor.doctorId}, Name: {doctor.doctorName}, Specialization: {doctor.specialization}");
                if (doctorSchedules[doctor.doctorId].Count > 0)
                {
                    Console.WriteLine("\tAvailable Times:");
                    foreach (var time in doctorSchedules[doctor.doctorId])
                    {
                        Console.WriteLine($"\t- {time}");
                    }
                }
                else
                {
                    Console.WriteLine("\tNo schedule available.");
                }
            }
        }

        public void ViewSchedule(int doctorId)
        {
            if (!doctorSchedules.ContainsKey(doctorId))
            {
                Console.WriteLine("\tDoctor ID does not exist.");
                return;
            }

            Console.WriteLine($"\nSchedule for Doctor ID: {doctorId}");
            if (doctorSchedules[doctorId].Count > 0)
            {
                foreach (var time in doctorSchedules[doctorId])
                {
                    Console.WriteLine($"\t- {time}");
                }
            }
            else
            {
                Console.WriteLine("\tNo schedule available.");
            }
        }

        public void AddPatient()
        {
            try
            {
                Console.Write("Enter Patient ID: ");
                int patientId = int.Parse(Console.ReadLine());
                Console.Write("Enter Patient Name: ");
                string patientName = Console.ReadLine();
                Console.Write("Enter Patient Age: ");
                int age = int.Parse(Console.ReadLine());

                patientList.Add((patientId, patientName, age));
                patientSchedules[patientId] = new List<string>();
                Console.WriteLine("\tPatient added successfully.");
            }
            catch (FormatException)
            {
                Console.WriteLine("\tInvalid input. Please enter valid data.");
            }
        }

        public void AddPatientSchedule()
        {
            try
            {
                Console.Write("Enter Patient ID: ");
                int patientId = int.Parse(Console.ReadLine());

                if (!patientSchedules.ContainsKey(patientId))
                {
                    Console.WriteLine("\tPatient ID does not exist.");
                    return;
                }

                Console.Write("Enter Schedule Date (e.g., 25/11/2024 3:00 PM): ");
                string schedule = Console.ReadLine();
                patientSchedules[patientId].Add(schedule);

                Console.WriteLine("\tPatient schedule added successfully.");
            }
            catch (FormatException)
            {
                Console.WriteLine("\tInvalid input. Please enter valid data.");
            }
        }

        public void ViewPatients()
        {
            Console.WriteLine("\nPatients List:");
            foreach (var patient in patientList)
            {
                Console.WriteLine($"\tPatient ID: {patient.patientId}, Name: {patient.patientName}, Age: {patient.age}");
            }
        }

        public void ViewPatientSchedule()
        {
            try
            {
                Console.Write("Enter Patient ID: ");
                int patientId = int.Parse(Console.ReadLine());

                if (!patientSchedules.ContainsKey(patientId))
                {
                    Console.WriteLine("\tPatient ID does not exist.");
                    return;
                }

                Console.WriteLine($"\nSchedules for Patient ID: {patientId}");
                if (patientSchedules[patientId].Count > 0)
                {
                    foreach (var schedule in patientSchedules[patientId])
                    {
                        Console.WriteLine($"\t- {schedule}");
                    }
                }
                else
                {
                    Console.WriteLine("\tNo schedules available.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("\tInvalid input. Please enter valid data.");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Department cardiology = new Department(1);
            Department neurology = new Department(2);
            Department Dermatologists = new Department(3);
            Department Psychiatrists = new Department(4);
            Department Urologists = new Department(5);
            Department Endocrinologists = new Department(6);


            cardiology.AddDoctor(101, "Dr. Smith", "Cardiologist");
            cardiology.AddDoctor(102, "Dr. John", "Cardiologist");

            neurology.AddDoctor(201, "Dr. Jane", "Neurologist");
            neurology.AddDoctor(202, "Dr. Emily", "Neurologist");

            Dermatologists.AddDoctor(301, "Dr. David", " Dermatologists");
            Dermatologists.AddDoctor(302, "Dr. Emy", " Dermatologists");

            Psychiatrists.AddDoctor(401, "Dr. Sam", "Psychiatrists");
            Psychiatrists.AddDoctor(402, "Dr. Jhon", "Psychiatrists");

            Urologists.AddDoctor(501, "Dr. Rambo", "Urologists");
            Urologists.AddDoctor(502, "Dr. Ama", "Urologists");

            Endocrinologists.AddDoctor(601, "Dr. Witch", "Endocrinologists");
            Endocrinologists.AddDoctor(602, "Dr. None", "Endocrinologists");

            while (true)
            {
                Console.WriteLine("\nOptions: ");
                Console.WriteLine("1 : View doctors in a department");
                Console.WriteLine("2 : Add a schedule for a doctor");
                Console.WriteLine("3 : View doctor schedules in a department");
                Console.WriteLine("4 : Add a patient");
                Console.WriteLine("5 : View patients");
                Console.WriteLine("6 : Add patient schedule");
                Console.WriteLine("7 : View patient schedule");
                Console.WriteLine("8 : Exit");

             try
            {
                Console.Write("Enter Option: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
            case 1:
                Console.WriteLine("Choose a department:");
                foreach (var dept in CentralHospital.GetDepartmentList())
                {
                    Console.WriteLine($"\tDepartment ID: {dept.DepartmentId}");
                }
                Console.Write("Enter Department ID: ");
                int deptId1 = int.Parse(Console.ReadLine());

                Department selectedDept1 = CentralHospital.GetDepartmentList().Find(d => d.DepartmentId == deptId1);
                if (selectedDept1 != null)
                {
                    selectedDept1.ViewDoctors();
                }
                else
                {
                    Console.WriteLine("\tInvalid Department ID.");
                }
                break;

            case 2:
                Console.WriteLine("Choose a department:");
                foreach (var dept in CentralHospital.GetDepartmentList())
                {
                    Console.WriteLine($"\tDepartment ID: {dept.DepartmentId}");
                }
                Console.Write("Enter Department ID: ");
                int deptId2 = int.Parse(Console.ReadLine());

                Department selectedDept2 = CentralHospital.GetDepartmentList().Find(d => d.DepartmentId == deptId2);
                if (selectedDept2 != null)
                {
                    Console.Write("Enter Doctor ID: ");
                    int doctorId2 = int.Parse(Console.ReadLine());
                    Console.Write("Enter Schedule Time (e.g., 25/11/2024 3:00 PM): ");
                    string scheduleTime = Console.ReadLine();
                    selectedDept2.AddSchedule(doctorId2, scheduleTime);
                }
                else
                {
                    Console.WriteLine("\tInvalid Department ID.");
                }
                break;

            case 3:
                Console.WriteLine("Choose a department:");
                foreach (var dept in CentralHospital.GetDepartmentList())
                {
                    Console.WriteLine($"\tDepartment ID: {dept.DepartmentId}");
                }
                Console.Write("Enter Department ID: ");
                int deptId3 = int.Parse(Console.ReadLine());

                Department selectedDept3 = CentralHospital.GetDepartmentList().Find(d => d.DepartmentId == deptId3);
                if (selectedDept3 != null)
                {
                    selectedDept3.ViewDoctorSchedule();
                }
                else
                {
                    Console.WriteLine("\tInvalid Department ID.");
                }
                break;

            case 4:
                Console.WriteLine("Choose a department:");
                foreach (var dept in CentralHospital.GetDepartmentList())
                {
                    Console.WriteLine($"\tDepartment ID: {dept.DepartmentId}");
                }
                Console.Write("Enter Department ID: ");
                int deptId4 = int.Parse(Console.ReadLine());

                Department selectedDept4 = CentralHospital.GetDepartmentList().Find(d => d.DepartmentId == deptId4);
                if (selectedDept4 != null)
                {
                    selectedDept4.AddPatient();
                }
                else
                {
                    Console.WriteLine("\tInvalid Department ID.");
                }
                break;

            case 5:
                Console.WriteLine("Choose a department:");
                foreach (var dept in CentralHospital.GetDepartmentList())
                {
                    Console.WriteLine($"\tDepartment ID: {dept.DepartmentId}");
                }
                Console.Write("Enter Department ID: ");
                int deptId5 = int.Parse(Console.ReadLine());

                Department selectedDept5 = CentralHospital.GetDepartmentList().Find(d => d.DepartmentId == deptId5);
                if (selectedDept5 != null)
                {
                    selectedDept5.ViewPatients();
                }
                else
                {
                    Console.WriteLine("\tInvalid Department ID.");
                }
                break;

            case 6:
                Console.WriteLine("Choose a department:");
                foreach (var dept in CentralHospital.GetDepartmentList())
                {
                    Console.WriteLine($"\tDepartment ID: {dept.DepartmentId}");
                }
                Console.Write("Enter Department ID: ");
                int deptId6 = int.Parse(Console.ReadLine());

                Department selectedDept6 = CentralHospital.GetDepartmentList().Find(d => d.DepartmentId == deptId6);
                if (selectedDept6 != null)
                {
                    selectedDept6.AddPatientSchedule();
                }
                else
                {
                    Console.WriteLine("\tInvalid Department ID.");
                }
                break;

            case 7:
                Console.WriteLine("Choose a department:");
                foreach (var dept in CentralHospital.GetDepartmentList())
                {
                    Console.WriteLine($"\tDepartment ID: {dept.DepartmentId}");
                }
                Console.Write("Enter Department ID: ");
                int deptId7 = int.Parse(Console.ReadLine());

                Department selectedDept7 = CentralHospital.GetDepartmentList().Find(d => d.DepartmentId == deptId7);
                if (selectedDept7 != null)
                {
                    selectedDept7.ViewPatientSchedule();
                }
                else
                {
                    Console.WriteLine("\tInvalid Department ID.");
                }
                break;

            case 8:
                Console.WriteLine("\nExiting the system. Have a nice day!");
                return;

            default:
                Console.WriteLine("\nInvalid Option. Please try again.");
                break;
        }
            }
            catch (FormatException)
            {
                Console.WriteLine("\tInvalid input. Please enter an integer.");
            }
            }

        }
    }
}
