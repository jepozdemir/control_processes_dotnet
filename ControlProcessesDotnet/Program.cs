using System.Diagnostics;

class Program
{
	static void Main()
	{
		File.WriteAllText("example.txt", "example");

		OpenNotepad();
		OpenSpecificTextFile();
		PassingArguments_Sample();
		ControlProcessPriority_Sample();
		RunningProcessSilently_Sample();
		DetectingProcessExit_Sample();
		EnvironmentalVariables_Sample();
		RedirectStandardInput_Sample();
		RedirectStandardOutput_Sample();
		RedirectStandardError_Sample();
		MonitoringProcesses_Sample();
		KillingProcess_Sample();
	}

	static void OpenNotepad()
	{
		Process.Start("notepad.exe");
	}

	static void OpenSpecificTextFile()
	{
		Process.Start("notepad.exe", "example.txt");
	}

	static void PassingArguments_Sample()
	{
		var startInfo = new ProcessStartInfo
		{
			FileName = "notepad.exe",
			Arguments = "example.txt"
		};

		var process = new Process
		{
			StartInfo = startInfo
		};

		process.Start();
	}

	static void ControlProcessPriority_Sample()
	{
		var startInfo = new ProcessStartInfo
		{
			FileName = "notepad.exe"
		};

		var process = new Process
		{
			StartInfo = startInfo
		};

		process.Start();

		// Set process priority to BelowNormal
		process.PriorityClass = ProcessPriorityClass.BelowNormal;

		Console.WriteLine($"Process {process.Id} started with priority {process.PriorityClass}.");
	}

	static void RunningProcessSilently_Sample()
	{
		var startInfo = new ProcessStartInfo
		{
			FileName = "cmd.exe",
			Arguments = "/c dir",
			CreateNoWindow = true,
			UseShellExecute = false
		};

		var process = new Process
		{
			StartInfo = startInfo
		};

		process.Start();
		process.WaitForExit();
	}

	static void DetectingProcessExit_Sample()
	{
		var startInfo = new ProcessStartInfo
		{
			FileName = "notepad.exe"
		};

		var process = new Process
		{
			StartInfo = startInfo,
			EnableRaisingEvents = true
		};

		process.Exited += (sender, e) =>
		{
			Console.WriteLine("Process has exited.");
		};

		process.Start();
		Console.WriteLine("Waiting for process to exit...");
		process.WaitForExit();
	}

	static void EnvironmentalVariables_Sample()
	{
		ProcessStartInfo startInfo = new ProcessStartInfo
		{
			FileName = "cmd.exe",
			Arguments = "/c echo %MY_VAR%",
			RedirectStandardOutput = true,
			UseShellExecute = false,
			CreateNoWindow = true
		};

		startInfo.EnvironmentVariables["MY_VAR"] = "Hello, Environment!";

		Process process = new Process
		{
			StartInfo = startInfo
		};

		process.Start();

		string output = process.StandardOutput.ReadToEnd();
		process.WaitForExit();

		Console.WriteLine("Output: " + output);
	}

	static void RedirectStandardInput_Sample()
	{
		var startInfo = new ProcessStartInfo
		{
			FileName = "cmd.exe",
			RedirectStandardInput = true,
			UseShellExecute = false,
			CreateNoWindow = true
		};

		Process process = new Process
		{
			StartInfo = startInfo
		};

		process.Start();

		using (StreamWriter writer = process.StandardInput)
		{
			if (writer.BaseStream.CanWrite)
			{
				writer.WriteLine("echo Hello from ProcessStartInfo!");
				writer.WriteLine("dir");
			}
		}

		process.WaitForExit();
	}

	static void RedirectStandardOutput_Sample()
	{
		var startInfo = new ProcessStartInfo
		{
			FileName = "cmd.exe",
			Arguments = "/C dir", // Run 'dir' command
			RedirectStandardOutput = true, // Capture the output
			UseShellExecute = false,
			CreateNoWindow = true
		};

		using (var process = new Process())
		{
			process.StartInfo = startInfo;
			process.Start();

			// Read and display the output
			string output = process.StandardOutput.ReadToEnd();
			process.WaitForExit();
			Console.WriteLine(output);
		}
	}

	static void RedirectStandardError_Sample()
	{
		ProcessStartInfo startInfo = new ProcessStartInfo
		{
			FileName = "cmd.exe",
			Arguments = "/c non_existing_command",
			RedirectStandardError = true,
			UseShellExecute = false,
			CreateNoWindow = true
		};

		Process process = new Process
		{
			StartInfo = startInfo
		};

		process.Start();

		string error = process.StandardError.ReadToEnd();
		process.WaitForExit();

		Console.WriteLine("Error: " + error);
	}

	static void MonitoringProcesses_Sample()
	{
		Process[] processes = Process.GetProcesses();

		foreach (Process process in processes)
		{
			Console.WriteLine($"Process: {process.ProcessName}, ID: {process.Id}");
		}
	}

	static void KillingProcess_Sample()
	{
		Process[] notepadProcesses = Process.GetProcessesByName("notepad");

		foreach (Process process in notepadProcesses)
		{
			process.Kill();
			Console.WriteLine($"Killed process: {process.ProcessName}, ID: {process.Id}");
		}
	}
}