namespace SmartHomeController
{
    public class SmartHomeController
    {
        //private properties
        private List<SmartDevice> devices = new List<SmartDevice>();

        //public properties

        //Methods
        public void AddDevice(SmartDevice device)
        {
            //is there anything within the device
            if (device == null)
            {
                throw new ArgumentNullException(nameof(device));
            }

            //does the device ID already exist in the list
            if (HasDevice(device.DeviceID))
            {
                throw new ArgumentException($"This ID exists, {device.DeviceID}");
            }
            devices.Add(device);
        }

        public bool HasDevice(int deviceID)
        {
            int recordIndex = GetRecordIndex(deviceID);
            if (recordIndex == -1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public int GetRecordIndex(int deviceID)
        {
            for (int indexNo = 0; indexNo < devices.Count; indexNo++)
            {
                if ((devices[indexNo]).DeviceID == deviceID)
                {
                    return indexNo;
                }
            }

            return -1;
        }

        private void AddStarterDevices()
        {
            AddDevice(new SmartLight(50, "Warm white", 1, "Kitchen light", true));
            AddDevice(new SmartSpeaker(30, false, 2, "Kitchen speaker", true));
        }

        private void ShowMenu()
        {
            Console.WriteLine();
            Console.WriteLine("=== Smart Home Menu ===");
            Console.WriteLine("1. Show all devices");
            Console.WriteLine("2. Remove one device");
            Console.WriteLine("0. Exit");
        }

        private int ReadChoice(string prompt, int min, int max)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (!int.TryParse(input, out int value))
                {
                    Console.WriteLine("Please enter a whole number");
                }
                else if (value < min || value > max)
                {
                    Console.WriteLine($"Please enter a number between {min} and {max}");
                }
                else
                {
                    return value;
                }
            }
        }

        public void ShowAllStatus()
        {
            foreach (SmartDevice device in devices)
            {
                device.GetStatus();
            }
        }

        private void RemoveOneDevice()
        {
            int id = ReadWholeNumber("Device ID to remove : ");
            if (RemoveDevice(id) == true)
            {
                Console.WriteLine($"Device {id} was removed");
            }
            else
            {
                Console.WriteLine($"There is no device with ID {id}");
            }
        }

        private bool RemoveDevice(int deviceID)
        {
            int index = GetRecordIndex(deviceID); 
            if (index < 0)
            {
                return false;
            }
            devices.RemoveAt(index);
            return true;
        }

        private int ReadWholeNumber(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int parsedValue))
                {
                    return parsedValue;
                }
                Console.WriteLine("Please enter a whole number");
            }
        }

        private void  AddNewDevice()
        {
            Console.WriteLine("1. Light, 2. Thermostat, 3. Speaker, 4. Camera");
            int answer = ReadChoice("What kind of device? Choose an option.", 1, 4);
            int id = ReadWholeNumber("Device ID: ");

            if (HasDevice(id) == true)
            {
                Console.WriteLine($"There is already a device with id {id}");
            }

            try
            {
                SmartDevice device = 
            }
            catch (Exception ex)
            {

            }
        }

        private void CreateDevice(int smartDeviceNo, int id, string name, bool on)
        {
            switch (smartDeviceNo)
            {
                case 1:
                    double brightness = ReadWholeNumber("Brightness (0 - 100)");
                    string colour = ReadText("Colour ");
                    return new SmartLight(brightness, colour, id, name);
                    break;
            }
        }

        static void Main(string[] args)
        {
            SmartHomeController controller = new SmartHomeController();
            controller.AddStarterDevices();

            bool running = true;
            while (running == true)
            {
                controller.ShowMenu();
                int menuChoice = controller.ReadChoice("Please select an option", 0, 2);

                switch (menuChoice)
                {
                    case 1:
                        controller.ShowAllStatus();
                        break;
                    case 2:
                        controller.RemoveOneDevice();
                        break;
                    case 0:
                        running = false;
                        break;
                }
            }
        }
    }
}