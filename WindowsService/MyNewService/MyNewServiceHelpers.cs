using System.IO;

internal static class MyNewServiceHelpers
{

    private static double GetDriveVal()
    {
        double DriveVal = 0;
        DriveInfo gDrive = new DriveInfo("G");
        if (gDrive.IsReady)
        {
             DriveVal = 100 - ((gDrive.AvailableFreeSpace / (float)gDrive.TotalSize) * 100);
            
        }
        return DriveVal;
    }
}